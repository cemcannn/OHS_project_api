using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using NpgsqlTypes;
using OHS_program_api.API.Configurations;
using OHS_program_api.API.Configurations.ColumnWriters;
using OHS_program_api.API.Extensions;
using OHS_program_api.API.Filters;
using OHS_program_api.API.Middlewares;
using OHS_program_api.API.Services;
using OHS_program_api.Application;
using OHS_program_api.Application.Validators.Personnels;
using OHS_program_api.Infrastructure;
using OHS_program_api.Infrastructure.Filters;
using OHS_program_api.Infrastructure.Services.Storage.Local;
using OHS_program_api.Persistence;
using OHS_program_api.SignalR;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using System.Security.Claims;
using System.Text;
using OHS_program_api.API.Seed;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();//Client'tan gelen request neticvesinde oluþturulan HttpContext nesnesine katmanlardaki class'lar üzerinden(busineess logic) eriþebilmemizi saðlayan bir servistir.
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddSignalRServices();
builder.Services.AddStorage<LocalStorage>();
builder.Services.AddMemoryCache(); // In-memory cache
builder.Services.AddScoped<ICacheService, MemoryCacheService>(); // Cache service
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
));

var seqServerUrl = builder.Configuration["Seq:ServerURL"] ?? "http://localhost:5341";

Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
//    .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("SqlServer"), "logs", autoCreateSqlTable: true,
//        columnOptions: new Serilog.Sinks.MSSqlServer.ColumnOptions
//{
//            AdditionalColumns = new List<SqlColumn>
//            {
//                new SqlColumn {ColumnName = "message", DataType = System.Data.SqlDbType.NVarChar, DataLength = -1},
//                new SqlColumn {ColumnName = "message_template", DataType = System.Data.SqlDbType.NVarChar, DataLength = -1},
//                new SqlColumn {ColumnName = "level", DataType = System.Data.SqlDbType.VarChar, DataLength = 128},
//                new SqlColumn {ColumnName = "time_stamp", DataType = System.Data.SqlDbType.DateTimeOffset},
//                new SqlColumn {ColumnName = "exception", DataType = System.Data.SqlDbType.NVarChar, DataLength = -1},
//                new SqlColumn {ColumnName = "log_event", DataType = System.Data.SqlDbType.NVarChar, DataLength = -1},
//                new SqlColumn {ColumnName = "user_name", DataType = System.Data.SqlDbType.NVarChar, DataLength = 256}
//            }
//        })
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"), "logs",
        needAutoCreateTable: true,
        columnOptions: new Dictionary<string, ColumnWriterBase>
        {
            {"message", new RenderedMessageColumnWriter(NpgsqlDbType.Text)},
            {"message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text)},
            {"level", new LevelColumnWriter(true , NpgsqlDbType.Varchar)},
            {"time_stamp", new TimestampColumnWriter(NpgsqlDbType.Timestamp)},
            {"exception", new ExceptionColumnWriter(NpgsqlDbType.Text)},
            {"log_event", new LogEventSerializedColumnWriter(NpgsqlDbType.Json)},
            {"user_name", new UsernameColumnWriter()}
        })
    .WriteTo.Seq(seqServerUrl)
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
    options.Filters.Add<RolePermissionFilter>();
})
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreatePersonnelValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

var securityKey = builder.Configuration["Token:SecurityKey"] ?? throw new InvalidOperationException("Token:SecurityKey is not configured.");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("Admin", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),
        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
        NameClaimType = ClaimTypes.Name
    };

    // SignalR WebSocket/SSE bağlantılarında token querystring'den gelebilir:
    // https://localhost:7170/accidents-hub?access_token=...
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;

            if (!string.IsNullOrEmpty(accessToken) &&
                (path.StartsWithSegments("/accidents-hub") || path.StartsWithSegments("/personnels-hub")))
            {
                context.Token = accessToken;
            }

            return Task.CompletedTask;
        }
    };
});


var app = builder.Build();

// Development ortamında SuperAdmin kullanıcı/rol seed
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    await SuperAdminSeeder.SeedAsync(scope.ServiceProvider, app.Configuration, app.Logger);
}

// Rate limiting middleware
app.UseMiddleware<RateLimitingMiddleware>();

app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());
app.UseStaticFiles();
app.UseSerilogRequestLogging();
app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.Use(async (context, next) =>
{
    var userName = context.User?.Identity?.IsAuthenticated == true
        ? context.User.Identity?.Name ?? string.Empty
        : string.Empty;
    LogContext.PushProperty("user_name", userName);
    await next();
});
app.MapControllers();
app.MapHubs();
app.Run();

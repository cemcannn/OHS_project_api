using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OHSprogramapi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personnels_Professions_ProfessionId",
                table: "Personnels");

            migrationBuilder.DropTable(
                name: "ActualWageAndPersonnelNumbers");

            migrationBuilder.DropTable(
                name: "HealthPersonnels");

            migrationBuilder.DropTable(
                name: "OHSComittees");

            migrationBuilder.DropTable(
                name: "PeriodicControls");

            migrationBuilder.DropTable(
                name: "Reasons");

            migrationBuilder.DropTable(
                name: "SafetyEquipments");

            migrationBuilder.DropTable(
                name: "SafetyExperts");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropTable(
                name: "TaskInstructions");

            migrationBuilder.DropTable(
                name: "TypeOfDiseases");

            migrationBuilder.DropTable(
                name: "TypeOfExaminations");

            migrationBuilder.DropTable(
                name: "TypeOfReports");

            migrationBuilder.DropTable(
                name: "WorkplaceTestAndAnalyses");

            migrationBuilder.DropTable(
                name: "TypeOfWorkEquipments");

            migrationBuilder.DropTable(
                name: "TypeOfEquipments");

            migrationBuilder.DropTable(
                name: "TypeOfCertificates");

            migrationBuilder.DropTable(
                name: "OccupationalDiseases");

            migrationBuilder.DropTable(
                name: "HealthSurveillances");

            migrationBuilder.DropTable(
                name: "TypeOfAnalyses");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropIndex(
                name: "IX_Personnels_ProfessionId",
                table: "Personnels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeOfAccidents",
                table: "TypeOfAccidents");

            migrationBuilder.DropColumn(
                name: "TypeOfPlace",
                table: "Professions");

            migrationBuilder.DropColumn(
                name: "CertificateId",
                table: "Personnels");

            migrationBuilder.DropColumn(
                name: "InsuranceId",
                table: "Personnels");

            migrationBuilder.DropColumn(
                name: "ProfessionId",
                table: "Personnels");

            migrationBuilder.DropColumn(
                name: "TaskInstructionID",
                table: "Personnels");

            migrationBuilder.DropColumn(
                name: "TypeOfPlace",
                table: "Personnels");

            migrationBuilder.RenameTable(
                name: "TypeOfAccidents",
                newName: "TypeOfAccident");

            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "Personnels",
                newName: "Profession");

            migrationBuilder.RenameColumn(
                name: "RetiredId",
                table: "Personnels",
                newName: "Directorate");

            migrationBuilder.AlterColumn<string>(
                name: "TRIdNumber",
                table: "Personnels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "AccidentArea",
                table: "Accidents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeOfAccident",
                table: "TypeOfAccident",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AccidentAreas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccidentAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Directorates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directorates", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccidentAreas");

            migrationBuilder.DropTable(
                name: "Directorates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeOfAccident",
                table: "TypeOfAccident");

            migrationBuilder.DropColumn(
                name: "AccidentArea",
                table: "Accidents");

            migrationBuilder.RenameTable(
                name: "TypeOfAccident",
                newName: "TypeOfAccidents");

            migrationBuilder.RenameColumn(
                name: "Profession",
                table: "Personnels",
                newName: "Unit");

            migrationBuilder.RenameColumn(
                name: "Directorate",
                table: "Personnels",
                newName: "RetiredId");

            migrationBuilder.AddColumn<int>(
                name: "TypeOfPlace",
                table: "Professions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "TRIdNumber",
                table: "Personnels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CertificateId",
                table: "Personnels",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsuranceId",
                table: "Personnels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProfessionId",
                table: "Personnels",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TaskInstructionID",
                table: "Personnels",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfPlace",
                table: "Personnels",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeOfAccidents",
                table: "TypeOfAccidents",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ActualWageAndPersonnelNumbers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApprenticeActualWageNumber = table.Column<int>(type: "int", nullable: false),
                    ApprenticeNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternActualWageNumber = table.Column<int>(type: "int", nullable: false),
                    InternNumber = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfficerNumber = table.Column<int>(type: "int", nullable: false),
                    SalableProduction = table.Column<int>(type: "int", nullable: false),
                    TypeOfPlace = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WageEmployeeActualWageNumber = table.Column<int>(type: "int", nullable: false),
                    WagePersonNumber = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActualWageAndPersonnelNumbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonnelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TakenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeOfCertificateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificates_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthPersonnels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonnelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthPersonnels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthPersonnels_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthSurveillances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExaminationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeOfExaminationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthSurveillances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OccupationalDiseases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NotificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OnTheJobDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeOfDiseaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OccupationalDiseases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reasons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfPlace = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskInstructions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonnelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BornDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDateOfWork = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskInstructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskInstructions_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfAnalyses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfAnalyses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfEquipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfEquipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfWorkEquipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Period = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfWorkEquipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfCertificates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CertificateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfCertificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypeOfCertificates_Certificates_CertificateId",
                        column: x => x.CertificateId,
                        principalTable: "Certificates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TypeOfExaminations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HealthSurveillanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Period = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfExaminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypeOfExaminations_HealthSurveillances_HealthSurveillanceId",
                        column: x => x.HealthSurveillanceId,
                        principalTable: "HealthSurveillances",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TypeOfDiseases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccupationalDiseaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfDiseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypeOfDiseases_OccupationalDiseases_OccupationalDiseaseId",
                        column: x => x.OccupationalDiseaseId,
                        principalTable: "OccupationalDiseases",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SafetyEquipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeOfSafetyEquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GivenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyEquipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SafetyEquipments_TypeOfEquipments_TypeOfSafetyEquipmentId",
                        column: x => x.TypeOfSafetyEquipmentId,
                        principalTable: "TypeOfEquipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OHSComittees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hour = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OHSComittees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OHSComittees_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeriodicControls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeOfWorkEquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodicControls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeriodicControls_TypeOfWorkEquipments_TypeOfWorkEquipmentId",
                        column: x => x.TypeOfWorkEquipmentId,
                        principalTable: "TypeOfWorkEquipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeriodicControls_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkplaceTestAndAnalyses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeOfAnalysisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnalysisDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkplaceTestAndAnalyses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkplaceTestAndAnalyses_TypeOfAnalyses_TypeOfAnalysisId",
                        column: x => x.TypeOfAnalysisId,
                        principalTable: "TypeOfAnalyses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkplaceTestAndAnalyses_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SafetyExperts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeOfCertificateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HazardClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBidAssignment = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TRIdNumber = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkPlaceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyExperts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SafetyExperts_TypeOfCertificates_TypeOfCertificateId",
                        column: x => x.TypeOfCertificateId,
                        principalTable: "TypeOfCertificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SafetyExperts_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_ProfessionId",
                table: "Personnels",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_PersonnelId",
                table: "Certificates",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthPersonnels_PersonnelId",
                table: "HealthPersonnels",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_OHSComittees_UnitId",
                table: "OHSComittees",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodicControls_TypeOfWorkEquipmentId",
                table: "PeriodicControls",
                column: "TypeOfWorkEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodicControls_UnitId",
                table: "PeriodicControls",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyEquipments_TypeOfSafetyEquipmentId",
                table: "SafetyEquipments",
                column: "TypeOfSafetyEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyExperts_TypeOfCertificateId",
                table: "SafetyExperts",
                column: "TypeOfCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyExperts_UnitId",
                table: "SafetyExperts",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskInstructions_PersonnelId",
                table: "TaskInstructions",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfCertificates_CertificateId",
                table: "TypeOfCertificates",
                column: "CertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfDiseases_OccupationalDiseaseId",
                table: "TypeOfDiseases",
                column: "OccupationalDiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfExaminations_HealthSurveillanceId",
                table: "TypeOfExaminations",
                column: "HealthSurveillanceId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkplaceTestAndAnalyses_TypeOfAnalysisId",
                table: "WorkplaceTestAndAnalyses",
                column: "TypeOfAnalysisId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkplaceTestAndAnalyses_UnitId",
                table: "WorkplaceTestAndAnalyses",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personnels_Professions_ProfessionId",
                table: "Personnels",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id");
        }
    }
}

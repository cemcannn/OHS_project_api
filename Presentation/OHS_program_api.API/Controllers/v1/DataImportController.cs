using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OHS_program_api.Application.Consts;
using OHS_program_api.Application.CustomAttributes;
using OHS_program_api.Application.Enums;
using System.Diagnostics;
using System.Text;

namespace OHS_program_api.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class DataImportController : ControllerBase
    {
        // Excel dosyalarının (Veri.xlsx, Veri Yevmiye.xlsx) bulunduğu klasör
        private static readonly string BaseDir =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                         "OHS_Program");

        // import_data.py artık API projesi içinde: ExcelDataImport/Scripts/
        // BaseDirectory: bin/Debug/net6.0/ → 5 üst → OHS_project_api kökü
        private static readonly string ScriptPath =
            Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
                "..", "..", "..", "..", "..",
                "ExcelDataImport", "Scripts", "import_data.py"));

        // ─────────────────────────────────────────────────────────────
        // POST /api/v1/dataimport/veri
        // ─────────────────────────────────────────────────────────────
        [HttpPost("veri")]
        [AuthorizeDefinition(ActionType = ActionType.Writing,
                             Definition = "Import Veri",
                             Menu = AuthorizeDefinitionConstants.DataImport)]
        public async Task<IActionResult> ImportVeri(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { success = false, lines = new[] { "Dosya seçilmedi." } });

            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                return BadRequest(new { success = false, lines = new[] { "Sadece .xlsx dosyası kabul edilir." } });

            var targetPath = Path.Combine(BaseDir, "Veri.xlsx");
            await SaveFile(file, targetPath);

            var result = await RunScript("--mode veri");
            return Ok(result);
        }

        // ─────────────────────────────────────────────────────────────
        // POST /api/v1/dataimport/yevmiye
        // ─────────────────────────────────────────────────────────────
        [HttpPost("yevmiye")]
        [AuthorizeDefinition(ActionType = ActionType.Writing,
                             Definition = "Import Yevmiye",
                             Menu = AuthorizeDefinitionConstants.DataImport)]
        public async Task<IActionResult> ImportYevmiye(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { success = false, lines = new[] { "Dosya seçilmedi." } });

            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                return BadRequest(new { success = false, lines = new[] { "Sadece .xlsx dosyası kabul edilir." } });

            var targetPath = Path.Combine(BaseDir, "Veri Yevmiye.xlsx");
            await SaveFile(file, targetPath);

            var result = await RunScript("--mode yevmiye");
            return Ok(result);
        }

        // ─────────────────────────────────────────────────────────────
        // Yardımcılar
        // ─────────────────────────────────────────────────────────────
        private static async Task SaveFile(IFormFile file, string targetPath)
        {
            await using var stream = new FileStream(targetPath, FileMode.Create, FileAccess.Write);
            await file.CopyToAsync(stream);
        }

        private static async Task<object> RunScript(string modeArg)
        {
            var psi = new ProcessStartInfo
            {
                FileName               = "python3",
                Arguments              = $"\"{ScriptPath}\" {modeArg}",
                WorkingDirectory       = BaseDir,
                RedirectStandardOutput = true,
                RedirectStandardError  = true,
                UseShellExecute        = false,
                CreateNoWindow         = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding  = Encoding.UTF8,
            };

            var lines   = new List<string>();
            var success = true;

            try
            {
                using var process = new Process { StartInfo = psi };
                process.Start();

                var stdout = await process.StandardOutput.ReadToEndAsync();
                var stderr = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();

                if (!string.IsNullOrWhiteSpace(stdout))
                    lines.AddRange(stdout.Split('\n').Select(l => l.TrimEnd('\r')));

                if (!string.IsNullOrWhiteSpace(stderr))
                {
                    // Sadece DeprecationWarning / UserWarning değilse hata say
                    var errLines = stderr.Split('\n')
                        .Select(l => l.TrimEnd('\r'))
                        .Where(l => !string.IsNullOrWhiteSpace(l))
                        .ToList();

                    var realErrors = errLines.Where(l =>
                        !l.Contains("DeprecationWarning") &&
                        !l.Contains("UserWarning") &&
                        !l.Contains("warnings.warn")).ToList();

                    if (realErrors.Any())
                    {
                        success = false;
                        lines.AddRange(realErrors.Select(l => $"HATA: {l}"));
                    }
                }

                if (process.ExitCode != 0)
                    success = false;
            }
            catch (Exception ex)
            {
                success = false;
                lines.Add($"Script çalıştırılamadı: {ex.Message}");
            }

            return new { success, lines };
        }
    }
}

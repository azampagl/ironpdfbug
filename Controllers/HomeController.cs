using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IronPdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Set these variables.
            var rootFolder = Directory.GetCurrentDirectory();
            var inputFilePath = Path.GetFullPath(Path.Combine(rootFolder, "test.pdf"));
            ////IronPdf.License.LicenseKey = "IRONP........020";

            var fileContents = System.IO.File.ReadAllBytes(inputFilePath);
            var fileStream = new System.IO.MemoryStream(fileContents);

            var pdfDoc = new PdfDocument(fileStream);

            var outputFilePath = Path.Combine(rootFolder, Guid.NewGuid() + "-*.jpg");

            // Throws a "Exception of type 'System.ExecutionEngineException' was thrown." with no details. Sometimes this works and generates images.
            var outputImages = pdfDoc.RasterizeToImageFiles(outputFilePath, ImageType.Jpeg);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

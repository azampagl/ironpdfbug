using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var filePath = @"C:\.........\WebApplication1\example1.pdf";
            var licenseKey = "IRONP........020";

            var fileContents = System.IO.File.ReadAllBytes(filePath);
            var fileStream = new System.IO.MemoryStream(fileContents);

            var pdfDoc = new PdfDocument(fileStream);

            var valueToWatermark = "2020-07-28";
            IronPdf.License.LicenseKey = licenseKey;
            IronPdf.HtmlToPdf Renderer = new IronPdf.HtmlToPdf();
            Renderer.PrintOptions.InputEncoding = System.Text.Encoding.UTF8;

            try
            {
                pdfDoc.WatermarkAllPages($"<h3 style='padding-top:10px; font-family: Arial, sans-serif;'>#{valueToWatermark}</h2>", PdfDocument.WaterMarkLocation.TopRight, 100, 0);
            }
            catch (System.Exception e)
            {
                throw e;
            }            

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

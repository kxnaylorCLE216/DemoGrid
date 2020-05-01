using ExampleGrid.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.FileProviders;
using OfficeOpenXml;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Net.Http.Headers;

namespace ExampleGrid.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly DatabaseContext _context;
        private readonly long _fileSizeLimit;

        public HomeController(ILogger<HomeController> logger, DatabaseContext context, IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var fileName = ContentDispositionHeaderValue.Parse(
                            file.ContentDisposition).FileName.ToString().Trim('"');

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot",
                        fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            ImportSample();

            return RedirectToAction("Index");
        }

        // GET: Samples/Create
        public void ImportSample()
        {
            string rootFolder = _hostingEnvironment.WebRootPath;
            string fileName = @"samples.xlsx";
            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["Sample"];
                int totalRows = workSheet.Dimension.Rows;

                List<Sample> samplesList = new List<Sample>();

                for (int i = 2; i <= totalRows; i++)
                {
                    samplesList.Add(new Sample
                    {
                        Country = workSheet.Cells[i, 1].Value.ToString(),
                        Product = workSheet.Cells[i, 2].Value.ToString(),
                        DiscountBand = workSheet.Cells[i, 3].Value.ToString(),
                        UnitsSold = decimal.Parse(workSheet.Cells[i, 4].Value.ToString()),
                        ManufacturingPrice = decimal.Parse(workSheet.Cells[i, 5].Value.ToString()),
                        SalePrice = decimal.Parse(workSheet.Cells[i, 6].Value.ToString()),
                        GrossSales = decimal.Parse(workSheet.Cells[i, 7].Value.ToString()),
                        Discounts = decimal.Parse(workSheet.Cells[i, 8].Value.ToString()),
                        Sales = decimal.Parse(workSheet.Cells[i, 9].Value.ToString()),
                        COGS = decimal.Parse(workSheet.Cells[i, 10].Value.ToString()),
                        Profit = decimal.Parse(workSheet.Cells[i, 11].Value.ToString()),
                        Date = DateTime.Parse(workSheet.Cells[i, 12].Value.ToString()),
                        EnteredBy = workSheet.Cells[i, 13].Value.ToString()
                    });
                }

                _context.Samples.AddRange(samplesList);
                _context.SaveChanges();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
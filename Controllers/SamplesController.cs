using ExampleGrid.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleGrid
{
    public class SamplesController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly DatabaseContext _context;
        private readonly long _fileSizeLimit;

        public SamplesController(IHostingEnvironment hostingEnvironment, DatabaseContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            //_fileSizeLimit = config.GetValue<long>("FileSizeLimit");
        }

        // GET: Samples
        public async Task<IActionResult> Index()
        {
            return View(await _context.Samples.ToListAsync());
        }

        // GET: Samples/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sample = await _context.Samples
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sample == null)
            {
                return NotFound();
            }

            return View(sample);
        }

        // GET: Samples/Create
        public IList<Sample> ImportSample()
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

                return samplesList;
            }
        }

        // GET: Samples/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Upload()
        {
            return View();
        }

        // POST: Samples/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Country,Product,DiscountBand,UnitsSold,ManufacturingPrice,SalePrice,GrossSales,Discounts,Sales,COGS,Profit,Date,EnteredBy")] Sample sample)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sample);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sample);
        }

        // GET: Samples/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sample = await _context.Samples.FindAsync(id);
            if (sample == null)
            {
                return NotFound();
            }
            return View(sample);
        }

        // POST: Samples/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Country,Product,DiscountBand,UnitsSold,ManufacturingPrice,SalePrice,GrossSales,Discounts,Sales,COGS,Profit,Date,EnteredBy")] Sample sample)
        {
            if (id != sample.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sample);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SampleExists(sample.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sample);
        }

        // GET: Samples/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sample = await _context.Samples
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sample == null)
            {
                return NotFound();
            }

            return View(sample);
        }

        // POST: Samples/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sample = await _context.Samples.FindAsync(id);
            _context.Samples.Remove(sample);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SampleExists(int id)
        {
            return _context.Samples.Any(e => e.Id == id);
        }
    }
}
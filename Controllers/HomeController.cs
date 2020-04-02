using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ExampleGrid.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.Security.Claims;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExampleGrid.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Users()
        {
            var uses = new Models.Users();
            return View(uses.GetUsers());
        }

        public IActionResult Excel123()
        {
            var userid = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            System.Security.Claims.ClaimsPrincipal currentUser = this.User;

            var wat = ClaimTypes.GivenName;
            var watwat = User.FindFirst(ClaimTypes.GivenName).Value;

            //CreateSpreadsheetWorkbook("C:\\Users\\naylo\\Downloads\\New folder\\new.xlsx");
            Excel1();
            return View();
        }

        public static void CreateSpreadsheetWorkbook(string filepath)
        {
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.
                Create(filepath, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            var sheetData = new SheetData();
            // worksheetPart.Worksheet = new Worksheet(sheetData);
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            // Add Sheets to the Workbook.
            Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "mySheet"
            };

            // Open the document for editing.
            //using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(docName, true))
            //{
            //    // Insert other code here.
            //}

            sheets.Append(sheet);

            string cs = "URI=file:CustomerDB.db";
            string data = String.Empty;

            int i = 0;
            int j = 0;

            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();

                string stm = "SELECT * FROM CustomerTB";

                using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read()) // Reading Rows
                        {
                            Row newRow = new Row();
                            for (j = 0; j <= rdr.FieldCount - 1; j++) // Looping throw colums
                            {
                                data = rdr.GetValue(j).ToString();
                                //xlWorkSheet.Cells[i + 1, j + 1] = data;

                                Cell cell = new Cell();
                                cell.DataType = CellValues.String;
                                cell.CellValue = new CellValue(data);
                                newRow.AppendChild(cell);
                            }
                            sheetData.AppendChild(newRow);
                            i++;
                        }
                    }
                }
                con.Close();
            }

            workbookpart.Workbook.Save();

            // Close the document.
            spreadsheetDocument.Close();
        }

        public static void Excel1()
        {
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create("C:\\Users\\naylo\\Downloads\\New folder\\new.xlsx", SpreadsheetDocumentType.Workbook))
            {
                // Add a WorkbookPart to the document.
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();

                // Add a WorksheetPart to the WorkbookPart.
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);

                // Add Sheets to the Workbook.
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.
                    AppendChild<Sheets>(new Sheets());

                // Append a new worksheet and associate it with the workbook.
                Sheet sheet = new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Swag"
                };

                Row row = new Row() { RowIndex = 1 };
                Cell header1 = new Cell() { CellReference = "A1", CellValue = new CellValue("Interval Period Timestamp"), DataType = CellValues.String };
                row.Append(header1);
                Cell header2 = new Cell() { CellReference = "B1", CellValue = new CellValue("Settlement Interval"), DataType = CellValues.String };
                row.Append(header2);
                Cell header3 = new Cell() { CellReference = "C1", CellValue = new CellValue("Aggregated Consumption Factor"), DataType = CellValues.String };
                row.Append(header3);
                Cell header4 = new Cell() { CellReference = "D1", CellValue = new CellValue("Loss Adjusted Aggregated Consumption"), DataType = CellValues.String };
                row.Append(header4);

                sheetData.Append(row);

                sheets.Append(sheet);

                workbookpart.Workbook.Save();

                // Close the document.
                spreadsheetDocument.Close();
            }
        }

        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
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
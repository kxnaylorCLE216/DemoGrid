using System;
using System.ComponentModel.DataAnnotations;

namespace ExampleGrid.Models
{
    public class Sample
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string Product { get; set; }

        [Display(Name = "Discount Band")]
        public string DiscountBand { get; set; }

        [Display(Name = "Units Sold")]
        public int UnitsSold { get; set; }

        [Display(Name = "Manufacturing Price")]
        [DataType(DataType.Currency)]
        public decimal ManufacturingPrice { get; set; }

        [Display(Name = "Sale Price")]
        [DataType(DataType.Currency)]
        public decimal SalePrice { get; set; }

        [Display(Name = "Gross Sales")]
        [DataType(DataType.Currency)]
        public decimal GrossSales { get; set; }

        [DataType(DataType.Currency)]
        public decimal Discounts { get; set; }

        [DataType(DataType.Currency)]
        public decimal Sales { get; set; }

        [DataType(DataType.Currency)]
        public decimal COGS { get; set; }

        [DataType(DataType.Currency)]
        public decimal Profit { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Entered By")]
        public string EnteredBy { get; set; }
    }
}
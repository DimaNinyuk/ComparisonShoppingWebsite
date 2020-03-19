using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComparisonShoppingWebsite.Models
{
    public class ProductAmazon
    {
        public string ASIN { get; set; }
        public string title { get; set; }
        public string price { get; set; }
        public string listPrice { get; set; }
        public string imageUrl { get; set; }
        public string detailPageURL { get; set; }
        public string rating { get; set; }
        public string totalReviews { get; set; }
        public string subtitle { get; set; }
        public string isPrimeEligible { get; set; }
    }
}

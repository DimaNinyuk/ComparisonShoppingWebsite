using eBayReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComparisonShoppingWebsite.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Currentcy { get; set; }
        public string Url { get; set; }
        public string Imageurl { get; set; }
        public string Name { get; set; }
        public bool detailsenabled { get; set; }
    }
}

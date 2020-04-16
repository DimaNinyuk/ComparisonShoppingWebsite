using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComparisonShoppingWebsite.Models
{
    public class ProductAsos
    {
        public ProductAs[] Products { get; set; }
    }

    public class ProductAs
    {
        public int id { get; set; }
        public string name { get; set; }
        public Price price { get; set; }
        public string colour { get; set; }
        public int colourWayId { get; set; }
        public string brandName { get; set; }
        public bool hasVariantColours { get; set; }
        public bool hasMultiplePrices { get; set; }
        public int? groupId { get; set; }
        public int productCode { get; set; }
        public string productType { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
        public bool isSellingFast { get; set; }
    }
    public class Price
    {
        public Current current { get; set; }
        public Previous previous { get; set; }
        public Rrp rrp { get; set; }
        public bool isMarkedDown { get; set; }
        public bool isOutletPrice { get; set; }
        public string currency { get; set; }
    }
    public class Current
    {
        public double value { get; set; }
        public string text { get; set; }
    }

    public class Previous
    {
        public double? value { get; set; }
        public string text { get; set; }
    }

    public class Rrp
    {
        public double? value { get; set; }
        public string text { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ComparisonShoppingWebsite.Models
{
    
    public class ProductAmazon
    {

        public string Asin { get; set; }

      
        public string Title { get; set; }

        
        public string Price { get; set; }

        
        public string ListPrice { get; set; }

        
        public string ImageUrl { get; set; }


        public string DetailPageUrl { get; set; }

  
        public string Rating { get; set; }

    
 
        public string TotalReviews { get; set; }

 
        public string Subtitle { get; set; }

     
       
        public string IsPrimeEligible { get; set; }
    }
}

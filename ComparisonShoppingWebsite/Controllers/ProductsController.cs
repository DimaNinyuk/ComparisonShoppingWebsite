using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using ComparisonShoppingWebsite.Models;
using System.Collections.Generic;

namespace ComparisonShoppingWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        static readonly List<Product> data;
        static ProductsController()
        {
            data = new List<Product>();
        }


        [HttpGet, Route("getAmazon")]
        public IEnumerable<Product> GetAmazon(string keywords)
        {
            var client = new RestClient("https://amazon-price1.p.rapidapi.com/search?keywords=iphone&marketplace=ES");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "amazon-price1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "aceb22d176msh3d21a7602d172ffp194272jsn55c949386a6b");
            IRestResponse response = client.Execute(request);
            var newProd = JsonConvert.DeserializeObject <List<ProductAmazon>>(response.Content.ToString());
            
            foreach (var prod in newProd)
            {
                
                Product pr = new Product();
                pr.Id = prod.ASIN;
                pr.Title = prod.title;
                pr.Url = prod.detailPageURL;
                pr.Price = double.Parse(prod.price.Substring(4));
                pr.Currentcy = prod.price.Substring(0,3);
                data.Add(pr);
            }
            return data;
        }
        [HttpGet, Route("getAsos")]
        public IEnumerable<Product> GetAsos(string keywords)
        {
            var client = new RestClient("https://asos2.p.rapidapi.com/products/v2/list?country=US&currency=USD&sort=freshness&lang=en-US&q=skirt&sizeSchema=US&offset=0&categoryId=4209&limit=48&store=US");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "asos2.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "aceb22d176msh3d21a7602d172ffp194272jsn55c949386a6b");
            IRestResponse response = client.Execute(request);
            var newProd = JsonConvert.DeserializeObject<ProductAsos>(response.Content.ToString());
            foreach (var prod in newProd.Products)
            {
                Product pr = new Product();
                pr.Id = prod.id.ToString();
                pr.Title = prod.name;
                pr.Url = prod.url;
                pr.Price = prod.price.current.value;
                pr.Currentcy = prod.price.currency;
                data.Add(pr);
            }
            return data;
        }

    }
}
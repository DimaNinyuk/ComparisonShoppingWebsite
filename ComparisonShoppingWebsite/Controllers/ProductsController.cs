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


        [HttpGet, Route("get")]
        public IEnumerable<Product> GetAmazon(string keywords)
        {
            var client = new RestClient("https://amazon-price1.p.rapidapi.com/search?keywords="+keywords+"&marketplace=ES");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "amazon-price1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "aceb22d176msh3d21a7602d172ffp194272jsn55c949386a6b");
            IRestResponse response = client.Execute(request);
            var newProd = JsonConvert.DeserializeObject <List<ProductAmazon>>(response.Content.ToString());
            foreach (var prod in newProd)
            {
                Product pr = new Product();
                pr.Title = prod.title;
                pr.Url = prod.detailPageURL;
                pr.Price = double.Parse(prod.price.Remove(prod.price.IndexOf(' '), prod.price.IndexOf('.')));
                pr.Currentcy = prod.price.Remove(0, prod.price.IndexOf(' '));
                data.Add(pr);
            }
            return data;
        }
    }
}
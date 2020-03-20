using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using ComparisonShoppingWebsite.Models;
using eBayReference;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace ComparisonShoppingWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        public List<Product> data = new List<Product>();

        [HttpGet, Route("get")]
        public IEnumerable<Product> Get(string keywords)
        {
            data.Clear();
            GetEbay(keywords);
            //GetAsos(keywords);
            //GetAmazon(keywords);
            return data;
        }

        [HttpGet, Route("getebay")]
        public IEnumerable<Product> GetEbay(string keywords)
        {
            FindingServicePortTypeClient client = new FindingServicePortTypeClient();
            MessageHeader header = MessageHeader.CreateHeader("CustomHeader", "", "");
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                HttpRequestMessageProperty httpRequestProperty = new HttpRequestMessageProperty();
                httpRequestProperty.Headers.Add("X-EBAY-SOA-SECURITY-APPNAME", "XeniaK-comp-PRD-4b4e51644-6f63ec03");
                httpRequestProperty.Headers.Add("X-EBAY-SOA-OPERATION-NAME", "findItemsByKeywords");
                httpRequestProperty.Headers.Add("X-EBAY-SOA-GLOBAL-ID", "EBAY-US");
                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;

                //FindItemsByProductRequest request2 = new FindItemsByProductRequest();
                //request2.productId
                FindItemsByKeywordsRequest request = new FindItemsByKeywordsRequest();
                request.keywords = keywords;
                FindItemsByKeywordsResponse response = client.findItemsByKeywords(request);
                data.Clear();
                if (response.searchResult != null && response.searchResult.count > 0)
                    foreach (var item in response.searchResult.item)
                    {
                        var pr = new Product();
                        pr.Title = item.title;
                        pr.Url = item.viewItemURL;
                        pr.Price = item.sellingStatus.currentPrice.Value;
                        pr.Currentcy = item.sellingStatus.currentPrice.currencyId;
                        pr.Id = item.itemId;
                        pr.Imageurl = item.galleryURL;
                        pr.Name = "Ebay";
                        pr.detailsenabled = true;
                        data.Add(pr);
                    }
                return data;
            }
        }
        [HttpGet, Route("getamazon")]
        public IEnumerable<Product> GetAmazon(string keywords)
        {
            try
            {
                var client = new RestClient("https://amazon-price1.p.rapidapi.com/search?keywords=" + keywords + "&marketplace=ES");
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-rapidapi-host", "amazon-price1.p.rapidapi.com");
                request.AddHeader("x-rapidapi-key", "aceb22d176msh3d21a7602d172ffp194272jsn55c949386a6b");
                IRestResponse response = client.Execute(request);
                var newProd = JsonConvert.DeserializeObject<List<ProductAmazon>>(response.Content.ToString());
                if (newProd.Count > 0)

                    foreach (var prod in newProd)
                    {
                        Product pr = new Product();
                        pr.Title = prod.title;
                        pr.Url = prod.detailPageURL;
                        pr.Id = prod.ASIN;
                        pr.Price = double.Parse(prod.price.Substring(4));
                        pr.Currentcy = prod.price.Substring(0, 3);
                        pr.Imageurl = prod.imageUrl;
                        pr.Name = "Amazon";
                        pr.detailsenabled = true;
                        data.Add(pr);
                    }
            }
            catch(Exception e)
            {

            }
            return data;
        }

        [HttpGet, Route("getAsos")]
        public IEnumerable<Product> GetAsos(string keywords)
        {
            try
            {
                var client = new RestClient("https://asos2.p.rapidapi.com/products/v2/list?country=US&currency=USD&sort=freshness&lang=en-US&q=" + keywords + "&sizeSchema=US&offset=0&categoryId=4209&limit=48&store=US");
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-rapidapi-host", "asos2.p.rapidapi.com");
                request.AddHeader("x-rapidapi-key", "aceb22d176msh3d21a7602d172ffp194272jsn55c949386a6b");
                IRestResponse response = client.Execute(request);
                var newProd = JsonConvert.DeserializeObject<ProductAsos>(response.Content.ToString());
                if (newProd.Products.Count() > 0)

                    foreach (var prod in newProd.Products)
                    {
                        Product pr = new Product();
                        pr.Id = prod.id.ToString();
                        pr.Title = prod.name;
                        pr.Url = "https://www.asos.com/" + prod.url;
                        pr.Price = prod.price.current.value;
                        pr.Currentcy = prod.price.currency;
                        pr.Imageurl = "https://"+prod.imageUrl;
                        pr.Name = "Asos";
                        pr.detailsenabled = true;
                        data.Add(pr);
                    }
            }
            catch(Exception e)
            {

            }
            return data;
        }
    }
}
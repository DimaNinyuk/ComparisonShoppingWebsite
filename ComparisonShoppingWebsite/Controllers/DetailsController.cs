using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using ComparisonShoppingWebsite.Models;
using eBayReference;
using ebayService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace ComparisonShoppingWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsController : ControllerBase
    {
        public Product data;

        [HttpGet, Route("get")]
        public Product Get(string shop,string id)
        {
            data = new Product();
          
            if(shop=="Ebay")
            {
                EbayGet(id);
            }
            else if (shop == "Asos")
            {
                AsosGet(id);
            }
            else if (shop == "Amazon")
            {
                AmazonGet(id);
            }
            return data;
        }

        [HttpGet, Route("ebayget")]
        public Product EbayGet( string id)
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
                FindItemsByKeywordsRequest request = new FindItemsByKeywordsRequest();
                request.keywords = id;
                FindItemsByKeywordsResponse response = client.findItemsByKeywords(request);
                if (response.searchResult.count>0  )
                {
                    var pr = new Product();
                    pr.Title = response.searchResult.item[0].title;
                    pr.Url = response.searchResult.item[0].viewItemURL;
                    pr.Price = response.searchResult.item[0].sellingStatus.currentPrice.Value;
                    pr.Currentcy = response.searchResult.item[0].sellingStatus.currentPrice.currencyId;
                    pr.Id = response.searchResult.item[0].itemId;
                    pr.Imageurl = response.searchResult.item[0].pictureURLLarge;
                    pr.Name = "Ebay";
                    data = pr;
                }
                return data;
            }
        }

        [HttpGet, Route("amazonget")]
        public Product AmazonGet(string id)
        {
            try
            {
                var client = new RestClient("https://amazon-price1.p.rapidapi.com/search?keywords=" + id + "&marketplace=ES");
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-rapidapi-host", "amazon-price1.p.rapidapi.com");
                request.AddHeader("x-rapidapi-key", "aceb22d176msh3d21a7602d172ffp194272jsn55c949386a6b");
                IRestResponse response = client.Execute(request);
                var newProd = JsonConvert.DeserializeObject<List<ProductAmazon>>(response.Content.ToString());

                if (newProd.Count() > 0)
                {
                    Product pr = new Product();
                    pr.Title = newProd[0].title;
                    pr.Url = newProd[0].detailPageURL;
                    pr.Id = newProd[0].ASIN;
                    pr.Price = double.Parse(newProd[0].price.Substring(4));
                    pr.Currentcy = newProd[0].price.Substring(0, 3);
                    pr.Imageurl = newProd[0].imageUrl;
                    pr.Name = "Amazon";
                    pr.detailsenabled = true;
                    data = pr;
                }
            }
            catch(Exception e)
            {

            }
            return data;
        }

        [HttpGet, Route("asosget")]
        public Product AsosGet(string id)
        {
            try
            {
                var client = new RestClient("https://asos2.p.rapidapi.com/products/v2/list?country=US&currency=USD&sort=freshness&lang=en-US&q=" + id + "&sizeSchema=US&offset=0&categoryId=4209&limit=48&store=US");
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-rapidapi-host", "asos2.p.rapidapi.com");
                request.AddHeader("x-rapidapi-key", "aceb22d176msh3d21a7602d172ffp194272jsn55c949386a6b");
                IRestResponse response = client.Execute(request);
                var newProd = JsonConvert.DeserializeObject<ProductAsos>(response.Content.ToString());
                if (newProd.Products.Count() > 0)
                {
                    Product pr = new Product();
                    pr.Id = newProd.Products[0].id.ToString();
                    pr.Title = newProd.Products[0].name;
                    pr.Url = "https://www.asos.com/" + newProd.Products[0].url;
                    pr.Price = newProd.Products[0].price.current.value;
                    pr.Currentcy = newProd.Products[0].price.currency;
                    pr.Imageurl = "https://www.asos.com/" + newProd.Products[0].imageUrl;
                    pr.Name = "Asos";
                    pr.detailsenabled = true;
                    data = pr;
                }
            }
            catch (Exception e)
            {

            }
            return data;
        }
    }
}
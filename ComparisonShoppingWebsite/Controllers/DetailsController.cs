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
                if (response.searchResult != null)
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


        [HttpGet, Route("asosget")]
        public Product AsosGet(string id)
        {

            var client = new RestClient("https://asos2.p.rapidapi.com/products/detail?sizeSchema=US&store=US&lang=en-US&currency=USD&id=14292246");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "asos2.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "aceb22d176msh3d21a7602d172ffp194272jsn55c949386a6b");
            IRestResponse response = client.Execute(request);

                return data;
        }
    }
}
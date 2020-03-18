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

namespace ComparisonShoppingWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsEbayController : ControllerBase
    {

        static readonly List<Product> data;
        static ProductsEbayController()
        {
            data = new List<Product>();
        }
        [HttpGet, Route("get")]
        public IEnumerable<Product> Get(string keywords)
        {
            if (keywords == null)
                return null;

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
                request.keywords = keywords;
                FindItemsByKeywordsResponse response = client.findItemsByKeywords(request);
                data.Clear();
                if (response.searchResult != null)
                    foreach (var item in response.searchResult.item)
                    {
                        var pr = new Product();
                        pr.Title = item.title;
                        pr.Url = item.viewItemURL;
                        pr.Price = item.sellingStatus.currentPrice.Value;
                        pr.Currentcy = item.sellingStatus.currentPrice.currencyId;
                        data.Add(pr);
                    }

                return data;
            }

        }
    }
}
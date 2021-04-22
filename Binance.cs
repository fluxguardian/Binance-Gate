using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Karkov
{
    class Binance
    {
        Dictionary<string, string> MarketPlace { get; set; }
        //Dictionary<string, string> secondMarketPlace { get; set; }
        public HttpClient httpClient { get; set; }
        public string Token { get; set; } 
        public string SecretToken { get; set; } 
        public Binance(string token, string secretToken)
        {
            MarketPlace =
            new Dictionary<string, string>();

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("apiKey", token);
            httpClient.DefaultRequestHeaders.Add("secretKey", secretToken);
        }

        public async Task<JToken> GetRequest(string url)
        {
            var response = httpClient.GetStringAsync(url);
            var json = (JToken)JsonConvert.DeserializeObject(await response);
            
            return json;
        }

       
       
    }
}

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
        public HttpClient httpClient { get; set; }
        public string Token { get; set; } 
        public string SecretToken { get; set; } 
        private string KrakenURL { get; set; }
        public Binance(string token, string secretToken)
        {
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

        public void ParseAPI(object url, out string[] linkList, out string[] nameList)
        {
            int size = GetSize(url);
            int i = 0;

            linkList = new string[size];
            nameList = new string[size];

            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(url))
            {
                var name = descriptor.Name;

                if (name.EndsWith("BTC"))
                {
                    nameList[i] = descriptor.Name;
                    linkList[i] = $"https://api.kraken.com/0/public/Ticker?pair={name}";
                    i++;
                }
            }
        }
        public int GetSize(object url)
        {
            int size = 0;

            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(url))
            {
                if (descriptor.Name.EndsWith("XBT"))
                {
                    size++;
                }
            }

            return size;
        }
    }
}

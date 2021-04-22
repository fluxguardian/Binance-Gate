using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Collections.Generic;

namespace Karkov
{
    class Program
    {
        // gate
        static string gateURL = "https://data.gateapi.io/api2/1/marketlist";
        static string token = "d4uSixfPDpjVRkYi9UFhHOLHKlK7ASIfLgTzKvA1WwTrybE0xHHYIO8HpQ39uThZ";
        static string secretToken = "RiPVeO6lDQ4SDsQ3lTy8XxnAWlwhcZDHhGnIgipCBEsxuJDvwknXbCeiMG41Updf";
        // binance
        static string binanceUrl = "https://api.binance.com/api/v3/ticker/price";
        static string token2 = "WJSRMFM+Rx0ottw5jxNqKblOmp/2DGs5jWN+5YunvpzdCRYszqjnfX3b";
        static string secretToken2 = "2ahfGPscAHxQ+xNTrP+FVtpuoylYGkDtKr0zE//qgPn0jBOBbbzDueLWYDdh6VbmKGllUqqxFf1XAYdRf6wQ6Q==";
        static async Task Main(string[] args)
        {
            #region gate
            Binance BinanceUser = new Binance(token2, secretToken2);

            Dictionary<string, string> firstMarketPlace =
            new Dictionary<string, string>();

            var js = await BinanceUser.GetRequest(gateURL);
            var jsOld = js["data"];
            foreach (var item in jsOld)
            {
                if (item["pair"].ToString().EndsWith("btc")) 
                {
                    firstMarketPlace.Add(item["pair"].ToString().Replace("_", "").ToUpper(),
                    item["rate"].ToString());                   
                }
            }
            
            foreach (var v in  firstMarketPlace)
            {
                Console.WriteLine("Pars: " + v.Key);
                Console.WriteLine("Price:" + v.Value);
                Console.WriteLine();
            }
            #endregion

            #region binance
            Dictionary<string, string> secondMarketPlace =
            new Dictionary<string, string>();

            var js2 = await BinanceUser.GetRequest(binanceUrl);
            var jsOld2 = js2;

            foreach (var item in jsOld2)
            {
                if (item["symbol"].ToString().EndsWith("BTC"))
                {
                    secondMarketPlace.Add(item["symbol"].ToString(),
                    item["price"].ToString());
                }
            }

            foreach (var v in firstMarketPlace)
            {
                Console.WriteLine("Pars: " + v.Key);
                Console.WriteLine("Price:" + v.Value);
                Console.WriteLine();
            }

            #endregion

            Thread.Sleep(50000);
        }
    }
}

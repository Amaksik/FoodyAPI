using FoodyAPI.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;



namespace FoodyAPI.Clients
{
    public class BarCodeClient
    {
        private static string apiID;
        private static string apiKey;        
        private static readonly string url = @"https://trackapi.nutritionix.com/v2/";
 
        private static readonly string codeEndpoint = @"search/item?upc=";
        private static readonly string consumeEndpoint = @"natural/nutrients";
        
        private HttpClient httpClient;

        public BarCodeClient()
        {
            httpClient = new HttpClient();
            apiID = "f7edc5cb";
            apiKey = "35ada962ece3fcc17af2dde93ff1332a";
        }


        public async Task<Product> GetBarcodeInfo(string barcode)
        {

            
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            request.RequestUri = new Uri(url + codeEndpoint + barcode);
            request.Headers.Add("x-app-id", apiID);
            request.Headers.Add("x-app-key", apiKey);


            using (var response = await httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var body = await response.Content.ReadAsStringAsync();

                var foodlist = JsonSerializer.Deserialize<NutritionixResponse>(body);
                var respprod = foodlist.foods[0];

                var product = respprod.MakeProduct();
                return product;

            } 

        }


        public async Task<Product> Consume100Info(string name) 
        {
            string servings = $"100g of {name}";
            return await GetNaturalInfo(servings);

        }


        public async Task<Product> GetNaturalInfo(string query)
        {


            Query q = new Query(query);
            var json = JsonSerializer.Serialize(q);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("x-app-id", apiID);
            client.DefaultRequestHeaders.Add("x-app-key", apiKey);
            var response = await client.PostAsync(url + consumeEndpoint, data);

            string result = await response.Content.ReadAsStringAsync();

            var p = JsonSerializer.Deserialize<NutritionixResponse>(result);

            return p.foods[0].MakeProduct();
        }
    }
}

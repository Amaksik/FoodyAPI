using FoodyAPI.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FoodyAPI.Clients
{
    public class LogMealClient
    {
        private static readonly string url = "https://api.logmeal.es/v2/recognition/dish/v0.8?skip_types=%5B1%2C3%5D&language=eng";
        private static readonly string bearertoken = "a191bc0b178170db39a22225f5ba87d8e441641b";
        //private HttpClient httpClient;

        public LogMealClient() 
        {
            //httpClient = new HttpClient();
            
        }

        public LogMealClient(string methodtype)
        {

        }

        public async Task<Product> GetProductInfo(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                using (var img = Image.FromStream(memoryStream))
                {

                    PhotoResponse resend = new PhotoResponse();

                    resend =await MakeRequest(img);


                    Product p = new Product();
                    p.SetProduct(resend.recognition_results[0].name);
                    return p;
                }
            }
        }

        public static async Task<PhotoResponse> MakeRequest(Image img)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var requestContent = new MultipartFormDataContent();

                //ImageConverter Class convert Image object to Byte array.
                byte[] bytes = (byte[])new System.Drawing.ImageConverter().ConvertTo(img, typeof(byte[]));

                var imageContent = new ByteArrayContent(bytes);
                imageContent.Headers.ContentType =
                    MediaTypeHeaderValue.Parse("image/jpeg");

                client.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", bearertoken);


                requestContent.Add(imageContent, "image", "image.jpeg");
                var result = await client.PostAsync(url, requestContent);



                string resultContent = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<PhotoResponse>(resultContent);
            };
        }
    }
}

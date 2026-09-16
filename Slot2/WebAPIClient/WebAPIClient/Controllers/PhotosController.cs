using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebAPIClient.Models;

namespace WebAPIClient.Controllers
{
    public class PhotosController : Controller
    {
        static readonly HttpClient client = new HttpClient();
        private string getGivenAPIBaseUrl()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            string baseUrl = config["GivenAPIBaseUrl"];
            return baseUrl;
        }
        public async Task<IActionResult> Index()
        {
            string baseUrl = getGivenAPIBaseUrl();
            string uri = $"{baseUrl}/photos";
            string responseJson = "";
            List<Photo> photos = null;
            try { 
                HttpResponseMessage response = await client.GetAsync(uri);
                responseJson = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                photos = JsonSerializer.Deserialize<List<Photo>>(responseJson, options);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(model: photos);
        }
    }
}

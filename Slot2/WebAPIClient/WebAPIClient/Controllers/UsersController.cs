using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebAPIClient.Models;

namespace WebAPIClient.Controllers
{
    public class UsersController : Controller
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
            string uri = $"{baseUrl}/users";
            string responseJson = "";
            List<User> users = null;
            try {
                HttpResponseMessage response = await client.GetAsync(uri);
                responseJson = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                users = JsonSerializer.Deserialize<List<User>>(responseJson, options);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(model: users);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebAPIClient.Models;

namespace WebAPIClient.Controllers
{
    public class CommentsController : Controller
    {
        readonly HttpClient httpClient = new HttpClient();
        private string GivenBaseUrl()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            string baseUrl = config["GivenAPIBaseUrl"];
            return baseUrl;
        }
        public async Task<IActionResult> Index()
        {
            string baseUrl = GivenBaseUrl();
            string uri = $"{baseUrl}/comments";
            List<Comment> comments = null;
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(uri);
                string responseJson = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                comments = JsonSerializer.Deserialize<List<Comment>>(responseJson, jsonOptions);
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = $"Error fetching comments: {ex.Message}";
            }
            return View(model : comments);
        }
    }
}

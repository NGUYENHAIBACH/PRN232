using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Q2RazorPage.Models;

namespace Q2RazorPage.Pages.Ticket
{
    public class IndexModel : PageModel
    {
        static readonly HttpClient client = new HttpClient();

        public List<Models.Ticket> Tickets { get; set; } = new();
        public List<Movie> Movies { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int MovieId { get; set; } = 0;

        [BindProperty(SupportsGet = true)]
        public string AgeGroup { get; set; } = "All viewers";

        public async Task OnGetAsync()
        {
            string uriMovies = Utilities.GetAbsoluteUrl("/api/movies");
            string uriTickets = Utilities.GetAbsoluteUrl(
                $"/api/tickets/search?movieId={MovieId}&ageGroup={Uri.EscapeDataString(AgeGroup)}");
            string responseJson = "";
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    PropertyNameCaseInsensitive = true
                };

                // 1. Lấy danh sách phim
                HttpResponseMessage response = await client.GetAsync(uriMovies);
                Console.WriteLine($"Status: {response.StatusCode}");
                responseJson = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Data: {responseJson}");
                if (response.IsSuccessStatusCode)
                {
                    Movies = JsonSerializer.Deserialize<List<Movie>>(responseJson, options) ?? new();
                }

                // 2. Lấy danh sách vé
                response = await client.GetAsync(uriTickets);
                Console.WriteLine($"Status: {response.StatusCode}");
                responseJson = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Data: {responseJson}");
                if (response.IsSuccessStatusCode)
                {
                    Tickets = JsonSerializer.Deserialize<List<Models.Ticket>>(responseJson, options) ?? new();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

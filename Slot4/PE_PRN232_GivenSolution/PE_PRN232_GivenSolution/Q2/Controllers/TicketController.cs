using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Q2.Models;

public class TicketController : Controller
{
    static readonly HttpClient client = new HttpClient();
    private string GetGivenAPIBaseURL()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        string baseURL = config["GivenAPIBaseUrl"];
        return baseURL;
    }
    public async Task<IActionResult> Index(int movieId = 0, string ageGroup = "All viewers")
    {
        string apiBaseUrl = GetGivenAPIBaseURL() + "";
        string uriMovies = $"{apiBaseUrl}/api/movies";
        string uriTickets = $"{apiBaseUrl}/api/tickets/search?movieId={movieId}&ageGroup={Uri.EscapeDataString(ageGroup)}";
        string responseJson = "";
        List<Movie> movies = null;
        List<Ticket> tickets = null;
        try
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };

            // 1. Lấy danh sách phim
            HttpResponseMessage response
                = await client.GetAsync(uriMovies);
            Console.WriteLine($"Status: {response.StatusCode}");
            responseJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Data: {responseJson}");
            movies = JsonSerializer.Deserialize<List<Movie>>(responseJson, options);

            // 2. Lấy danh sách vé
            response = await client.GetAsync(uriTickets);
            Console.WriteLine($"Status: {response.StatusCode}");
            responseJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Data: {responseJson}");
            tickets = JsonSerializer.Deserialize<List<Ticket>>(responseJson, options);
            if (tickets != null)
            {
                foreach (Ticket ticket in tickets)
                {
                    Console.WriteLine($"Ticket: {ticket.TicketID} - {ticket.ViewerName} - {ticket.ViewerAge}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        ViewBag.Movies = movies ?? new List<Movie>();
        ViewBag.MovieId = movieId;
        ViewBag.AgeGroup = ageGroup;
        return View(tickets ?? new List<Ticket>());
    }
    [HttpGet("/Ticket/{id:int}")]
    public async Task<IActionResult> Detail(int id)
    {
        string apiBaseUrl = GetGivenAPIBaseURL() + "";
        string uri = $"{apiBaseUrl}/api/tickets/{id}";
        string responseJson = "";
        TicketDetail ticket = null;
        try
        {
            HttpResponseMessage response
                = await client.GetAsync(uri);
            Console.WriteLine($"Status: {response.StatusCode}");
            responseJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Data: {responseJson}");

            if (response.IsSuccessStatusCode)
            {
                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    PropertyNameCaseInsensitive = true
                };
                ticket = JsonSerializer.Deserialize<TicketDetail>(responseJson, options);
            }
            if (ticket != null)
            {
                Console.WriteLine($"Ticket: {ticket.TicketID} - {ticket.Email} - {ticket.ShowTime}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return View(ticket);
    }
}
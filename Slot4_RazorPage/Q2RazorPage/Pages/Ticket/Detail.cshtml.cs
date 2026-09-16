using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Q2RazorPage.Models;

namespace Q2RazorPage.Pages.Ticket
{
    public class DetailModel : PageModel
    {
        static readonly HttpClient client = new HttpClient();

        public TicketDetail? Ticket { get; set; }

        public async Task OnGetAsync(int id)
        {
            string uri = Utilities.GetAbsoluteUrl($"/api/tickets/{id}");
            string responseJson = "";
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
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
                    Ticket = JsonSerializer.Deserialize<TicketDetail>(responseJson, options);
                }
                if (Ticket != null)
                {
                    Console.WriteLine($"Ticket: {Ticket.TicketID} - {Ticket.Email} - {Ticket.ShowTime}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

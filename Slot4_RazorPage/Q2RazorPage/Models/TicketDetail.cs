using System.Text.Json.Serialization;

namespace Q2RazorPage.Models
{
    public class TicketDetail
    {
        public int TicketID { get; set; }

        [JsonPropertyName("viewerEmail")]
        public string Email { get; set; } = "";

        public DateTime ShowTime { get; set; }

        [JsonPropertyName("details")]
        public List<BookingItem> Bookings { get; set; } = new();
    }

    public class BookingItem
    {
        public int MovieID { get; set; }
        public string Title { get; set; } = "";
        public int Quantity { get; set; }
        public decimal BasePrice { get; set; }
    }
}

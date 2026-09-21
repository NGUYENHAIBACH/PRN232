namespace WebAPIService.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }

        public string? CustomerName { get; set; }

        public string? EmployeeName { get; set; }

        public string? OrderDate { get; set; }

        public decimal TotalAmount { get; set; }
    }
}

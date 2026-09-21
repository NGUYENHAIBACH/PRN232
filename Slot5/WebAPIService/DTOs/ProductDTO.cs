namespace WebAPIService.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;

        public string categoryName { get; set; } = null!;

     
        public string SupplierName { get; set; } = null!;

    }
}

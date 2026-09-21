using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIService.DTOs;
using WebAPIService.Models;

namespace WebAPIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly NorthwindContext dbContext;
        public ProductsController(NorthwindContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet("")]
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            var products = dbContext.Products
                .Include(productsp => productsp.Supplier)
                .Include(p => p.Category)
                .Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    categoryName = p.Category.CategoryName,
                    SupplierName = p.Supplier.CompanyName,
                   
                });
            return Ok(products);
        }
    }
}

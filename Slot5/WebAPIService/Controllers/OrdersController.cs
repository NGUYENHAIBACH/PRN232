using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIService.DTOs;
using WebAPIService.Models;

namespace WebAPIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly NorthwindContext dbContext;
        public OrdersController(NorthwindContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet("")]
        public ActionResult<IEnumerable<OrderDTO>> Get()
        {
            var orders = dbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.OrderDetails)
                .Select(o => new OrderDTO
                {
                    OrderId = o.OrderId,
                    CustomerName = o.Customer.CompanyName,
                    EmployeeName = o.Employee.FirstName + " " + o.Employee.LastName,
                    OrderDate = o.OrderDate.Value.ToString("yyyy-MM-dd"),
                    TotalAmount = o.OrderDetails.Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount))
                });

            return Ok(orders);
        }
    }
}

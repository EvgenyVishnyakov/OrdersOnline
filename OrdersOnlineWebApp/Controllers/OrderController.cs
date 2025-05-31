using Microsoft.AspNetCore.Mvc;
using OnlineOrder.Db.Models;
using OnlineOrderWebApp.Service;
using OrdersOnlineWebApp.DTO;

namespace OnlineOrderWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddAsync(List<ProductDto> productsDto)
        {
            var order = await _orderService.CreateAsync(productsDto);
            if (order == null)
                return BadRequest("Ошибка при создании заказа");

            return Ok(order);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> IndexAsync(Guid orderId)
        {
            var order = await _orderService.GetOrderAsync(orderId);
            if (order == null)
                return BadRequest("Ошибка при получении заказа");

            return Ok(order);
        }

        [HttpDelete("Remove")]
        public async Task<IActionResult> RemoveAsync(Guid orderId)
        {
            var result = await _orderService.DeleteAsync(orderId);
            if (result)
                return Ok();
            return BadRequest("Ошибка при удалении заказа");
        }

        [HttpPut("Put")]
        public async Task<IActionResult> UpdateAsync(Guid id, Status status, [FromBody] List<ProductDto> productsDto)
        {
            var order = await _orderService.UpdateAsync(id, status, productsDto);
            if (order == null)
                return BadRequest("Ошибка при получении заказа");

            return Ok(order);
        }
    }
}

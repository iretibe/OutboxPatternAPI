using Microsoft.AspNetCore.Mvc;
using OutboxPatternAPI.EmailOutboxService;
using OutboxPatternAPI.Models;
using OutboxPatternAPI.Services;

namespace OutboxPatternAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMailService _mailService;
        private readonly IEmailOutboxService _emailOutboxService;

        public OrderController(IOrderService orderService, IMailService mailService, IEmailOutboxService emailOutboxService)
        {
            _orderService = orderService;
            _mailService = mailService;
            _emailOutboxService = emailOutboxService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Order order)
        {
            var result = await _orderService.AddOrder(order);

            if (result is not null)
            {
                var send = _mailService.Send(result.Email, "Order is completed", "Your order has been saved in the database", false);

                if (send is true)
                {
                    return Ok(result);
                }
                else
                {
                    //Store in the email outbox
                    EmailOutbox emailOutbox = new EmailOutbox
                    {
                        OrderId = result.Id,
                        Success = false
                    };
                    var outbox = await _emailOutboxService.Add(emailOutbox);

                    return Ok(outbox);
                }
            }

            return BadRequest();
        }
    }
}

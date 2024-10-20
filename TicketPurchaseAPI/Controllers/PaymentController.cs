using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using TicketPurchaseAPI.Dto.Payment;
using TicketPurchaseAPI.Extensions;
using TicketPurchaseAPI.Model;

namespace TicketPurchaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;

        public PaymentController(IConfiguration config, UserManager<AppUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        
        [HttpPost("/checkout")]
        [Authorize]
        public async Task<IActionResult> Payment([FromBody] int amount )
        {

            var email = User.FindFirstValue(ClaimTypes.Email);

            var paymentDetail = new CheckoutPaymentDto
            {
                amount = amount,
                email = email
            };
            string json = JsonConvert.SerializeObject(paymentDetail);
            var content = new StringContent(json, Encoding.UTF8, "application/json");


            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.flutterwave.com/v3/payments");

            var response = client.PostAsync("checkout", content).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var deserializedResponse = JsonConvert.DeserializeObject(responseContent);
                return Ok(deserializedResponse);
            }
            else
            {
                return BadRequest(response.StatusCode);
            }
        }



    }
}
    
            


   

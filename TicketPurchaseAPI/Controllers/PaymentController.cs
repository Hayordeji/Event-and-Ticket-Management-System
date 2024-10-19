using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TicketPurchaseAPI.Dto.Payment;

namespace TicketPurchaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IConfiguration _config;
        public PaymentController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("/checkout")]
        public async Task<IActionResult> Payment([FromBody] CheckoutPaymentDto checkoutDto )
        {
            var paymentDetail = new CheckoutPaymentDto();
            string json = JsonConvert.SerializeObject(paymentDetail);
            var content = new StringContent(json,Encoding.UTF8, "application/json"); 
            var client = new HttpClient();
            var response = client.PostAsync("https://api.flutterwave.com/v3/payments", content).Result;
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
    
            


   

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

        public class PaymentRequest
        {
            public string tx_ref { get; set; }
            public string amount { get; set; }
            public string currency { get; set; }
            public string redirect_url { get; set; }
            public Customer customer { get; set; }
            public Customizations customizations { get; set; }
            public int session_duration {  get; set; }  
        }

        public class Customer
        {
            public string email { get; set; }
            public string name { get; set; }
            public string phonenumber { get; set; }
        }

        public class Customizations
        {
            public string title { get; set; }
        }

        [HttpPost("/checkout")]
        //[Authorize]
        public async Task<IActionResult> Payment([FromBody] int amount )
        {

            var request = new PaymentRequest
            {
                tx_ref = "UNIQUE_TRANSACTION_REFERENCE",
                amount = "7500",
                currency = "NGN",
                redirect_url = "https://example_company.com/success",
                session_duration = 150,
                customer = new Customer
                {
                    email = "developers@flutterwavego.com",
                    name = "Flutterwave Developers",
                    phonenumber = "09012345678"
                },
                customizations = new Customizations
                {
                    title = "Flutterwave Standard Payment"
                }
            };


            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            //var paymentDetail = new CheckoutPaymentDto
            //{
            //    tx_ref = Guid.NewGuid().ToString(),
            //    amount = amount,
            //    redirect_url = "www.googe.com",
            //    customer = new Customer
            //    {
            //        email = "ayd"
            //    }
            //};
            string json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");


            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "FLWSECK_TEST-4953327d0ba03586644dcfe455625469-X");
            //client.BaseAddress = new Uri("https://api.flutterwave.com/v3/payments");

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
    
            


   

﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using TicketPurchaseAPI.Dto.Payment;
using TicketPurchaseAPI.Extensions;
using TicketPurchaseAPI.Interface;
using TicketPurchaseAPI.Model;

namespace TicketPurchaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITicketRepository _ticketRepo;
        public PaymentController(IConfiguration config, UserManager<AppUser> userManager, ITicketRepository ticketRepo )
        {
            _config = config;
            _userManager = userManager;
            _ticketRepo = ticketRepo;
        }

       

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Checkout(int ticketId )
        {

            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var ticket = await _ticketRepo.GetTicketById(ticketId);
            if (ticket == null)
            {
                return NotFound();
            }
            var paymentDetail = new CheckoutPaymentDto
            {
                tx_ref = Guid.NewGuid().ToString(),
                amount = ((int)ticket.Price),
                currency = "NGN",
                redirect_url = "https://google.com",
                customer = new Customer
                {
                    email = userEmail
                }
            };
            string json = JsonConvert.SerializeObject(paymentDetail);
            string secretKey = _config["FlutterwaveSecretKey"];
            var content = new StringContent(json, Encoding.UTF8, "application/json");


            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", secretKey);
            

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
    
            


   

﻿using MarketplaceService.Application.Dtos.Paypal;
using MarketplaceService.Application.Interfaces;
using MarketplaceService.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaypalController : ControllerBase
    {
        private readonly IPaypalService paypalService;

        public PaypalController(IPaypalService paypalService)
        {
            this.paypalService = paypalService;
        }

        [HttpPost("create")]
        public IActionResult CreatePayment([FromBody]decimal amount)
        {
            CreatePaymentDto createPaymentDto = new CreatePaymentDto()
            {
                Amount=amount,
                Description="pays commande",
                CancelUrl= "https://28c7-105-73-98-11.ngrok-free.app/api/Paypal/cancel",
                ReturnUrl= "https://28c7-105-73-98-11.ngrok-free.app/api/Paypal/success"

            };
            var payment = paypalService.CreatePayment(createPaymentDto);

            return Ok(new { approvalUrl = payment.GetApprovalUrl() });
        }
        [HttpGet("success")]
        public async Task<IActionResult> Success([FromQuery] string paymentId, string token, string PayerID)
        {
            await Console.Out.WriteLineAsync("Payment Successful");
            // Handle success logic here
            return Ok("Payment Successful");
        }

        [HttpGet("cancel")]
        public async Task<IActionResult> Cancel()
        {
            // Handle cancel logic here
            await Console.Out.WriteLineAsync("Payment Cancelled");

            return Ok("Payment Cancelled");
        }
    }
}
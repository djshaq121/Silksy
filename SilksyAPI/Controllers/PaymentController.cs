using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SilksyAPI.Interface;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SilksyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;
        private readonly IUserRepository userRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IConfiguration configuration;

        public PaymentController(IPaymentService paymentService, IUserRepository userRepository,
            IShoppingCartRepository shoppingCartRepository, IConfiguration configuration)
        {
            this.paymentService = paymentService;
            this.userRepository = userRepository;
            this.shoppingCartRepository = shoppingCartRepository;
            this.configuration = configuration;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateCheckoutSession()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await userRepository.GetUserByUsernameAsync(username);

            var cart = await shoppingCartRepository.GetCartByUserIdAsync(user.Id);
            var SessionLineItems = paymentService.CreateItemsFromCart(cart);

            var options = new SessionCreateOptions
            {
                LineItems = SessionLineItems,
                Mode = "payment",
                SuccessUrl = "https://localhost:4200/checkout/success",
                CancelUrl = "https://localhost:4200/cancel",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            //Response.Headers.Add("Location", session.Url);
            return Ok(new { checkoutSessionUrl = session.Url });
        }

        [HttpGet("PublishKey")]
        public ActionResult GetStripePublishableKey()
        {
            return Ok(new { StripePublishKey = configuration.GetValue<string>("StripeSettings:PublishableKey") });
        }

        [HttpPost("PaymentIntent")]
        [Authorize]
        public async Task<ActionResult> CreatePaymentIntent()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await userRepository.GetUserByUsernameAsync(username);

            var cart = await shoppingCartRepository.GetCartByUserIdAsync(user.Id);

            var paymentIntentService = new PaymentIntentService();
            PaymentIntent paymentIntent;
            try
            {
                paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
                {
                    Amount = paymentService.CalculateOrderAmountFromCart(cart),
                    Currency = "gbp",
                    AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                    {
                        Enabled = true,
                    },
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = new { Message = ex.Message, } });
            }

            return Ok(new { clientSecret = paymentIntent.ClientSecret, paymentIntentId = paymentIntent.Id });
        }

        [HttpPost("Webhook")]
        public async Task<ActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            string endpointSecret = configuration.GetValue<string>("StripeSettings:WHSecret");
            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);
                var signatureHeader = Request.Headers["Stripe-Signature"];

                stripeEvent = EventUtility.ConstructEvent(json,
                        signatureHeader, endpointSecret);

                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    //await paymentService.HandlePaymentIntentSucceededAsync(paymentIntent.Id);
                }
                else if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    // await paymentService.HandlePaymentIntentFailedAsync(paymentIntent.Id);
                }
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }
                return Ok();
            }
            catch (StripeException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    
    }
}

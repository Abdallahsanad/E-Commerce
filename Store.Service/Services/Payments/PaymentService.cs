using Microsoft.Extensions.Configuration;
using Store.Core.Dtos.Basket;
using Store.Core.Entities;
using Store.Core.Entities.Order;
using Store.Core.Repositories.Contract;
using Store.Core.Services.Contract;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Store.Core.Entities.Product;


namespace Store.Service.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public PaymentService(IBasketService basketService,
            IUnitOfWork unitOfWork,
            IConfiguration configuration)
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntentIdAsync(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:Secretkey"];

            //get basket
            var basket=await _basketService.GetBasket(basketId);
            if (basket is null) return null;

            //amount
            var shipingPrice = 0m;

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value);
                shipingPrice = deliveryMethod.Cost;
            }

            if (basket.Items.Count() >0)
            {
                foreach (var item in basket.Items)
                {
                    var product=await _unitOfWork.Repository<Product, int>().GetByIdAsync(item.Id);
                    if (item.Price !=product.Price)
                    {
                        item.Price = product.Price;
                    }
                }
            }

            var supTotal = basket.Items.Sum(i => i.Price * i.Quantity);


            var service = new PaymentIntentService();

            PaymentIntent paymentIntent;


            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                //create
                var options = new PaymentIntentCreateOptions()
                {
                    //هضرب كل رقم في 100 عشان احول لسنت 
                    //وال amount ده من نوع long فمحتاج احول
                    Amount = (long)(shipingPrice * 100 + supTotal * 100),

                    PaymentMethodTypes=new List<string> { "card"},
                    Currency="USD"
                };
                paymentIntent =await service.CreateAsync(options);
                basket.PaymentIntentId=paymentIntent.Id;
                basket.ClientSecret=paymentIntent.ClientSecret;
            }

            else
            {
                //Update
                var options = new PaymentIntentUpdateOptions()
                {
                    //هضرب كل رقم في 100 عشان احول لسنت 
                    //وال amount ده من نوع long فمحتاج احول
                    Amount = (long)(shipingPrice * 100 + supTotal * 100),

                };
                paymentIntent = await service.UpdateAsync(basket.PaymentIntentId,options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }


            basket=await _basketService.CreateOrUpdateBaket(basket);
            if (basket is null) return null;
            return basket;

        }
    }
}

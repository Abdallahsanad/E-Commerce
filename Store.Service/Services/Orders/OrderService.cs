using Store.Core.Entities;
using Store.Core.Entities.Order;
using Store.Core.Repositories.Contract;
using Store.Core.Services.Contract;
using Store.Core.Specifications.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;

        public OrderService
            (
            IUnitOfWork unitOfWork,
            IBasketService basketService
            )
        {
            _unitOfWork = unitOfWork;
            _basketService = basketService;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            var basket =await _basketService.GetBasket(basketId);
            if (basket is null) return null;
            var orderItems=new List<OrderItem>();
            if (basket.Items.Count() > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product =await _unitOfWork.Repository<Product, int>().GetByIdAsync(item.Id);
                    if (product is null) return null;
                    var ProductItemOrder=new ProductItemOrder(product.Id,product.Name,product.PictureUrl);
                    var orderItem = new OrderItem(ProductItemOrder, product.Price, item.Quantity);
                    orderItems.Add(orderItem);
                }
            }
            var deliveryMethod=await _unitOfWork.Repository<DeliveryMethod,int>().GetByIdAsync(deliveryMethodId);
            var supTotal=orderItems.Sum(i=>i.Price * i.Quantity);
            var order = new Order(buyerEmail,shippingAddress, deliveryMethodId, deliveryMethod, orderItems, supTotal,"");
            await _unitOfWork.Repository<Order,int>().AddAsync(order);
            var result=await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;
            return order;
        }

        public async Task<Order?> GetOrderByIdForSpecificUserAsync(string buyerEmail, int orderId)
        {
            var spec=new OrderSpecifications(buyerEmail,orderId);
            var order=await _unitOfWork.Repository<Order,int>().GetByIdWithSpecAsync(spec);
            if (order is null) return null;
            return order;

        }

        public async Task<IEnumerable<Order>?> GetOrdersForSpecificUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecifications(buyerEmail);
            var orders = await _unitOfWork.Repository<Order, int>().GetAllWithSpecAsync(spec);
            if (orders is null) return null;
            return orders;
        }
    }
}

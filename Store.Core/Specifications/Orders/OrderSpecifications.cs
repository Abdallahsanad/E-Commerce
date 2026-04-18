using Store.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Specifications.Orders
{
    public class OrderSpecifications : BaseSpecifications<Order,int>
    {
        public OrderSpecifications(string buyerEmail,int orderId) : base(o=>o.BuyerEmail == buyerEmail && o.Id == orderId) 
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
        }

        public OrderSpecifications(string buyerEmail ) : base(o => o.BuyerEmail == buyerEmail )
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
        }


    }
}

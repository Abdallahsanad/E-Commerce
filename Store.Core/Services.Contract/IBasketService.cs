using Store.Core.Dtos.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Services.Contract
{
    public interface IBasketService
    {
        Task<CustomerBasketDto> GetBasket(string basketId);
        Task<CustomerBasketDto> CreateOrUpdateBaket(CustomerBasketDto basket);
        Task<bool> DeleteBaket(string basketId);
    }
}

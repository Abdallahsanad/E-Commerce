using AutoMapper;
using Store.Core.Dtos.Basket;
using Store.Core.Entities;
using Store.Core.Repositories.Contract;
using Store.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _repository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomerBasketDto> CreateOrUpdateBaket(CustomerBasketDto basket)
        {
            if (basket is null) return null;
            var mapper=_mapper.Map<CustomerBasket>(basket);
            var baskt =await _repository.UpdateBasketAsync(mapper);
            var baskermap=_mapper.Map<CustomerBasketDto>(baskt);
            if (baskermap is null) return null;
            return baskermap;
        }

        public async Task<bool> DeleteBaket(string basketId)
        {
            
           return await _repository.DeleteBasketAsync(basketId);


        }

        public async Task<CustomerBasketDto> GetBasket(string basketId)
        {
            if (basketId is null) return null;
            var basket = await _repository.GetBasketAsync(basketId);
            var mapper=_mapper.Map<CustomerBasketDto>(basket ?? new CustomerBasket { Id = basketId });
            return (mapper);

        }
    }
}

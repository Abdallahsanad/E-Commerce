using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Api.Errors;
using Store.Core.Entities;
using Store.Core.Repositories.Contract;
using System.Threading.Tasks;
using Store.Core.Dtos.Basket;
using Store.Core.Services.Contract;

namespace Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public BasketController(IBasketService basketService, IMapper mapper)
        {;
            _basketService = basketService;
            _mapper = mapper;
        }



        [ProducesResponseType(typeof(CustomerBasketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasketDto>> GetBasket(string? id)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var basket= await _basketService.GetBasket(id);
            return Ok(basket);

        }

        [ProducesResponseType(typeof(CustomerBasketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdate(CustomerBasketDto model)
        {
            if (model is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            var basket=await _basketService.CreateOrUpdateBaket(model);
            if (basket is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(basket);
        }


        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasket(string? id)
        {
            if (id is null)  BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var flag=await _basketService.DeleteBaket(id);
            if (flag is false)  BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return NoContent();
        }
    }
}

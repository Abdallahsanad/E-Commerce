using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using Store.Api.Errors;
using Store.Core.Dtos.Orders;
using Store.Core.Entities.Identity;
using Store.Core.Entities.Order;
using Store.Core.Repositories.Contract;
using Store.Core.Services.Contract;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IOrderService orderService,IMapper mapper,IUnitOfWork unitOfWork) 
        {
            _orderService = orderService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateOrder(OrderDto model)
        { 
            var userEmail=User.FindFirstValue(ClaimTypes.Email);
            if(userEmail is  null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            var address=_mapper.Map<Core.Entities.Order.Address>(model.ShipToAddress);
            var order =await _orderService.CreateOrderAsync(userEmail, model.BasketId, model.DeliveryMethodId, address);
            if (order is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(_mapper.Map<OrderReturnDto>(order));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrdersForSpecificUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            var orders= await _orderService.GetOrdersForSpecificUserAsync(userEmail);
            if (orders is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(_mapper.Map<IEnumerable<OrderReturnDto>>(orders));

        }

        [Authorize]
        [HttpGet("{OrderId}")]
        public async Task<IActionResult> GetOrder(int orderid)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            var order=await _orderService.GetOrderByIdForSpecificUserAsync(userEmail, orderid);
            if (order is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(_mapper.Map<OrderReturnDto>(order));

        }

        [HttpGet("DeliveryMethod")]
        public async Task<IActionResult> GetDeliveryMethod()
        {
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetAllAsync();
            if (deliveryMethod is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            return Ok(_mapper.Map<IEnumerable<DeliveryMethodDto>>(deliveryMethod));
        }


    }
}

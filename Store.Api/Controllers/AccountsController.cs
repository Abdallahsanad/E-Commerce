using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Api.Errors;
using Store.Api.Extensions;
using Store.Core.Dtos.Auth;
using Store.Core.Entities.Identity;
using Store.Core.Services.Contract;
using Store.Core.Services.Contract.Auth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        //mahmoud.dev.2026@gmail.com
        //Password123!
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public AccountsController
            (
            IUserService userService,
            UserManager<AppUser> userManager,
            ITokenService tokenService,
            IMapper mapper
            )
        {
            _userService = userService;
            _tokenService = tokenService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [ProducesResponseType(typeof(UserDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse),StatusCodes.Status401Unauthorized)]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user=await _userService.LoginAsync(loginDto);
            if (user is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized,"Invalid Login"));
            return Ok(user);
        }


        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _userService.RegisterAsync(registerDto);
            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid Registeratin"));
            return Ok(user);
        }

        [Authorize]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        { 
            var userEmail=User.FindFirstValue(ClaimTypes.Email);
            if(userEmail is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var user=await _userManager.FindByEmailAsync(userEmail);
            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email
            });
        }

        [Authorize]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("Address")]
        public async Task<ActionResult<AppUser>> GetAddressUser()
        {
            var user =await _userManager.FindByEmailWithAddressAsync(User);
            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(_mapper.Map<AddressDto>(user.Address));
        }


        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            if(address is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var user = await _userManager.FindByEmailWithAddressAsync(User);
            if (user is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            if (user.Address == null)
            {
                user.Address = _mapper.Map<Address>(address);
            }
            _mapper.Map(address, user.Address);

            var result =await _userManager.UpdateAsync(user);
            if(!result.Succeeded) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(_mapper.Map<AddressDto>(user.Address));

        }
    }
}

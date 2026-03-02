using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Api.Attributes;
using Store.Api.Errors;
using Store.Core.Dtos.Products;
using Store.Core.Services.Contract;
using Store.Service.Services;

namespace Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeService _typeService;

        public TypeController(ITypeService typeService)
        {
            _typeService = typeService;
        }


        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet]
        [Cached(5)]
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllTypes()
        {
            var result = await _typeService.GetAllTypesAsync();
            return Ok(result);
        }





        [ProducesResponseType(typeof(TypeBrandDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<TypeBrandDto>> AddType([FromForm] TypeBrandDto type)
        {
            if (type is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var result = await _typeService.AddTypeAsync(type);
            if (result is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(result);
        }

        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var flag = await _typeService.DeleteType(id);
            if (flag is false) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return NoContent();
        }

        [ProducesResponseType(typeof(TypeBrandDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public async Task<ActionResult<TypeBrandDto>> UpdateBrand(int? id, [FromForm] TypeBrandDto typeDto)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var type = await _typeService.UpdateType(id.Value, typeDto);
            if (type is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(type);
        }
    }
}

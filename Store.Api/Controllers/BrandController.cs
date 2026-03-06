using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Api.Attributes;
using Store.Api.Errors;
using Store.Core.Dtos.Products;
using Store.Core.Services.Contract;

namespace Store.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }



        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet]
        [Cached(5)]
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllBrands()
        {
            var result = await _brandService.GetAllBrandsAsync();
            return Ok(result);
        }

        [ProducesResponseType(typeof(TypeBrandDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<TypeBrandDto>> AddBrand([FromForm] TypeBrandDto brand)
        {
            if (brand is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var result = await _brandService.AddBrandAsync(brand);
            if (result is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(result);
        }

        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var flag=await _brandService.DeleteBrand(id);
            if (flag is false) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return NoContent();
        }


        [ProducesResponseType(typeof(TypeBrandDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public async Task<ActionResult<TypeBrandDto>> UpdateBrand(int? id, [FromForm]TypeBrandDto brandDto)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var brand= await _brandService.UpdateBrand(id.Value, brandDto);
            if (brand is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(brand);
        }
    }
}

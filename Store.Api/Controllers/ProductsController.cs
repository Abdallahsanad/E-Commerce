using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Api.Attributes;
using Store.Api.Errors;
using Store.Core.Dtos.Products;
using Store.Core.Helper;
using Store.Core.Services.Contract;
using Store.Core.Specifications.Products;
using Store.Service.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService=productService;
        }


        [ProducesResponseType(typeof(PaginationResponse<ProductsDto>),StatusCodes.Status200OK)]
        [HttpGet]
        [Cached(5)]
        public async Task<ActionResult<PaginationResponse<ProductsDto>>> GetAllProducts([FromQuery] ProductsSpecParams productsSpec)
        {
            var result= await _productService.GetAllProductsAsync(productsSpec);
            return Ok(result);
        }



        [ProducesResponseType(typeof(ProductsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
       
        public async Task<ActionResult<ProductsDto>> GetProductById(int? id)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var result = await _productService.GetProductByIdAsync(id.Value);
            if (result is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(result);
           
        }


        [ProducesResponseType(typeof(ProductsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<ProductsDto>> AddPrduct([FromForm] CreateProductsDto prouct)
        {
            if (prouct is null) return  BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var result=await _productService.AddProductAsync(prouct);
            if (result is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(result);
        }

        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var flag=await _productService.DeleteProductAsync(id);
            if (flag is false) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return NoContent();
        }


        [ProducesResponseType(typeof(ProductsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductsDto>> UpdateProduvt(int? id, [FromForm]CreateProductsDto productDto)
        {
            if(id is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var product= await _productService.UpdateProductAsync(id.Value, productDto);
            if (product is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(product);
        }
       
    }
}

using CRM_Lite.Application.DTO;
using CRM_Lite.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Lite.Controllers;

[ApiController]
[Route("api/v1/product")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromQuery] CreateProductDto dto)
    {
        var product = await _productService.CreateProduct(dto);
        return Ok(product);
    }
}
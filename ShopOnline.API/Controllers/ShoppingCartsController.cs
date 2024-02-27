using Microsoft.AspNetCore.Mvc;
using ShopOnline.API.Extensions;
using ShopOnline.API.Repositories.Interfaces;
using ShopOnline.Common.DTOs;

namespace ShopOnline.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShoppingCartsController : ControllerBase
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IProductRepository _productRepository;

    public ShoppingCartsController(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _productRepository = productRepository;
    }

    [HttpGet]
    [Route("{userId}/GetItems")]
    public async Task<IActionResult> GetItems(int userId)
    {
        try
        {
            var cartItems = await _shoppingCartRepository.GetItems(userId);

            if (cartItems == null)
            {
                return NoContent();
            }

            var products = await _productRepository.GetProducts();

            if (products == null)
            {
                throw new Exception("No products exists in the systems");
            }

            var cartItemDto = cartItems.ConvertToDto(products);
            return Ok(cartItemDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetItem(int id)
    {
        try
        {
            var cartItem = await _shoppingCartRepository.GetItem(id);
            if (cartItem == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetProduct(cartItem.ProductId);
            if (product == null)
            {
                return NotFound();
            }

            var cartItemDto = cartItem.ConvertToDto(product);
            return Ok(cartItemDto);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IActionResult> AddItem([FromBody] CreateCartItemDto model)
    {
        try
        {
            var newCartItem = await _shoppingCartRepository.AddItem(model);
            if (newCartItem == null)
            {
                return NoContent();
            }

            var product = await _productRepository.GetProduct(newCartItem.ProductId);
            if (product == null)
            {
                throw new Exception($"Something went wrong when attempting to retrieve product (productId:({model.ProductId})");
            }

            var newCartItemDto = newCartItem.ConvertToDto(product);
            return CreatedAtAction(nameof(GetItem), new { id = newCartItemDto.Id },newCartItemDto);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
        }
    }
}
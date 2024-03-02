using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using OnlineShop.Client.Services.Interfaces;
using ShopOnline.Common.DTOs;

namespace OnlineShop.Client.Services;

public class ShoppingCartService: IShoppingCartService
{
    private readonly HttpClient _httpClient;

    public ShoppingCartService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CartItemDto>?> GetItems(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/ShoppingCarts/{userId}/GetItems");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<CartItemDto>().ToList();
                }

                return await response.Content.ReadFromJsonAsync<List<CartItemDto>>();
            }
            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status:{response.StatusCode} Message -{message}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<CartItemDto?> AddItem(CreateCartItemDto model)
    {
        var response = await _httpClient.PostAsJsonAsync<CreateCartItemDto>("/api/ShoppingCarts", model);

        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return default(CartItemDto);
            }

            return await response.Content.ReadFromJsonAsync<CartItemDto>();
        }

        var message = await response.Content.ReadAsStringAsync();
        throw new Exception($"Http status:{response.StatusCode} Message -{message}");

    }

    public async Task<CartItemDto?> DeleteItem(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/ShoppingCarts/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CartItemDto>();
            }

            return default(CartItemDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<CartItemDto?> UpdateQty(UpdateCartItemQtyDto model)
    {
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");
            var response = await _httpClient.PatchAsync($"api/ShoppingCarts/{model.CartItemId}", content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CartItemDto>();
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

}
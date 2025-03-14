using ApiGateway.Model.Dto;
using ApiGateway.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

[Route("api/gateway")]
[ApiController]
public class GatewayController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IHttpClientFactory _httpClientFactory;
    public GatewayController(HttpClient httpClient, IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClient;
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost("auth/login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("http://localhost:2000/api/auth/login", request);
        if (!response.IsSuccessStatusCode)
        {
            return Unauthorized();
        }

        var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
        var token = result.Token;
        var refreshToken = result.RefreshToken;

        return Ok(new { Token = token, RefreshToken = refreshToken });
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetProducts([FromHeader(Name = "Authorization")] string authorization)
    {
        if (string.IsNullOrEmpty(authorization))
        {
            return BadRequest("Authorization token is required.");
        }

        // Bearer varsa ayrıştır, yoksa ekle
        string token = authorization.StartsWith("Bearer ") ? authorization.Substring(7) : authorization;

        using var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri("http://localhost:4000/");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync("api/Product");

        if (!response.IsSuccessStatusCode)
        {
            return Unauthorized("Invalid token or unauthorized access.");
        }

        var products = await response.Content.ReadFromJsonAsync<List<Product>>();

        return Ok(products);
    }
}


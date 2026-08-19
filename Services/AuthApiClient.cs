using System.Net;
using System.Net.Http.Json;
using TaskManager.Blazor.Models;

namespace TaskManager.Blazor.Services;

public class AuthApiClient
{
  private readonly IHttpClientFactory
      _httpClientFactory;


  public AuthApiClient(
      IHttpClientFactory httpClientFactory)
  {
    _httpClientFactory =
        httpClientFactory;
  }


  public async Task<AuthResponse?> LoginAsync(
      LoginRequest request,
      CancellationToken cancellationToken = default)
  {
    HttpClient httpClient =
        _httpClientFactory.CreateClient(
            "TaskManagerApi");


    HttpResponseMessage response =
        await httpClient.PostAsJsonAsync(
            "api/auth/login",
            request,
            cancellationToken);


    if (response.StatusCode ==
        HttpStatusCode.Unauthorized)
    {
      return null;
    }


    response.EnsureSuccessStatusCode();


    return await response.Content
        .ReadFromJsonAsync<AuthResponse>(
            cancellationToken);
  }


  public async Task<HttpResponseMessage>
      RegisterAsync(
          RegisterRequest request,
          CancellationToken cancellationToken = default)
  {
    HttpClient httpClient =
        _httpClientFactory.CreateClient(
            "TaskManagerApi");


    return await httpClient.PostAsJsonAsync(
        "api/auth/register",
        request,
        cancellationToken);
  }
}
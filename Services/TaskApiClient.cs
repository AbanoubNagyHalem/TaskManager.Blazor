using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaskManager.Blazor.Models;

namespace TaskManager.Blazor.Services;

public class TaskApiClient
{
    private readonly IHttpClientFactory
        _httpClientFactory;

    private readonly ApiRequestFactory
        _requestFactory;


    private static readonly JsonSerializerOptions
        JsonOptions =
            new(JsonSerializerDefaults.Web)
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                }
            };


    public TaskApiClient(
        IHttpClientFactory httpClientFactory,
        ApiRequestFactory requestFactory)
    {
        _httpClientFactory =
            httpClientFactory;

        _requestFactory =
            requestFactory;
    }


    public async Task<PagedResponse<TaskResponse>?>
        GetAllAsync(
            TaskQueryParameters parameters,
            CancellationToken cancellationToken = default)
    {
        HttpClient httpClient =
            _httpClientFactory.CreateClient(
                "TaskManagerApi");


        string url =
            "api/tasks" +
            $"?search={Uri.EscapeDataString(parameters.Search ?? "")}" +
            $"&sortBy={Uri.EscapeDataString(parameters.SortBy ?? "")}" +
            $"&sortDirection={Uri.EscapeDataString(parameters.SortDirection)}" +
            $"&page={parameters.Page}" +
            $"&pageSize={parameters.PageSize}";


        if (parameters.Status.HasValue)
        {
            url +=
                $"&status={parameters.Status.Value}";
        }


        if (parameters.Priority.HasValue)
        {
            url +=
                $"&priority={parameters.Priority.Value}";
        }


        using HttpRequestMessage request =
            _requestFactory.Create(
                HttpMethod.Get,
                url,
                requiresAuthentication:
                    true);


        HttpResponseMessage response =
            await httpClient.SendAsync(
                request,
                cancellationToken);


        response.EnsureSuccessStatusCode();


        return await response.Content
            .ReadFromJsonAsync<
                PagedResponse<TaskResponse>>(
                    JsonOptions,
                    cancellationToken);
    }


    public async Task<TaskDashboardResponse?>
        GetDashboardAsync(
            CancellationToken cancellationToken = default)
    {
        HttpClient httpClient =
            _httpClientFactory.CreateClient(
                "TaskManagerApi");


        using HttpRequestMessage request =
            _requestFactory.Create(
                HttpMethod.Get,
                "api/tasks/dashboard",
                requiresAuthentication:
                    true);


        HttpResponseMessage response =
            await httpClient.SendAsync(
                request,
                cancellationToken);


        response.EnsureSuccessStatusCode();


        return await response.Content
            .ReadFromJsonAsync<TaskDashboardResponse>(
                cancellationToken);
    }

    public async Task<TaskResponse?>
        GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
    {
        HttpClient httpClient =
            _httpClientFactory.CreateClient(
                "TaskManagerApi");


        using HttpRequestMessage request =
            _requestFactory.Create(
                HttpMethod.Get,
                $"api/tasks/{id}",
                requiresAuthentication:
                    true);


        HttpResponseMessage response =
            await httpClient.SendAsync(
                request,
                cancellationToken);


        if (response.StatusCode ==
            HttpStatusCode.NotFound)
        {
            return null;
        }


        response.EnsureSuccessStatusCode();


        return await response.Content
            .ReadFromJsonAsync<TaskResponse>(
                JsonOptions,
                cancellationToken);
    }


    public async Task<HttpResponseMessage>
        CreateAsync(
            CreateTaskRequest model,
            CancellationToken cancellationToken = default)
    {
        HttpClient httpClient =
            _httpClientFactory.CreateClient(
                "TaskManagerApi");


        using HttpRequestMessage request =
            _requestFactory.Create(
                HttpMethod.Post,
                "api/tasks",
                JsonContent.Create(
                    model,
                    options:
                        JsonOptions),
                requiresAuthentication:
                    true);


        return await httpClient.SendAsync(
            request,
            cancellationToken);
    }


    public async Task<HttpResponseMessage>
        UpdateAsync(
            int id,
            UpdateTaskRequest model,
            CancellationToken cancellationToken = default)
    {
        HttpClient httpClient =
            _httpClientFactory.CreateClient(
                "TaskManagerApi");


        using HttpRequestMessage request =
            _requestFactory.Create(
                HttpMethod.Put,
                $"api/tasks/{id}",
                JsonContent.Create(
                    model,
                    options:
                        JsonOptions),
                requiresAuthentication:
                    true);


        return await httpClient.SendAsync(
            request,
            cancellationToken);
    }


    public async Task<HttpResponseMessage>
        DeleteAsync(
            int id,
            CancellationToken cancellationToken = default)
    {
        HttpClient httpClient =
            _httpClientFactory.CreateClient(
                "TaskManagerApi");


        using HttpRequestMessage request =
            _requestFactory.Create(
                HttpMethod.Delete,
                $"api/tasks/{id}",
                requiresAuthentication:
                    true);


        return await httpClient.SendAsync(
            request,
            cancellationToken);
    }
}
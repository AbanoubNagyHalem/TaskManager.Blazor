using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using TaskManager.Blazor.Models;

namespace TaskManager.Blazor.Services;

public class AuthStateService
{
    private const string StorageKey =
        "task-manager-auth";


    private readonly ProtectedSessionStorage
        _sessionStorage;


    public AuthStateService(
        ProtectedSessionStorage sessionStorage)
    {
        _sessionStorage =
            sessionStorage;
    }


    public string? Token { get; private set; }


    public int? UserId { get; private set; }


    public string? Name { get; private set; }


    public string? Email { get; private set; }


    public string? Role { get; private set; }


    public bool IsAuthenticated =>
        !string.IsNullOrWhiteSpace(
            Token);


    public bool IsAdmin =>
        IsAuthenticated &&
        Role == "Admin";


    public event Action? OnChange;


    public async Task SetUserAsync(
        AuthResponse response)
    {
        Token =
            response.Token;

        UserId =
            response.UserId;

        Name =
            response.Name;

        Email =
            response.Email;

        Role =
            response.Role;


        await _sessionStorage.SetAsync(
            StorageKey,
            response);


        NotifyStateChanged();
    }


    public async Task<bool> LoadUserAsync()
    {
        ProtectedBrowserStorageResult<AuthResponse>
            result =
                await _sessionStorage
                    .GetAsync<AuthResponse>(
                        StorageKey);


        if (!result.Success ||
            result.Value is null)
        {
            ClearState();

            return false;
        }


        AuthResponse response =
            result.Value;


        Token =
            response.Token;

        UserId =
            response.UserId;

        Name =
            response.Name;

        Email =
            response.Email;

        Role =
            response.Role;


        NotifyStateChanged();


        return true;
    }


    public async Task LogoutAsync()
    {
        ClearState();


        await _sessionStorage.DeleteAsync(
            StorageKey);


        NotifyStateChanged();
    }


    private void ClearState()
    {
        Token =
            null;

        UserId =
            null;

        Name =
            null;

        Email =
            null;

        Role =
            null;
    }


    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}
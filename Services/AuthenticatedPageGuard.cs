using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace TaskManager.Blazor.Services;

public class AuthenticatedPageGuard
{
  private readonly AuthStateService
      _authState;

  private readonly AuthenticationStateProvider
      _authenticationStateProvider;

  private readonly NavigationManager
      _navigation;


  public AuthenticatedPageGuard(
      AuthStateService authState,
      AuthenticationStateProvider authenticationStateProvider,
      NavigationManager navigation)
  {
    _authState =
        authState;

    _authenticationStateProvider =
        authenticationStateProvider;

    _navigation =
        navigation;
  }


  public async Task<bool>
      EnsureAuthenticatedAsync()
  {
    await _authState.LoadUserAsync();


    AuthenticationState state =
        await _authenticationStateProvider
            .GetAuthenticationStateAsync();


    bool isAuthenticated =
        state.User.Identity
            ?.IsAuthenticated
        ?? false;


    if (!isAuthenticated)
    {
      _navigation.NavigateTo(
          "/login");

      return false;
    }


    return true;
  }
}
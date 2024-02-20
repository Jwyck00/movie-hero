using System.Security.Claims;
using System.Text.Encodings.Web;
using Application.Common.Error.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Security;

public class SimpleAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly AuthorizationSettings _jwtOptions;

    public SimpleAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IOptions<AuthorizationSettings> jwtOptions
    ) : base(options, logger, encoder, clock)
    {
        _jwtOptions = jwtOptions.Value;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        await Task.CompletedTask;

        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.Fail("Authorization Header not Found!");

        string authorizationHeader = Request.Headers["Authorization"];

        if (IsValidToken(authorizationHeader))
        {
            var claims = new List<Claim> { new(ClaimTypes.Name, "dummy_username") };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }

        return AuthenticateResult.Fail("Token is Invalid!");
    }

    protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        await Task.CompletedTask;
        throw new UnauthorizedException("Authorization Token is Invalid!");
    }

    private bool IsValidToken(string token)
    {
        return _jwtOptions.ApiToken == token;
    }
}

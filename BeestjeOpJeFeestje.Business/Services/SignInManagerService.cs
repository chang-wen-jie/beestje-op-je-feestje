using System.Security.Claims;
using BeestjeOpJeFeestje.Business.Interfaces;
using BeestjeOpJeFeestje.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BeestjeOpJeFeestje.Business.Services;

public class SignInManagerService(
    UserManager<Customer> userManager,
    IHttpContextAccessor contextAccessor,
    IUserClaimsPrincipalFactory<Customer> claimsFactory,
    IOptions<IdentityOptions> optionsAccessor,
    ILogger<SignInManager<Customer>> logger,
    IAuthenticationSchemeProvider schemes,
    IUserConfirmation<Customer> confirmation)
    : SignInManager<Customer>(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes,
        confirmation), ISignInManagerService
{
    public override async Task<ClaimsPrincipal> CreateUserPrincipalAsync(Customer user)
    {
        var principal = await base.CreateUserPrincipalAsync(user);
        var identity = (ClaimsIdentity)principal.Identity;

        if (identity == null) return principal;
        identity.AddClaim(new Claim("Name", user.Name ?? string.Empty));
        identity.AddClaim(new Claim("HouseNumber", user.HouseNumber.ToString() ?? "0"));
        identity.AddClaim(new Claim("ZipCode", user.ZipCode ?? string.Empty));
        identity.AddClaim(new Claim("EmailAddress", user.Email ?? string.Empty));
        identity.AddClaim(new Claim("PhoneNumber", user.PhoneNumber ?? string.Empty));
        Console.WriteLine($"TypeId: {user.TypeId}");
        identity.AddClaim(new Claim("TypeId", user.TypeId.ToString() ?? string.Empty));

        return principal;
    }
}
using System.Security.Claims;
using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Business.Interfaces;

public interface ISignInManagerService
{
    Task<ClaimsPrincipal> CreateUserPrincipalAsync(Customer user);
}
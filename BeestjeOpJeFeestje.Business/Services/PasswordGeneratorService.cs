using BeestjeOpJeFeestje.Business.Interfaces;
using BeestjeOpJeFeestje.Data.Interfaces;
using static System.Guid;

namespace BeestjeOpJeFeestje.Data.Repositories;

public class PasswordGeneratorService : IPasswordGeneratorService
{
    public string GeneratePassword()
    {
        return string.Concat(NewGuid().ToString("N").AsSpan(0, 12), "!");
    }
}
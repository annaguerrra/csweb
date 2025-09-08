using Microsoft.AspNetCore.Identity;

namespace MockTestCs.Services.Password;

public class PBKDF2PasswordServices : IPasswordServices
{
    readonly PasswordHasher<string> hasher = new();
    public bool Compare(string password, string hash)
    {
        var result = hasher.VerifyHashedPassword(password, hash, password);
        return result == PasswordVerificationResult.Success;
    }

    public string Hash(string password)
        => hasher.HashPassword(password, password);
}
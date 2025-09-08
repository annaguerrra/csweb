using Microsoft.EntityFrameworkCore;
using MockTestCs.Entities;
using MockTestCs.Services.JWT;
using MockTestCs.Services.Password;

namespace MockTestCs.Features.Login;

public class LoginUseCase(
    MockTestCsDbContext ctx, 
    IPasswordServices password,
    IJWTService jwt
)
{
    public async Task<Result<LoginResponse>> Do(LoginPayload payload)
    {
        var user = await ctx.Users
            .FirstOrDefaultAsync(u => u.Email == payload.Login || u.Username == payload.Login);

        var passwordMatch = password.Compare(payload.Password, user.Password);

        if (!passwordMatch || user is null)
            return Result<LoginResponse>.Fail("User not found");

        var jwtService = new EFJWTService();
        var token = jwtService.CreateToken(new UserToLoginDto(
            user.ID, user.Username
            )
        );

        await ctx.SaveChangesAsync();
        return Result<LoginResponse>.Success(new LoginResponse(token));
    }
}
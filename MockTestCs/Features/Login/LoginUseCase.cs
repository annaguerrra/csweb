using Microsoft.EntityFrameworkCore;
using MockTest.Entities;
using MockTestCs.Services.JWT;
using MockTestCs.Services.Login;
using MockTestCs.Services.Password;
namespace MockTestCs.Features.Login;

public class LoginUseCase(
    IJWTService jwt,
    MockTestDbContext ctx
)
{
    public async Task<Result<LoginResponse>> Do(LoginPayload payload)
    {
        var user = await ctx.Users.FirstOrDefaultAsync(
            u => u.Email == payload.Login || u.Username == payload.Login
            && u.Password == payload.Password
        );

        if (user is null)
            return Result<LoginResponse>.Fail("The login or password are incorrects.");

        var token = jwt.CreateToken(new LoginService
        {
            ID = user.ID,
            Email = user.Email,
            Username = user.Username
        });

        var response = new LoginResponse(token);
        return Result<LoginResponse>.Success(response);
    }
}
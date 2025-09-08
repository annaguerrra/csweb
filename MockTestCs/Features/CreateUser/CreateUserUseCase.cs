using Microsoft.EntityFrameworkCore;
using MockTestCs.Entities;
using MockTestCs.Services.Password;

namespace MockTestCs.Features.CreateUser;

public class CreateUserUseCase(
    MockTestCsDbContext ctx,
    IPasswordServices password
)
{
    public async Task<Result<CreateUserResponse>> Do(CreateUserPayload payload)
    {
        var exist = await ctx.Users
            .AnyAsync(u => u.Username == payload.Username || u.Email == payload.Email);

        if (exist)
            return Result<CreateUserResponse>.Fail("This login is already in use");
        
        var user = new User{
            Username = payload.Username,
            Email = payload.Email,
            Description = payload.Description,
            Password = password.Hash(payload.Password)
        };

        ctx.Users.Add(user);
        await ctx.SaveChangesAsync();

        return Result<CreateUserResponse>.Success( new CreateUserResponse(
                user.ID,
                user.Username,
                user.Email,
                user.Description
            )
        );
    }
}
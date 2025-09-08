using MockTestCs.Entities;

namespace MockTestCs.Features.CreateUser;

public class CreateUserUseCase(
    MockTestCsDbContext ctx
)
{
    public async Task<Result<CreateUserResponse>> Do(CreateUserPayload payload)
    {
        
    }

}
using MockTestCs.Services.Login;

namespace MockTestCs.Services.JWT;

public interface IJWTService
{
    string CreateToken(LoginService data);
}


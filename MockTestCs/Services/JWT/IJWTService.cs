namespace MockTestCs.Services.JWT;

public interface IJWTService
{
    string CreateToken(UserToLoginDto data);
}
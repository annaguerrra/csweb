namespace MockTestCs.Services.Password;

public interface IPasswordServices
{
    string Hash(string password);
    bool Compare(string password, string hash);
}
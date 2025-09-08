namespace MockTestCs.Services.Password;

public interface IPasswordServices
{
    string Hash(string password);
    string Compare(string password, string hash);
}
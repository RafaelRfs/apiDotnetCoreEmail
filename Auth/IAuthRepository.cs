using System.Threading.Tasks;

public interface IAuthRepository
{
    Task<ServiceResponse<int>> Register(User user, string password);
    Task<ServiceResponse<string>>  Login(string username, string password);

    Task<bool> userExists(string username);

}
using FloraSharedLibrary.DTOs;
using FloraSharedLibrary.Responses;
namespace FloraClient.Services
{
    public interface IUserAccountService
    {
        Task<ServiceResponse> Register(UserDTO model);
        Task<LoginResponse> Login(LoginDTO model);

    }
}

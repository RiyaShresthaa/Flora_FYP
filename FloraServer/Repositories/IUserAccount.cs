using FloraSharedLibrary.DTOs;
using FloraSharedLibrary.Responses;
namespace FloraServer.Repositories
{
    public interface IUserAccount
    {
        Task<ServiceResponse> Register(UserDTO model);
        Task<LoginResponse> Login(LoginDTO model);
        Task<UserSession> GetUserByToken(string token);
        Task<LoginResponse> GetRefreshToken(PostRefereshTokenDTO model);

    }
}

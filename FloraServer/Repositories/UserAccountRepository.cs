using FloraServer.Data;
using FloraSharedLibrary.DTOs;
using FloraSharedLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
namespace FloraServer.Repositories
{
    public class UserAccountRepository: IUserAccount
    {
        private readonly AppDbContext _appDbContext;

        public UserAccountRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Task<LoginResponse> GetRefreshToken(PostRefereshTokenDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<UserSession> GetUserByToken(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponse> Login(LoginDTO model)
        {
            if (model is null)
                return new LoginResponse(false, "Model is empty");

            var findUser = await _appDbContext.UserAccounts
                .FirstOrDefaultAsync(_ => _.Email!.Equals(model!.Email!));
            if (findUser is null)
                return new LoginResponse(false, "User not found");

            if (!BCrypt.Net.BCrypt.Verify(model!.Password, findUser.Password))
                return new LoginResponse(false, "Invalid Username/Password");

            var (accessToken, refreshToken) = await GenerateToken();
            //add or update Token info
            await SaveToTokenInfo(findUser.Id, accessToken, refreshToken);
            return new LoginResponse(true, "Login Successfull", accessToken, refreshToken);
        }

        private async Task SaveToTokenInfo(int userId, string accessToken, string refreshToken)
        {
            var getUser = await _appDbContext.TokenInfo.FirstOrDefaultAsync(_ => _.UserId == userId);
            if (getUser is null)
            {
                _appDbContext.TokenInfo.Add(new TokenInfo()
                { UserId=userId, AccessToken=accessToken, RefreshToken=refreshToken });
                await Commit();
            }
            else
            {
                getUser.RefreshToken = refreshToken;
                getUser.AccessToken = accessToken;  
                getUser.ExpiryDate = DateTime.Now.AddDays(1);
                await Commit();
            }
        }

        private async Task<(string AccessToken, string RefreshToken)> GenerateToken()
        {
            string accessToken = GenerateToken(256);
            string refreshToken = GenerateToken(64);

            while (!await VerifyToken(accessToken))
                accessToken= GenerateToken(256);
            while(!await VerifyToken(refreshToken))
                refreshToken=GenerateToken(256);
            return (accessToken, refreshToken);
        }
        private async Task<bool> VerifyToken(string refreshToken = null!, string accessToken = null!)
        {
            TokenInfo tokenInfo = new();
            if (!string.IsNullOrEmpty(refreshToken))
            {
                var getRefreshToken = await _appDbContext.TokenInfo
                    .FirstOrDefaultAsync(_ => _.RefreshToken!.Equals(refreshToken));
                return getRefreshToken is null ? true: false;
            }
            else
            {
                var getAccessToken = await _appDbContext.TokenInfo
                    .FirstOrDefaultAsync(_ => _.AccessToken!.Equals(accessToken));
                return getAccessToken is null;
            }
        }
        private static string GenerateToken(int numberOfBytes) =>
            Convert.ToBase64String(RandomNumberGenerator.GetBytes(numberOfBytes));
        public async Task<ServiceResponse> Register(UserDTO model)
        {
            var findUser = await _appDbContext.UserAccounts
                .FirstOrDefaultAsync(_ => _.Email!.ToLower().Equals(model.Email!.ToLower()));
            if (findUser is not null)
                return new ServiceResponse(false, "User Registered already");

            var user = _appDbContext.UserAccounts.Add(new UserAccount()
            {
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Name = model.Name,
                Email = model.Email
            }).Entity;
            await Commit();

            var checkIfAdminIsCreated = await _appDbContext.SystemRoles
                .FirstOrDefaultAsync(_ => _.Name!.ToLower().Equals("admin"));
            if (checkIfAdminIsCreated is null)
            {
                var result = _appDbContext.SystemRoles.Add(new SystemRole() { Name = "Admin" }).Entity;
                await Commit();

                _appDbContext.UserRoles.Add(new UserRole() { RoleId = result.Id, UserId = user.Id });
                await Commit();
            }
            else
            {
                var checkIfUserIsCreated = await _appDbContext.SystemRoles
                    .FirstOrDefaultAsync(_ => _.Name!.ToLower().Equals("user"));
                int RoleId = 0;
                if (checkIfUserIsCreated is null)
                {
                    var userResult = _appDbContext.SystemRoles.Add(new SystemRole() { Name = "User" }).Entity;
                    await Commit();
                    RoleId = userResult.Id;
                }

                _appDbContext.UserRoles.Add(new UserRole()
                {
                    RoleId = RoleId == 0 ? checkIfUserIsCreated!.Id : RoleId,
                    UserId = user.Id
                });
                await Commit();
            }
            return new ServiceResponse(true, "Account created");
        }

        private async Task Commit() => await _appDbContext.SaveChangesAsync();
    }
}

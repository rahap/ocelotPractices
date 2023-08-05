using OcelotCore.Model;

namespace AuthenticationApi.JwtService
{
    public interface IJWTManager
    {
        UserModel AuthenticateUser(UserModel model);
        string GenerateJSONWebToken(UserModel userInfo);
    }
}

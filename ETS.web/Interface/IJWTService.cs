using ETS.web.Model;

namespace ETS.web.Interface
{
    public interface IJWTService
    {
        public Task<ResponseModel> ValidateToken(string token);
        public Task<ResponseModel> GenerateToken(TokenRequestModel req);
    }
}

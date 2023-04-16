using ETS.web.Interface;
using ETS.web.Model;
using ETSystem.Interface;
using ETSystem.Model.User;
using ETSystem.Model;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ETS.web.Helper.JWT
{
    public class JWTService : IJWTService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public JWTService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<ResponseModel> GenerateToken(TokenRequestModel req)
        {
            var response = new ResponseModel();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var userdetail = _userRepository.LoginUser(connection, req);
            if (userdetail != null)
            {
                var claim = new List<Claim>();
                claim.Add(new Claim("UserId", userdetail.UserId.ToString()));
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var jwtsecurity = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                   claims: claim,
                   expires: DateTime.UtcNow.AddHours(24),
                   signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                   );
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.WriteToken(jwtsecurity);
                if (!string.IsNullOrEmpty(token))
                {
                    response.Result = new ETSystem.Model.Response { StatusCode = 0, StatusMessage = "Token Generated Successfully!" };
                    response.ResultObject = new TokenResponseModel() { Token = token, TokenExpiryTime = DateTime.UtcNow.AddHours(24) };
                }
                else
                {
                    response.Result = new ETSystem.Model.Response { StatusCode = 1, StatusMessage = "Unable to generate token!" };
                }
            }
            else
            {
                response.Result = new ETSystem.Model.Response { StatusCode = 1, StatusMessage = "User detail not found!" };
            }
            return response;
        }

        public async Task<ResponseModel> ValidateToken(string token)
        {
            var response = new ResponseModel();
            try
            {
                if(!string.IsNullOrEmpty(token))
                {
                    var tokenhandler = new JwtSecurityTokenHandler();
                    var paramreq = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"].ToString())),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = _configuration["JWT:ValidIssuer"],
                        ValidAudience = _configuration["JWT:ValidAudience"],
                        RoleClaimType = ClaimTypes.Role,
                        ClockSkew = TimeSpan.Zero,
                        ValidateLifetime = true
                    };
                    var principal = tokenhandler.ValidateToken(token, paramreq, out var securityToken);
                    if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                        !jwtSecurityToken.Header.Alg.Equals(
                            SecurityAlgorithms.HmacSha256,
                            StringComparison.InvariantCultureIgnoreCase))
                    {
                        response.Result = new ETSystem.Model.Response { StatusCode = 1, StatusMessage = "Invalid Token!" };
                    }
                    var userId = principal.Claims.First(x => x.Type == "UserId").Value;
                    SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
                    var userdetail = _userRepository.GetUserDetail(connection, userId);
                    if(userdetail != null)
                    {
                        response.Result = new ETSystem.Model.Response { StatusCode = 1, StatusMessage = "Valid Token!" };
                        response.ResultObject = userdetail;
                    }
                    else
                    {
                        response.Result = new ETSystem.Model.Response { StatusCode = 1, StatusMessage = "User detail not found!" };
                    }
                }
                else
                {
                    response.Result = new ETSystem.Model.Response { StatusCode = 1, StatusMessage = "Token cannot be empty!" };
                }
                return response;
            }
            catch(Exception ex)
            {
                response.Result = new ETSystem.Model.Response { StatusCode = 1, StatusMessage = ex.Message };
                return response;
            }
        }
    }
}

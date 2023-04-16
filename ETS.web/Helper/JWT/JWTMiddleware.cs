using ETS.web.Interface;
using ETSystem.Model.User;

namespace ETS.web.Helper.JWT
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IJWTService _jwtservice;

        public JWTMiddleware(RequestDelegate next, IConfiguration configuration, IJWTService jwtservice)
        {
            _next = next;
            _configuration = configuration;
            _jwtservice = jwtservice;
        }
        public async Task Invoke(HttpContext _context)
        {
            if (_context.Request.Path.ToString().Contains("GenerateToken"))
            {

            }
            else
            {
                var token = _context.Request.Headers["Token"];
                if(!string.IsNullOrEmpty(token))
                {
                    var res = await _jwtservice.ValidateToken(token);
                    if(res != null)
                    {
                        if(res.Result != null)
                        {
                            if(res.Result.StatusCode == 0)
                            {
                                if(res.ResultObject != null)
                                {
                                    var model = (User)res.ResultObject;
                                    _context.Items["UserId"] = model.UserId;
                                    _context.Items["EmailId"] = model.EmailId;
                                    _context.Items["Type"] = model.Type;
                                    _context.Items["StatusCode"] = 0;
                                    _context.Items["StatusMessage"] = "Valid Token";
                                }
                                else
                                {
                                    _context.Items["UserId"] = null;
                                    _context.Items["StatusCode"] = 1;
                                    _context.Items["StatusMessage"] = "Invalid Token!";
                                }
                            }
                            else
                            {
                                _context.Items["UserId"] = null;
                                _context.Items["StatusCode"] = res.Result.StatusCode;
                                _context.Items["StatusMessage"] = res.Result.StatusMessage;
                            }
                        }
                        else
                        {
                            _context.Items["UserId"] = null;
                            _context.Items["StatusCode"] = 1;
                            _context.Items["StatusMessage"] = "Invalid Token!";
                        }
                    }
                    else
                    {
                        _context.Items["UserId"] = null;
                        _context.Items["StatusCode"] = 1;
                        _context.Items["StatusMessage"] = "Invalid Token!";
                    }
                }
                else
                {
                    _context.Items["UserId"] = null;
                    _context.Items["StatusCode"] = 1;
                    _context.Items["StatusMessage"] = "Token cannot be empty!";
                }
            }
        }
    }
}

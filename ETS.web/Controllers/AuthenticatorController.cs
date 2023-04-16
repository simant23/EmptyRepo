using ETS.web.Interface;
using ETS.web.Model;
using ETSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETS.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticatorController : ControllerBase
    {
        private readonly IJWTService _jwtservice;
        public AuthenticatorController(IJWTService jwtservice)
        {
            _jwtservice = jwtservice;
        }
        [HttpPost]
        [Route("GetToken")]
        public async Task<IActionResult> GetToken(TokenRequestModel model)
        {
            var response = new ResponseModel();
            try
            {
                if (string.IsNullOrEmpty(model.Email))
                {
                    ModelState.AddModelError("Email", "Email is required");
                }
                if (string.IsNullOrEmpty(model.Password))
                {
                    ModelState.AddModelError("Password", "Password is required");
                }
                if (string.IsNullOrEmpty(model.Type))
                {
                    ModelState.AddModelError("Type", "Type is required");
                }
                if(ModelState.IsValid)
                {
                    var res = await _jwtservice.GenerateToken(model);
                    if(res != null)
                    {
                        response.Result = res.Result;
                        response.ResultObject = res.ResultObject;
                        return new ObjectResult(response) { StatusCode = 200 };
                    }
                    else
                    {
                        response.Result = new Response() { StatusCode = 1, StatusMessage = "Cannot generate token!" };
                        return new ObjectResult(response) { StatusCode = 400 };
                    }
                }
                else
                {
                    var errors = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    response.Result = new Response() { StatusCode = 1, StatusMessage = errors };
                    return new ObjectResult(response) { StatusCode = 400 };
                }
            }
            catch (Exception ex)
            {
                response.Result = new Response() { StatusCode = 1, StatusMessage = ex.Message };
                return new ObjectResult(response) { StatusCode = 400 };
            }
        }
    }
}

using ETS.web.Helper.Attributes;
using ETSystem.Interface;
using ETSystem.Model.Notice;
using ETSystem.Model.User;
using ETSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ETS.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [JWTTokenAttribute]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        [HttpGet]
        [Route("GetAll")]
        public ActionResult<List<User>> GetAll()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var user1 = _userRepository.GetAll(connection);
            return Ok(user1);
        }

    }
}

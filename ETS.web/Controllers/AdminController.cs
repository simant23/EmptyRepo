using ETS.web.Helper.Attributes;
using ETSystem.Model.User;
using ETSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ETS.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [JWTTokenAttribute]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IConfiguration _configuration;
        public AdminController(IAdminRepository adminRepository, IConfiguration configuration)
        {
            _adminRepository = adminRepository;
            _configuration = configuration;
        }



        [HttpPut("AdminDetails")]
        public IActionResult UpdateAdmin(Admin admin)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _adminRepository.UpdateAdmin(admin, connection);
            return Ok();
        }
    }
}

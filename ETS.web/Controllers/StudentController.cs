using ETS.web.Helper.Attributes;
using ETSystem.Model.Notice;
using ETSystem.Model.User;
using ETSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace ETS.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [JWTTokenAttribute]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IConfiguration _configuration;
        public StudentController(IStudentRepository studentRepository, IConfiguration configuration)
        {
            _studentRepository = studentRepository;
            _configuration = configuration;
        }



        [HttpPut("Update")]
        public IActionResult UpdateStudent(Student student)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _studentRepository.UpdateStudent(student, connection);
            return Ok();
        }
    }
}

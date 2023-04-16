using ETS.web.Helper.Attributes;
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
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IConfiguration _configuration;
        public TeacherController(ITeacherRepository teacherRepository, IConfiguration configuration)
        {
            _teacherRepository = teacherRepository;
            _configuration = configuration;
        }



        [HttpPut]
        public IActionResult UpdateTeacher(Teacher teacher)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _teacherRepository.UpdateTeacher(teacher, connection);
            return Ok();
        }
    }

}

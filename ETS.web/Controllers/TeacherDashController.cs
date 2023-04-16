using ETS.web.DAL;
using ETS.web.Helper.Attributes;
using ETS.web.Interface;
using ETS.web.Model.TeacherDash;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ETS.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [JWTTokenAttribute]
    public class TeacherDashController : ControllerBase
    {
        private readonly ITeacherDashRepoitory _teaherDashRepository;
        private readonly IConfiguration _configuration;
        public TeacherDashController(ITeacherDashRepoitory teaherDashRepository, IConfiguration configuration)
        {
            _teaherDashRepository = teaherDashRepository;
            _configuration = configuration;
        }

        [HttpGet("Total-ExamCreated")]
        public ActionResult<TeacherTExam> TotalExamCreated(int UserId)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test1 = _teaherDashRepository.TotalExamCreated(UserId, connection);
            return Ok(test1);
        }

    }
}

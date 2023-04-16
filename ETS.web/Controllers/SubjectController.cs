using ETS.web.Helper.Attributes;
using ETSystem.Model.Notice;
using ETSystem.Model.QuestionPaper;
using ETSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ETS.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [JWTTokenAttribute]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IConfiguration _configuration;

        public SubjectController(ISubjectRepository subjectRepository, IConfiguration configuration)
        {
            _subjectRepository = subjectRepository;
            _configuration = configuration;
        }

        [HttpGet("{SubjectName}")]
        public ActionResult<Subject> GetBySubject(string SubjectName)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var subject = _subjectRepository.GetBySubject(SubjectName, connection);

            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }
    }
}

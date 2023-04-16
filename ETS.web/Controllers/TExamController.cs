using ETS.web.DAL;
using ETS.web.Helper.Attributes;
using ETS.web.Model.TExam;
using ETSystem.Interface;
using ETSystem.Model.Message;
using ETSystem.Model.Notice;
using ETSystem.Model.TExam;
using ETSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ETS.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [JWTTokenAttribute]
    public class TExamController : ControllerBase
    {
        private readonly ITExamRepository _examRepository;
        private readonly IConfiguration _configuration;
        public TExamController(ITExamRepository examRepository, IConfiguration configuration)
        {
            _examRepository = examRepository;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Create(TeacherExam teacherExam)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _examRepository.Create(teacherExam, connection);
            return Ok();
        }

        [HttpPut("UpdateExam")]
        public IActionResult UpdteExam(UpdateExam updateExam)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _examRepository.UpdateExam(updateExam, connection);
            return Ok();
        }

        [HttpGet("{Class}")]
        public ActionResult<List<ViewExam>> GetByClass(int Class)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con")))
            {
                List<ViewExam> viewExams = _examRepository.GetByClass(Class, connection);

                if (viewExams.Count == 0)
                {
                    return NotFound();
                }

                return Ok(viewExams);
            }
        }

        [HttpGet("TeacherView")]
        public ActionResult<List<ViewExam>> GetAll()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _examRepository.GetAll(connection);
            return Ok(test);
        }


        [HttpDelete("{IExamId}")]
        public IActionResult Delete(int IExamId)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _examRepository.Delete(IExamId, connection);

            return Ok(test);

        }

        [HttpPut("UpdateExamStatus")]
        public IActionResult UpdateExamStatus(int IExamId)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _examRepository.UpdateExamStatus(IExamId, connection);
            return Ok();
        }

        [HttpPut("UpdateExamStatusFalse")]
        public IActionResult UpdateExamStatusFalse(int IExamId)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _examRepository.UpdateExamStatusFalse(IExamId, connection);
            return Ok();
        }

    }
}

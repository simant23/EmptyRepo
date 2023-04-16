using ETS.web.Helper.Attributes;
using ETSystem.Model.Notice;
using ETSystem.Model.QuestionPaper;
using ETSystem.Model.SampleQuestion;
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
    public class QuestionPaperController : ControllerBase
    {
        private readonly IQuestionPaperRepository _questionPaperRepository;
        private readonly IConfiguration _configuration;
        public QuestionPaperController(IQuestionPaperRepository questionPaperRepository, IConfiguration configuration)
        {
            _questionPaperRepository = questionPaperRepository;
            _configuration = configuration;
        }


        [HttpPost]
        public IActionResult CreatePaper(QuestionPaper questionPaper)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _questionPaperRepository.CreatePaper(questionPaper, connection);
            return Ok(test);
        }

        [HttpGet("{Class}")]
        public ActionResult<List<ViewPaper>> ViewPaperById(int Class)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var viewPaper = _questionPaperRepository.ViewPaperById(Class, connection);

            if (viewPaper == null)
            {
                return NotFound();
            }

            return Ok(viewPaper);
        }

        [HttpGet]
        public ActionResult<List<ViewPaper>> GetAll()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _questionPaperRepository.GetAll(connection);
            return Ok(test);
        }

        [HttpDelete("{PaperId}")]
        public IActionResult DeletePaper(int PaperId)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var delete = _questionPaperRepository.DeletePaper(PaperId, connection);

            return Ok(delete);

        }

        [HttpPut("UpdatePaper")]
        public IActionResult UpdatePaper(Paper paper)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _questionPaperRepository.UpdatePaper(paper, connection);
            return Ok();
        }



    }
}

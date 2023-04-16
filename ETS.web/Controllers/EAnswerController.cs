using ETS.web.Helper.Attributes;
using ETS.web.Interface;
using ETS.web.Model.EAnswer;
using ETSystem.Model.Notice;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace ETS.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [JWTTokenAttribute]
    public class EAnswerController : ControllerBase
    {
        private readonly IEAnswerRepository _eAnswerRepository;
        private readonly IConfiguration _configuration;
        public EAnswerController(IEAnswerRepository eAnswerRepository, IConfiguration configuration)
        {
            _eAnswerRepository = eAnswerRepository;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("CreateAnswer")]
        public IActionResult Create(CreateAnswer createAnswer)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _eAnswerRepository.Create(createAnswer, connection);
            return Ok(test);
            //return createdataction(nameof(getbyid), new { id = notice.noticeid }, notice);
        }

        [HttpGet]
        [Route("GetById")]
        public ActionResult<List<ViewAnswer>> GetById(int IExamId, int UserId)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con")))
            {
                List<ViewAnswer> list = _eAnswerRepository.GetById( IExamId, UserId, connection);

                if (list.Count == 0)
                {
                    return NotFound();
                }

                return Ok(list);
            }
        }

        //[HttpGet("Checkanswer")]
        //public ActionResult<Check> CheckAnswer(int UserId, int EQuestionId)
        //{
        //    SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("con").ToString());
        //    var answerexists = _eAnswerRepository.CheckAnswer(UserId, EQuestionId, connection);
        //    return Ok(answerexists);
        //}

    }
}

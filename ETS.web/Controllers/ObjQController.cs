using ETS.web.Helper.Attributes;
using ETS.web.Model.Question;
using ETSystem.Model.Notice;
using ETSystem.Model.Question;
using ETSystem.Model.SampleQuestion;
using ETSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ETS.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [JWTTokenAttribute]
    public class ObjQController : ControllerBase
    {
        private readonly IObjQRepository _objRepository;
        private readonly IConfiguration _configuration;
        public ObjQController(IObjQRepository objRepository, IConfiguration configuration)
        {
            _objRepository = objRepository;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("CreateQuestion")]
        public IActionResult Create(ObjQ objective)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _objRepository.Create(objective, connection);

            return Ok(test);
        }

        [HttpGet]
        [Route("ViewQuestions")]
        public ActionResult<List<ViewQuestions>> ViewQuestionsById(int PaperId)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _objRepository.ViewQuestionsById(PaperId, connection);
            return Ok(test);
        }


        [HttpPut]
        [Route("UpdateQuestion")]
        public IActionResult UpdateQ(UpdateQ obj)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("con").ToString());
            var test = _objRepository.UpdateQ(obj, connection);
            return Ok(test);
        }



        [HttpDelete("{QuestionId}")]
        public IActionResult DeleteQn(int QuestionId)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _objRepository.DeleteQn(QuestionId, connection);

            return Ok(test);

        }

    }
}

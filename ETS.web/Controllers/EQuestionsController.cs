using ETS.web.Helper.Attributes;
using ETS.web.Interface;
using ETS.web.Model.EQuestions;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ETS.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [JWTTokenAttribute]
    public class EQuestionsController : ControllerBase
    {
        private readonly IEQuestionsRepository _eQuestionsRepository;
        private readonly IConfiguration _configuration;
        public EQuestionsController(IEQuestionsRepository eQuestionsRepository, IConfiguration configuration)
        {
            _eQuestionsRepository = eQuestionsRepository;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("CreateQuestion")]
        public IActionResult Create(CreateQuestions createQuestions)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _eQuestionsRepository.Create(createQuestions, connection);
            return Ok(test);
            //return createdataction(nameof(getbyid), new { id = notice.noticeid }, notice);
        }

        [HttpPut]
        [Route("UpdateQuestion")]
        public IActionResult Update(UpdateQuestions updateQuestions)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("con").ToString());
            var test = _eQuestionsRepository.Update(updateQuestions, connection);
            return Ok(test);
            //return createdataction(nameof(getbyid), new { id = notice.noticeid }, notice);
        }



        [HttpGet]
        [Route("ViewExamQuestions")]
        //Teacher
        public ActionResult<List<ViewEQuestions>> ViewQuestions(int IExamId)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _eQuestionsRepository.ViewQuestions(IExamId, connection);
            return Ok(test);
        }


        [HttpGet]
        [Route("ViewExamQuestion")]
        //Student
        public ActionResult<List<ViewEQuestions>> ViewQuestion(int IExamId)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _eQuestionsRepository.ViewQuestion(IExamId, connection);
            return Ok(test);
        }


        [HttpGet]
        [Route("ViewExam")]
        //to get the details of exam and questions
        public ActionResult<ViewExams> ViewExam(int IExamId)
        {

            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
           // var es1 =  new ViewExams();
            var es1 = _eQuestionsRepository.GetById(IExamId, connection);
            var mnb = _eQuestionsRepository.ViewQuestion(IExamId, connection);
            es1.QExamList = mnb;
            return Ok(es1);
        }


        [HttpPut]
        [Route("UpdateQuestionStatus")]
        public IActionResult UpdateQnStatus(QnStatus qnStatus)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("con").ToString());
            var test = _eQuestionsRepository.UpdateQnStatus(qnStatus, connection);
            return Ok(test);
            //return createdataction(nameof(getbyid), new { id = notice.noticeid }, notice);
        }

        [HttpPut]
        [Route("UpdateQuestionStatusFalse")]
        public IActionResult UpdateQnStatusF(QnStatus qnStatus)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("con").ToString());
            var test = _eQuestionsRepository.UpdateQnStatusF(qnStatus, connection);
            return Ok(test);
            //return createdataction(nameof(getbyid), new { id = notice.noticeid }, notice);
        }


        [HttpDelete("{EQuestionId}")]
        public IActionResult Delete(int EQuestionId)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _eQuestionsRepository.Delete(EQuestionId, connection);

            return Ok(test);

        }



    }
}

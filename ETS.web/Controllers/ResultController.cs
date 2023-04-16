using ETS.web.DAL;
using ETS.web.Helper.Attributes;
using ETS.web.Interface;
using ETS.web.Model.Result;
using ETSystem.Model.Notice;
using ETSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ETS.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [JWTTokenAttribute]
    public class ResultController : ControllerBase
    {
        private readonly IResultRepository _resultRepository;
        private readonly IConfiguration _configuration;

        public ResultController(IResultRepository resultRepository, IConfiguration configuration)
        {
            _resultRepository = resultRepository;
            _configuration = configuration;
        }

        [HttpGet("{IExamId}")]
        public ActionResult<Mark> GetFullMarkpassmark(int IExamId)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var mark = _resultRepository.GetFullMarkpassmark(IExamId, connection);

            if (mark == null)
            {
                return NotFound();
            }

            return Ok(mark);
        }

        [HttpGet]
        [Route("GetMarkObtained")]
        public ActionResult<Markobtained> GetMarkObtained( int UserId, int IExamId)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var omark = _resultRepository.GetObtainedMark( UserId, IExamId, connection);

            if (omark == null)
            {
                return NotFound();
            }

            return Ok(omark);
        }

        [HttpGet]
        [Route("GetResultView")]
        public ActionResult<ResultView> Get(int UserId, int IExamId)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
           
            var test1 = new ResultView();
            test1.answerList = _resultRepository.Get(UserId, IExamId, connection);
            var test = _resultRepository.GetFullMarkpassmark(IExamId, connection);
            test1.FullMark = test.FullMark;
            test1.PassMark= test.PassMark;
            var t = _resultRepository.GetObtainedMark(UserId, IExamId, connection);
            test1.MarkObtained = t.MarkObtained;
            test1.IExamId = IExamId;


            return Ok(test1);
        }

        //[HttpGet]
        //public ActionResult<List<ResultView>> GetResultViewList(int UserId, int IExamId)
        //{
        //    SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());

        //    var test1 = new ResultView();
        //    test1.answerList = _resultRepository.Get(UserId, IExamId, connection);
        //    var test = _resultRepository.GetFullMarkpassmark(IExamId, connection);
        //    test1.FullMark = test.FullMark;
        //    test1.PassMark = test.PassMark;
        //    var t = _resultRepository.GetObtainedMark(UserId, IExamId, connection);
        //    test1.MarkObtained = t.MarkObtained;
        //    test1.IExamId = IExamId;


        //    return Ok(test1);
        //}


        [HttpGet]
        [Route("ListResult")]
        public ActionResult<List<ResultList>> GetResultList(int UserId)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var t = _resultRepository.GetResultList(UserId, connection);
            return Ok(t);
        }

        [HttpGet]
        [Route("GraphStudent")]
        public ActionResult<List<GraphStudent>> GetResultGraph(int UserId)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var t = _resultRepository.GetResultGraph(UserId, connection);
            return Ok(t);
        }

        [HttpGet]
        [Route("ListResultTeacher")]
        public ActionResult<List<ResultListTeacher>> GetResultListTeacher(int IExamId)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var t = _resultRepository.GetResultListTeacher(IExamId, connection);
            return Ok(t);
        }
    }
}

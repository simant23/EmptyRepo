using ETS.web.DAL;
using ETS.web.Helper.Attributes;
using ETS.web.Interface;
using ETS.web.Model.EAnswer;
using ETS.web.Model.EDetails;
using ETS.web.Model.EQuestions;
using ETSystem.Model.Notice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ETS.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [JWTTokenAttribute]
    public class EDetailsController : ControllerBase
    {
        private readonly IEDetailsRepository _iEDetailsRepository;
        private readonly IConfiguration _configuration;
        public EDetailsController(IEDetailsRepository iEDetailsRepository, IConfiguration configuration)
        {
            _iEDetailsRepository = iEDetailsRepository;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("CreatExamDetails")]
        public IActionResult Create(CEDetails cEDetails)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _iEDetailsRepository.Create(cEDetails, connection);
            return Ok(test);
            //return createdataction(nameof(getbyid), new { id = notice.noticeid }, notice);
        }

        [HttpPut]
        [Route("UpdateExamDetails")]
        public IActionResult Update(UEDetails uEDetails)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("con").ToString());
            var test = _iEDetailsRepository.Update(uEDetails, connection);
            return Ok(test);
            //return createdataction(nameof(getbyid), new { id = notice.noticeid }, notice);
        }

        [HttpGet]
        [Route("ViewExamPattern")]
        public ActionResult<List<VEDetails>> View(int IExamId)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _iEDetailsRepository.View(IExamId, connection);
            if (test == null)
            {
                return NotFound();
            }
            return Ok(test);
        }
    }
}

using ETS.web.DAL;
using ETS.web.Helper.Attributes;
using ETS.web.Interface;
using ETS.web.Model.Dashboard;
using ETSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ETS.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [JWTTokenAttribute]
    public class AdminDashController : ControllerBase
    {
        private readonly IAdminDashRepository _adminDashRepository;
        private readonly IConfiguration _configuration;
        public AdminDashController(IAdminDashRepository adminDashRepository, IConfiguration configuration)
        {
            _adminDashRepository = adminDashRepository;
            _configuration = configuration;
        }


        [HttpGet("total-students")]
        public ActionResult <AdminDash> GetTotalStudents()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
             var test1= _adminDashRepository.GetTotalStudents(connection);
            return Ok(test1);
        }


        [HttpGet("total-teachers")]
        public ActionResult<ATeacher> GetTotalTeachers()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test1 = _adminDashRepository.GetTotalTeachers(connection);
            return Ok(test1);
        }

        [HttpGet("OnGoingExam")]
        public ActionResult<ATeacher> OnGoingExam()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test1 = _adminDashRepository.OnGoingExam(connection);
            return Ok(test1);
        }

        [HttpGet("TotalExam")]
        public ActionResult<ATeacher> TotalExam()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test1 = _adminDashRepository.TotalExam(connection);
            return Ok(test1);
        }

        [HttpGet("total-notices")]
        public ActionResult<TNotice> GetTotalNotices()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test1 = _adminDashRepository.GetTotalNotices(connection);
            return Ok(test1);
        }

        [HttpGet("total-sample-questions")]
        public ActionResult<TSQn> GetTotalSample()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test1 = _adminDashRepository.GetTotalSample(connection);
            return Ok(test1);
        }

        [HttpGet("Teacher&StudentActiveness")]
        public ActionResult<List<Activeness>> GetActiveness()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test1 = _adminDashRepository.GetActiveness(connection);
            return Ok(test1);
        }

    }
}

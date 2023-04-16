using ETS.web.DAL;
using ETSystem.Helper;
using ETSystem.Model;
using ETSystem.Model.LOGINREG;
using ETSystem.Model.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ETS.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LOGINREG_Controller : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LOGINREG_Controller(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("StudentRegistration")]

        public IActionResult SRegistration(SREG_Model student)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            LOGINREG_DAL dal = new LOGINREG_DAL();
            response = dal.SRegistration(student, connection);
            if (!string.IsNullOrEmpty(student.EmailId))
            {
                StaticHelper.SendEmailVerification(student.EmailId, string.IsNullOrEmpty(student.FullName) ? "" : student.FullName);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("TeacherRegistration")]

        public IActionResult TRegistration(TREG_Model teacher)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            LOGINREG_DAL dal = new LOGINREG_DAL();
            response = dal.TRegistration(teacher, connection);
            if (!string.IsNullOrEmpty(teacher.EmailId))
            {
                StaticHelper.SendEmailVerification(teacher.EmailId, string.IsNullOrEmpty(teacher.FullName) ? "" : teacher.FullName);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("UserApproval")]
        public IActionResult UserApproval(UserApproval userApproval)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            LOGINREG_DAL dal = new LOGINREG_DAL();
            response = dal.UserApprove(userApproval, connection);
            return Ok(response);

        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LOGIN_Model login)
        {
            if (login.Type == "Student" || login.Type == "Teacher" || login.Type == "Admin")
            {
                Response response = new Response();
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
                LOGINREG_DAL dal = new LOGINREG_DAL();
                response = dal.Login(login, connection);
                return Ok(response);
            }
            else
            {
                return BadRequest("Invalid type.");
            }
        }

        [HttpPut]
        [Route("ChangePassword")]
        public IActionResult CPassword(ChangePwdl cPassword)
        {


            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            LOGINREG_DAL dal = new LOGINREG_DAL();
            response = dal.ChangePassword(cPassword, connection);
            return Ok(response);

        }

        [HttpPut]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword(ForgetPwd forgetPwd)
        {


            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            LOGINREG_DAL dal = new LOGINREG_DAL();
            response = dal.ForgetPassword(forgetPwd, connection);
            return Ok(response);

        }

        [HttpGet]
        [Route("StudentDetails")]
        public IActionResult GetStudentDetails(string email)
        {
            StudentDetails studentDetails = new StudentDetails
            {
                EmailId = email
            };

            string connectionString = _configuration.GetConnectionString("Con").ToString();

            LOGINREG_DAL dal = new LOGINREG_DAL();
            Response response = dal.StudentDetails(studentDetails, connectionString);

            if (response.StatusCode == 200)
            {
                return Ok(response);
            }
            else if (response.StatusCode == 404)
            {
                return NotFound(response);
            }
            else
            {
                return StatusCode(500, response);
            }
        }

        [HttpGet]
        [Route("TeacherDetails")]
        public IActionResult GetTeacherDetails(string email)
        {
            TeacherDetails tecacherDetails = new TeacherDetails
            {
                EmailId = email
            };

            string connectionString = _configuration.GetConnectionString("Con").ToString();

            LOGINREG_DAL dal = new LOGINREG_DAL();
            Response response = dal.TeachertDetails(tecacherDetails, connectionString);

            if (response.StatusCode == 200)
            {
                return Ok(response);
            }
            else if (response.StatusCode == 404)
            {
                return NotFound(response);
            }
            else
            {
                return StatusCode(500, response);
            }
        }

        [HttpGet]
        [Route("AdminDetails")]
        public IActionResult GetAdminDetails(string email)
        {
            AdminDetails adminDetails = new AdminDetails
            {
                EmailId = email
            };

            string connectionString = _configuration.GetConnectionString("Con").ToString();

            LOGINREG_DAL dal = new LOGINREG_DAL();
            Response response = dal.AdminDetails(adminDetails, connectionString);

            if (response.StatusCode == 200)
            {
                return Ok(response);
            }
            else if (response.StatusCode == 404)
            {
                return NotFound(response);
            }
            else
            {
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        [Route("test")]
        public IActionResult test(string email, string FullName)
        {
            StaticHelper.SendEmailVerification(email, FullName);
            return Ok("sucess");
        }


    }
}

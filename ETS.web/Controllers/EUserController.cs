using ETS.web.Helper.Attributes;
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
    public class EUserController : ControllerBase
    {
        private readonly IEUserRepository _eUserRepository;
        private readonly IConfiguration _configuration;

        public EUserController(IEUserRepository eUserRepository, IConfiguration configuration)
        {
            _eUserRepository = eUserRepository;
            _configuration = configuration;
        }

        [HttpGet("{EmailId}/{Type}")]
        public ActionResult<Notice> GetByEmail(string EmailId, string Type)
        {
            //var euser = _noticeRepository.GetById(id);
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var euser = _eUserRepository.GetByEmail(EmailId,Type, connection);

            if (euser == null)
            {
                return NotFound();
            }

            return Ok(euser);
        }


    }
}

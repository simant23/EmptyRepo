using ETS.web.Helper.Attributes;
using ETSystem.Model.Message;
using ETSystem.Model.Notice;
using ETSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace ETS.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [JWTTokenAttribute]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _msgRepository;
        private readonly IConfiguration _configuration;
        public MessageController(IMessageRepository msgRepository, IConfiguration configuration)
        {
            _msgRepository = msgRepository;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<List<UserDetails>> GetAll()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _msgRepository.GetAll(connection);
            return Ok(test);
            // return Ok();
        }

        [HttpPost]
        public IActionResult Create(SendMsg sendMsg)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _msgRepository.Create(sendMsg, connection);
            return Ok();
        }

        [HttpGet("{senderId}/{receiverId}")]
        public ActionResult<List<ReadMsg>> GetByUId(int senderId, int receiverId)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con")))
            {
                List<ReadMsg> messages = _msgRepository.GetByUId(senderId, receiverId, connection);

                if (messages.Count == 0)
                {
                    return NotFound();
                }

                return Ok(messages);
            }
        }
    }
}

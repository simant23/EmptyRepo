using System.Collections.Generic;
using System.Data.SqlClient;
using ETS.web.Helper.Attributes;
using ETSystem.Model.Notice;
using ETSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ETS.web.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    [JWTTokenAttribute]
    public class NoticesController : ControllerBase
    {
        private readonly INoticeRepository _noticeRepository;
        private readonly IConfiguration _configuration;
        public NoticesController(INoticeRepository noticeRepository, IConfiguration configuration)
        {
            _noticeRepository = noticeRepository;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<List<Notice>> GetAll()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _noticeRepository.GetAll(connection);
            return Ok(test);
        }

        [HttpGet("{NoticeId}")]
        public ActionResult<Notice> GetById(int NoticeId)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var notice = _noticeRepository.GetById(NoticeId, connection);

            if (notice == null)
            {
                return NotFound();
            }

            return Ok(notice);
        }

        [HttpPost]
        public IActionResult Create(CreateNotice cnotice)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _noticeRepository.Create(cnotice, connection);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(CreateNotice cnotice)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _noticeRepository.Update(cnotice, connection);
            return Ok();
        }

        [HttpDelete("{NoticeId}")]
        public IActionResult Delete(int NoticeId)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Con").ToString());
            var test = _noticeRepository.Delete(NoticeId, connection);

            return Ok(test);

        }
    }
}

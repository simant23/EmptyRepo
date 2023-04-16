using Microsoft.EntityFrameworkCore;

namespace ETSystem.Model.Notice
{
    public class Notice
    {
        public int NoticeId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime PostedAt { get; set; }

        public int UserId { get; set; }

        public string? FullName { get; set; }

        public string? Type { get; set; }

    }

}

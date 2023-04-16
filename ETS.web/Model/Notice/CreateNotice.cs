namespace ETSystem.Model.Notice
{
    public class CreateNotice
    {
        public int NoticeId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime PostedAt { get; set; }

        public int UserId { get; set; }
    }
}

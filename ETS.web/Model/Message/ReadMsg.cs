namespace ETSystem.Model.Message
{
    public class ReadMsg
    {
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string? Text { get; set; }

        public DateTime? DateAndTime { get; set; }
    }
}

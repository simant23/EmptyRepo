namespace ETSystem.Model.Message
{
    public class SendMsg
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string? Text { get; set; }
    }
}

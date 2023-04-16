namespace ETSystem.Model
{
    public class EmailHelperModel
    {
        public string? FromFullName { get; set; }
        public string? FromEmailAddress { get; set; }
        public string? ToFullName { get; set; }
        public string? ToEmailAddress { get; set; }
        public string? CCFullName { get; set; }
        public string? CCEmail { get; set; }
        public string? Message { get; set; }
        public string? SmtpPort { get; set; }
        public string? SmtpServer { get; set; }
        public string? SmtpUsername { get; set; }
        public string? SmtpPassword { get; set; }
        public string? EnableSsl { get; set; }
        public string? EmailAddress { get; set; }
        public string? Subject { get; set; }
        public string? Content { get; set; }
        public string? UseDefaultCredentials { get; set; }
    }
}

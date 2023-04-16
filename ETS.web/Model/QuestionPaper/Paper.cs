namespace ETSystem.Model.QuestionPaper
{
    public class Paper
    {
        public int PaperId { get; set; }

        public int UserId { get; set; }

        public string? Description { get; set; }

        public int Class { get; set; }

        //public int SubjectId { get; set; }
        public int TotalMarks { get; set; }

        public int TimeAllowedMinutes { get; set; }
    }
}

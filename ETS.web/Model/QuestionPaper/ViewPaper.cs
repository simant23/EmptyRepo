namespace ETSystem.Model.QuestionPaper
{
    public class ViewPaper
    {
        //View Paper details on the basis of QuestioPpaer
        public int PaperId { get; set; }

        public int Class { get; set; }
        public int UserId { get; set; }

        public string? SubjectName { get; set; }

        //public string? CreatedBy { get; set; }

        public string? Description { get; set; }

        public int TotalMarks { get; set; }

        public int TimeAllowedMinutes { get; set; }

        public DateTime? CreatedDate { get; set; }

    }
}

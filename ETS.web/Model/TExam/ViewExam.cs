namespace ETS.web.Model.TExam
{
    public class ViewExam
    {
        public int IExamId { get; set; }
        public string? ExamDescription { get; set; }
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }

        public int Class { get; set; }

        public DateTime ExamDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public int ExamDuration { get; set; }

        public int FullMark { get; set; }

        public int PassMark { get; set; }
        public int ExamStatus { get; set; }


    }

   
}

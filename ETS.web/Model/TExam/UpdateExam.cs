namespace ETS.web.Model.TExam
{
    public class UpdateExam
    {
        public int IExamId { get; set; }

        //public int SubjectId { get; set; }

        public int Class { get; set; }

        public int FullMark { get; set; }

        public int PassMark { get; set; }

        public string? ExamDate { get; set; }

        public string? StartTime { get; set; }

        public int ExamDuration { get; set; }

        public string? ExamDescription { get; set; }
    }
}

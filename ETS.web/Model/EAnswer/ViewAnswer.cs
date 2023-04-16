namespace ETS.web.Model.EAnswer
{
    public class ViewAnswer
    {
        public string Question { get; set; }
        public int IMark { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string Answer { get; set; }
        public string CorrectAnswer { get; set; }
        public ExamDetails ExamDetails { get; set; }
    }

    public class ExamDetails
    {
        public string SubjectName { get; set; }
        public string Class { get; set; }
        public int FullMark { get; set; }
        public int PassMark { get; set; }
        public string ExamDate { get; set; }
        public string StartTime { get; set; }
        public int ExamDuration { get; set; }
        public string ExamDescription { get; set; }
    }

}

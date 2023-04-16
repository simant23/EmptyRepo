namespace ETS.web.Model.Result
{
    public class ResultView
    {
      

        public int IExamId { get; set; }
        public int FullMark { get; set; }
        public int PassMark { get; set; }
        public int MarkObtained { get; set; }
        public List<AnswerResultView> answerList { get; set; }
    }


    public class AnswerResultView
    {
        public string? Question { get; set; }

        public string? Answer { get; set; }
        public string? CorrectAnswer { get; set; }
        public int IMark { get; set; }
    }

    public class Mark
    {
        public int PassMark {set; get; }

        public int FullMark { get; set; }

        //public int IExamId { get; set; }
    }

    public class Markobtained
    {
        public int FullMark { set; get; } 
        public int PassMark { set; get; }
        public int Class { set; get; }
        public string SubjectName { set; get; }
        public string ExamDescription { set; get; }
        public DateTime ExamDate { set; get; }
        public int MarkObtained { get; set; }
        public string Grade { set; get; }


    }
}

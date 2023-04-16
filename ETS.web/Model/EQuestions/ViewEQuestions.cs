using ETS.web.Model.TExam;

namespace ETS.web.Model.EQuestions
{
    public class ViewEQuestions
    {
        public int IExamId { get; set; }
        public int EQuestionId { get; set; }
        public string? Question { get; set; }

        public int IMark { get; set; }

        public string? OptionA { get; set; }
        public string? OptionB { get; set; }
        public string? OptionC { get; set; }
        public string? OptionD { get; set; }
        public string? CorrectAnswer { get; set; }

        public int QuestionStatus { get; set; }
    }

    public class ViewExams
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

        public List<ViewEQuestions> QExamList { get; set; }
    }
}

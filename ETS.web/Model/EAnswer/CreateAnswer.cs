namespace ETS.web.Model.EAnswer
{
    public class CreateAnswer
    {
        public int UserId { get; set; }

        public List<QAnswer> AnswerList { get; set; }

    }
    public class QAnswer
    {
        public int EQuestionId { get; set; }
        public string Answer { get; set; }
         public int IsSubmitted { get; set; }

    }
}

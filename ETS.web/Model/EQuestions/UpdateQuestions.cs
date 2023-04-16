namespace ETS.web.Model.EQuestions
{
    public class UpdateQuestions
    {
        public int EQuestionId { get; set; }
        public string? Question { get; set; }

        public string? IMark { get; set; }

        public string? OptionA { get; set; }
        public string? OptionB { get; set; }
        public string? OptionC { get; set; }
        public string? OptionD { get; set; }

        public string? CorrectAnswer { get; set; }
    }
}

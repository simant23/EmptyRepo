namespace ETS.web.Model.Question
{
    public class UpdateQ
    {
        public int QuestionId { get; set; }
        public string? QuestionText { get; set; }

        //public int PaperId { get; set; }

        public int Marks { get; set; }

        public string? A { get; set; }
        public string? B { get; set; }
        public string? C { get; set; }
        public string? D { get; set; }

        public string? Answer { get; set; }
    }
}

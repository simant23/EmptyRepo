using System.Runtime.CompilerServices;

namespace ETSystem.Model.Question
{
    public class ViewQuestions
    {
        public int Marks { get; set; }

        public int QuestionId { get; set; }

        public string? QuestionText { get; set; }

        public string? A { get; set; }
        public string? B { get; set; }
        public string? C { get; set; }
        public string? D { get; set; }
        public string? Answer { get; set; }

    }
}


using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ETSystem.Model.QuestionPaper
{
    public class QuestionPaper
    {

        public int SubjectId { get; set; }

        public int UserId { get; set; }

        public int PaperId { get; }
        public int Class { get; set; }

        public string? Description { get; set; }

        public int TotalMarks { get; set; }

        public int TimeAllowedMinutes { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}

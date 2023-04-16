using ETS.web.Model.Dashboard;
using ETSystem.Model.LOGINREG;
using ETSystem.Model.Notice;
using ETSystem.Model.Question;
using ETSystem.Model.QuestionPaper;

namespace ETSystem.Model
{
    public class Response
    {
        public int StatusCode { get; set; }

        public string? StatusMessage { get; set; }

        public StudentDetails? Result { get; set; }

        public TeacherDetails? teacherDetails { get; set; }
        public AdminDetails? adminDetails { get; set; }
    }
}

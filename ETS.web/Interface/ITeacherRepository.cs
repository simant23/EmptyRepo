using ETSystem.Model;
using ETSystem.Model.QuestionPaper;
using ETSystem.Model.User;
using System.Data.SqlClient;

namespace ETSystem.Repository
{
    public interface ITeacherRepository
    {
        Response UpdateTeacher(Teacher teacher, SqlConnection connection);
    }
}

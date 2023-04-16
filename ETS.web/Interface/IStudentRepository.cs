using ETSystem.Model;
using ETSystem.Model.Notice;
using ETSystem.Model.User;
using System.Data.SqlClient;

namespace ETSystem.Repository
{
    public interface IStudentRepository
    {
        Response UpdateStudent(Student student, SqlConnection connection);
    }
}

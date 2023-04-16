using ETS.web.Model.TeacherDash;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ETS.web.Interface
{
    public interface ITeacherDashRepoitory
    {
        TeacherTExam TotalExamCreated(int UserId, SqlConnection connection);
    }
}

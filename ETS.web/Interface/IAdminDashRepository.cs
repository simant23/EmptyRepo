using ETS.web.Model.Dashboard;
using ETSystem.Model;
using ETSystem.Model.Notice;
using System.Data.SqlClient;

namespace ETS.web.Interface
{
    public interface IAdminDashRepository
    {
        AdminDash GetTotalStudents( SqlConnection connection);
        ATeacher GetTotalTeachers(SqlConnection connection);
        TotalExam OnGoingExam(SqlConnection connection);
        TotalExam TotalExam(SqlConnection connection);
        TNotice GetTotalNotices(SqlConnection connection);
        TSQn GetTotalSample(SqlConnection connection);
        List<Activeness> GetActiveness(SqlConnection connection);


    }
}

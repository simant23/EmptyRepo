using ETS.web.Model.TExam;
using ETSystem.Model;
using ETSystem.Model.Message;
using ETSystem.Model.Notice;
using ETSystem.Model.TExam;
using System.Data.SqlClient;

namespace ETSystem.Interface
{
    public interface ITExamRepository
    {
        Response Create(TeacherExam teacherExam, SqlConnection connection);
        Response UpdateExam(UpdateExam updateExam, SqlConnection connection);

        List<ViewExam> GetByClass(int Class, SqlConnection connection);

        List<ViewExam> GetAll(SqlConnection connection);

        Response Delete(int IExamId, SqlConnection connection);

        Response UpdateExamStatus(int IExamId, SqlConnection connection);
        Response UpdateExamStatusFalse(int IExamId, SqlConnection connection);
    }
}

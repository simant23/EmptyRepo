using ETS.web.Model.Result;
using ETSystem.Model.Notice;
using System.Data.SqlClient;

namespace ETS.web.Interface
{
    public interface IResultRepository
    {
        //List<Markobtained> GetObtainedMarkList(int UserId, SqlConnection connection);

        Mark GetFullMarkpassmark(int IExamId, SqlConnection connection);
        Markobtained GetObtainedMark(int UserId, int IExamId, SqlConnection connection);
        List<AnswerResultView> Get(int UserId, int IExamId, SqlConnection connection);
        List<ResultList> GetResultList(int UserId, SqlConnection connection);
        List<GraphStudent> GetResultGraph(int UserId, SqlConnection connection);
        List<ResultListTeacher> GetResultListTeacher(int IExamId, SqlConnection connection);
    }
}

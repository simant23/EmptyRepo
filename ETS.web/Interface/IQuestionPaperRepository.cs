using ETSystem.Model;
using ETSystem.Model.Notice;
using ETSystem.Model.Question;
using ETSystem.Model.QuestionPaper;
using System.Data.SqlClient;

namespace ETSystem.Repository
{
    public interface IQuestionPaperRepository
    {

        Response CreatePaper(QuestionPaper questionPaper, SqlConnection connection);

        List<ViewPaper> ViewPaperById(int Class, SqlConnection connection);

        Response DeletePaper(int PaperId, SqlConnection connection);

        List<ViewPaper> GetAll(SqlConnection connection);

        Response UpdatePaper(Paper paper, SqlConnection connection);
    }
}

using ETS.web.Model.Question;
using ETSystem.Model;
using ETSystem.Model.Notice;
using ETSystem.Model.Question;
using ETSystem.Model.QuestionPaper;
using ETSystem.Model.SampleQuestion;
using System.Data.SqlClient;

namespace ETSystem.Repository
{
    public interface IObjQRepository
    {
        Response Create(ObjQ objective, SqlConnection connection);

        List<ViewQuestions> ViewQuestionsById(int PaperId, SqlConnection connection);

        Response UpdateQ(UpdateQ obj, SqlConnection connection);
        Response DeleteQn(int QuestionId, SqlConnection connection);










    }
}

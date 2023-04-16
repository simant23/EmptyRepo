using ETS.web.Model.EAnswer;
using ETS.web.Model.EQuestions;
using ETSystem.Model;
using ETSystem.Model.Message;
using System.Data.SqlClient;

namespace ETS.web.Interface
{
    public interface IEAnswerRepository
    {
        Response Create(CreateAnswer createAnswer, SqlConnection connection);

        List<ViewAnswer> GetById( int IExamId, int UserId, SqlConnection connection);

        //Check CheckAnswer(int UserId, int EQuestionId, SqlConnection connection);

    }

}

using ETSystem.Model.SampleQuestion;
using ETSystem.Model;
using System.Data.SqlClient;
using ETS.web.Model.EQuestions;
using ETSystem.Model.Question;

namespace ETS.web.Interface
{
    public interface IEQuestionsRepository
    {
        Response Create(CreateQuestions createQuestions, SqlConnection connection);

        Response Update(UpdateQuestions updateQuestions, SqlConnection connection);

        List<ViewEQuestions> ViewQuestions(int IExamId, SqlConnection connection);
        List<ViewEQuestions> ViewQuestion(int IExamId, SqlConnection connection);

        ViewExams GetById( int IExamId, SqlConnection connection);
        Response UpdateQnStatus(QnStatus qnStatus, SqlConnection connection);
        Response UpdateQnStatusF(QnStatus qnStatus, SqlConnection connection);

        Response Delete(int EQuestionId, SqlConnection connection);

    }
}

using ETSystem.Model.Notice;
using ETSystem.Model.QuestionPaper;
using System.Data.SqlClient;

namespace ETSystem.Repository
{
    public interface ISubjectRepository
    {
        Subject GetBySubject(string SubjectId, SqlConnection connection);
    }
}

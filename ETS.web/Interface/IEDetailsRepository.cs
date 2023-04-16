using ETS.web.Model.EAnswer;
using ETS.web.Model.EDetails;
using ETSystem.Model;
using System.Data.SqlClient;

namespace ETS.web.Interface
{
    public interface IEDetailsRepository
    {
        Response Create(CEDetails cEDetails, SqlConnection connection);
        Response Update(UEDetails uEDetails, SqlConnection connection);

        List<VEDetails> View(int IExamId, SqlConnection connection);
    }
}

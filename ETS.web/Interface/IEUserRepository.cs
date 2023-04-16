using ETSystem.Model.Notice;
using System.Data.SqlClient;

namespace ETSystem.Repository
{
    public interface IEUserRepository
    {
        EUser GetByEmail(string EmailId, string Type, SqlConnection connection);


    }
}

using ETSystem.Model;
using ETSystem.Model.Message;
using ETSystem.Model.Notice;
using System.Data.SqlClient;

namespace ETSystem.Repository
{
    public interface IMessageRepository
    {
        List<UserDetails> GetAll(SqlConnection connection);


        Response Create(SendMsg sendMsg, SqlConnection connection);

        List<ReadMsg> GetByUId(int senderId, int receiverId, SqlConnection connection);

    }
}

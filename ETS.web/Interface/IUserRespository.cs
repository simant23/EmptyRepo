using ETS.web.Model;
using ETSystem.Model.Notice;
using ETSystem.Model.User;
using System.Data.SqlClient;

namespace ETSystem.Interface
{
    public interface IUserRepository
    {
        List<User> GetAll(SqlConnection connection);
        User GetUserDetail(SqlConnection connection, string UserId);
        User LoginUser(SqlConnection connection, TokenRequestModel req);
    }
}

using ETSystem.Model;
using ETSystem.Model.User;
using System.Data.SqlClient;

namespace ETSystem.Repository
{
    public interface IAdminRepository
    {
        Response UpdateAdmin(Admin admin, SqlConnection connection);
    }
}

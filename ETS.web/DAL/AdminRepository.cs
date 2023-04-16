using ETSystem.Model;
using ETSystem.Model.User;
using ETSystem.Repository;
using System.Data.SqlClient;

namespace ETS.web.DAL
{
    public class AdminRepository : IAdminRepository
    {
        public Response UpdateAdmin(Admin admin, SqlConnection connection)
        {
            Response response = new Response();
            if (connection == null)
            {
                response.StatusCode = 500;
                response.StatusMessage = "SQL Connection is null";
                return response;
            }
            try
            {
                //Open the connection to the database.
                connection.Open();

                //SQL query to insert a new record into the SchoolUser table.
                string queryUser = "UPDATE SchoolUser SET FullName = @FullName , EmailId = @EmailId , Phone = @Phone, Address = @Address WHERE UserId = @UserId";


                //Execute the query using a SqlCommand object.
                using (SqlCommand cmdUser = new SqlCommand(queryUser, connection))
                {
                    //Add values to the parameters in the query.
                    cmdUser.Parameters.AddWithValue("@UserId", admin.UserId);
                    cmdUser.Parameters.AddWithValue("@FullName", admin.FullName);
                    cmdUser.Parameters.AddWithValue("@EmailId", admin.EmailId);
                    cmdUser.Parameters.AddWithValue("@Phone", admin.Phone);
                    cmdUser.Parameters.AddWithValue("@Address", admin.Address);



                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Update into Admin  failed");
                    }
                    else
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Updation Successful";
                        return response;
                    }
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Updation: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return response;
        }
    }
}

using ETSystem.Model;
using ETSystem.Model.Notice;
using ETSystem.Repository;
using System.Data.SqlClient;

namespace ETS.web.DAL
{
    public class EUserRepository : IEUserRepository
    {
        //public EUser GetByEmail(string EmailId, SqlConnection connection)
        //{
        //    Response response = new Response();
        //    EUser eUser = new EUser();

        //    try
        //    {
        //        // Open the connection to the database.
        //        connection.Open();

        //        // Define the SQL query.
        //        string queryEUser = "SELECT s.Class, u.UserId FROM SchoolUser u INNER JOIN Student s ON u.UserId = s.UserId WHERE u.EmailId = @EmailId;";

        //        // Execute the query using a SqlCommand.
        //        using (SqlCommand cmdUser = new SqlCommand(queryEUser, connection))
        //        {
        //            // Add the emailId parameter to the query.
        //            cmdUser.Parameters.AddWithValue("@EmailId", EmailId);

        //            // Execute the query and retrieve the results.
        //            SqlDataReader reader = cmdUser.ExecuteReader();
        //            if (!reader.HasRows)
        //            {
        //                reader.Close();
        //            }
        //            else
        //            {
        //                while (reader.Read())
        //                {
        //                    eUser.UserId = (int)reader["UserId"];
        //                    eUser.Class = (int)reader["Class"];
        //                    eUser.EmailId = (string)reader["EmailId"];
        //                }

        //                // Close the reader if a matching record is found.
        //                reader.Close();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.StatusCode = 500;
        //        response.StatusMessage = "An error occurred during User Email View: " + ex.Message;
        //    }
        //    finally
        //    {
        //        // Close the connection to the database.
        //        connection.Close();
        //    }

        //    return eUser;
        //}

        public EUser GetByEmail(string EmailId, string Type, SqlConnection connection)
        {
            Response response = new Response();
            EUser eUser = new EUser();

            try
            {
                // Open the connection to the database.
                connection.Open();

                string queryEUser = "";

                if (Type == "Student")
                {
                    queryEUser = "SELECT s.Class, u.UserId, u.EmailId FROM SchoolUser u INNER JOIN Student s ON u.UserId = s.UserId WHERE u.EmailId = @EmailId;";
                }
                else
                {
                    queryEUser = "SELECT u.UserId, u.EmailId FROM SchoolUser u WHERE u.EmailId = @EmailId;";
                }

                // Define the SQL query.

                // Execute the query using a SqlCommand.
                using (SqlCommand cmdUser = new SqlCommand(queryEUser, connection))
                {
                    // Add the emailId parameter to the query.
                    cmdUser.Parameters.AddWithValue("@EmailId", EmailId);

                    // Execute the query and retrieve the results.
                    SqlDataReader reader = cmdUser.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        reader.Close();
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            eUser.UserId = (int)reader["UserId"];
                            eUser.EmailId = (string)reader["EmailId"];

                            if (Type == "Student")
                            {
                                eUser.Class = (int)reader["Class"];
                            }
                        }

                        // Close the reader if a matching record is found.
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during User Email View: " + ex.Message;
            }
            finally
            {
                // Close the connection to the database.
                connection.Close();
            }

            return eUser;
        }

    }
}

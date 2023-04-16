using ETSystem.Model;
using ETSystem.Model.Notice;
using ETSystem.Model.QuestionPaper;
using ETSystem.Repository;
using System.Data.SqlClient;

namespace ETS.web.DAL
{
    public class SubjectRepository : ISubjectRepository
    {
        public Subject GetBySubject(string SubjectName, SqlConnection connection)
        {
            Response response = new Response();
            Subject subject = new Subject();

            try
            {
                // Open the connection to the database.
                connection.Open();

                // Define the SQL query.
                string querySubject = "SELECT SubjectId, SubjectName FROM Subjects WHERE SubjectName = @SubjectName";

                // Execute the query using a SqlCommand.
                using (SqlCommand cmdUser = new SqlCommand(querySubject, connection))
                {
                    // Add the emailId parameter to the query.
                    cmdUser.Parameters.AddWithValue("@SubjectName", SubjectName);

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
                            subject.SubjectId = (int)reader["SubjectId"];
                            subject.SubjectName = (string)reader["SubjectName"];
                        }

                        // Close the reader if a matching record is found.
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occured during Subject View:"+ex.Message;
            }
            finally
            {
                // Close the connection to the database.
                connection.Close();
            }

            return subject;
        }
    }
}

using ETSystem.Model;
using ETSystem.Model.User;
using ETSystem.Repository;
using System.Data.SqlClient;

namespace ETS.web.DAL
{
    public class TeacherRepository : ITeacherRepository
    {
        public Response UpdateTeacher(Teacher teacher, SqlConnection connection)
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
                string queryUser = "UPDATE SchoolUser SET FullName = @FullName , " +
                    "EmailId = @EmailId , Phone = @Phone, Gender = @Gender, Address = @Address " +
                    "WHERE UserId = @UserId";


                //Execute the query using a SqlCommand object.
                using (SqlCommand cmdUser = new SqlCommand(queryUser, connection))
                {
                    //Add values to the parameters in the query.
                    cmdUser.Parameters.AddWithValue("@UserId", teacher.UserId);
                    cmdUser.Parameters.AddWithValue("@FullName", teacher.FullName);
                    cmdUser.Parameters.AddWithValue("@EmailId", teacher.EmailId);
                    cmdUser.Parameters.AddWithValue("@Phone", teacher.Phone);
                    cmdUser.Parameters.AddWithValue("@Gender", teacher.Gender);
                    cmdUser.Parameters.AddWithValue("@Address", teacher.Address);

                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Updation Failed in SchoolUser");
                    }
                }

                //SQL query to insert a new record into the Student table.
                string queryTeacher = "UPDATE Teacher SET Major = @Major, Qualification = @Qualification, SubTeacher = @SubTeacher, TExperience = @TExperience WHERE TeacherId = @TeacherId";
                

                //Execute the query using a SqlCommand object.
                using (SqlCommand cmdStudent = new SqlCommand(queryTeacher, connection))
                {
                    cmdStudent.Parameters.AddWithValue("@TeacherId", teacher.TeacherId);
                    cmdStudent.Parameters.AddWithValue("@Major", teacher.Major);
                    cmdStudent.Parameters.AddWithValue("@Qualification", teacher.Qualification);
                    cmdStudent.Parameters.AddWithValue("@SubTeacher", teacher.SubTeacher);
                    cmdStudent.Parameters.AddWithValue("@TExperience", teacher.TExperience);

                    int k = cmdStudent.ExecuteNonQuery();
                    if (k <= 0)
                    {
                        throw new Exception("Updation Failed in Teacher");

                    }
                    else
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Updation Successful";
                    }
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Teacher's Details Updation: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return response;
        }
    }
}

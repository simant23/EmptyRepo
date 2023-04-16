using ETSystem.Model;
using ETSystem.Model.Notice;
using ETSystem.Model.User;
using ETSystem.Repository;
using System.Data.SqlClient;

namespace ETS.web.DAL
{
    public class StudentRepository : IStudentRepository
    {
        public Response UpdateStudent(Student student, SqlConnection connection)
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

                //SQL query to update the record in the SchoolUser table.
                string queryUser = @"UPDATE SchoolUser SET FullName = @FullName, EmailId = @EmailId, Phone = @Phone, Gender = @Gender, Address = @Address FROM SchoolUser WHERE UserId = @UserId ";

                //Execute the query using a SqlCommand object.
                using (SqlCommand cmdUser = new SqlCommand(queryUser, connection))
                {
                    //Add values to the parameters in the query.
                    cmdUser.Parameters.AddWithValue("@UserId", student.UserId);
                    cmdUser.Parameters.AddWithValue("@FullName", student.FullName);
                    cmdUser.Parameters.AddWithValue("@EmailId", student.EmailId);
                    cmdUser.Parameters.AddWithValue("@Phone", student.Phone);
                    cmdUser.Parameters.AddWithValue("@Gender", student.Gender);
                    cmdUser.Parameters.AddWithValue("@Address", student.Address);

                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Updation Failed in SchoolUser");
                    }
                }

                //SQL query to update the record in the Student table.
                string queryStudent = "UPDATE Student SET Grade = @Grade, GuardianName = @GuardianName, DOB = @DOB, EnrollmentDate = @EnrollmentDate WHERE  StudentId = @StudentId";

                //Execute the query using a SqlCommand object.
                using (SqlCommand cmdStudent = new SqlCommand(queryStudent, connection))
                {
                    //cmdStudent.Parameters.AddWithValue("@UserId", student.UserId);
                    cmdStudent.Parameters.AddWithValue("@StudentId", student.StudentId);
                    cmdStudent.Parameters.AddWithValue("@GuardianName", student.GuardianName);
                    cmdStudent.Parameters.AddWithValue("@Grade", student.Grade);
                    cmdStudent.Parameters.AddWithValue("@DOB", student.DOB);
                    cmdStudent.Parameters.AddWithValue("@EnrollmentDate", student.EnrollmentDate);

                    int k = cmdStudent.ExecuteNonQuery();
                    if (k <= 0)
                    {
                        throw new Exception("Updation Failed in Student");
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
                response.StatusMessage = "An error occurred during Student's Details Updation: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return response;
        }




    }
}

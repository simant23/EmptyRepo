using ETS.web.Interface;
using ETS.web.Model.TeacherDash;
using ETSystem.Model;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ETS.web.DAL
{
    public class TeacherDashRepository : ITeacherDashRepoitory
    {
        public TeacherTExam TotalExamCreated(int UserId, SqlConnection connection)
        {
            TeacherTExam teDash = new TeacherTExam();
            Response response = new Response();
            try
            {
                connection.Open();
                string query = $@"SELECT COUNT(*) AS TotalExams
                FROM IExam
                WHERE UserId = {UserId} AND ExamStatus = 0";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        reader.Close();
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            teDash.TotalExams = (int)reader["TotalExams"];
                        }
                        reader.Close();
                    }
                }

                response.StatusCode = 200;
                response.StatusMessage = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Total Exam Created View: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return teDash;
        }
    }
}

using ETSystem.Model;
using System.Data.SqlClient;
using ETS.web.Model.Dashboard;
using ETS.web.Interface;
using ETSystem.Model.Notice;

namespace ETS.web.DAL
{
    public class AdminDashRepository : IAdminDashRepository
        {
        public AdminDash GetTotalStudents(SqlConnection connection)
            {
            AdminDash adminDash = new AdminDash();
            Response response = new Response();
                try
                {
                    connection.Open();
                    string query = @"SELECT COUNT(*) AS TotalStudents
                             FROM SchoolUser
                             WHERE Type = 'Student' AND IsApproved = 1";

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

                            adminDash.TotalStudents = (int)reader["TotalStudents"];
                                //response.Data = adminDash;
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
                    response.StatusMessage = "An error occurred during Total Students View: " + ex.Message;
                }
                finally
                {
                    connection.Close();
                }

                return adminDash;
            }

        public ATeacher GetTotalTeachers(SqlConnection connection)
        {
            ATeacher adminDash1 = new ATeacher();
            Response response = new Response();
            try
            {
                connection.Open();
                string query = @"SELECT COUNT(*) AS TotalTeachers
                             FROM SchoolUser
                             WHERE Type = 'Teacher' AND IsApproved = 1";

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

                            adminDash1.TotalTeachers = (int)reader["TotalTeachers"];
                            //response.Data = adminDash;
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
                response.StatusMessage = "An error occurred during Total Teachers View: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return adminDash1;
        }
        
        public TotalExam OnGoingExam(SqlConnection connection)
        {
            TotalExam adminDash2 = new TotalExam();
            Response response = new Response();
            try
            {
                connection.Open();
                string query = @"SELECT COUNT(*) AS TotalExams
                             FROM IExam
                             WHERE ExamStatus = 1";

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

                            adminDash2.TotalExams = (int)reader["TotalExams"];
                            //response.Data = adminDash;
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
                response.StatusMessage = "An error occurred during Total Teachers View: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return adminDash2;
        }

        public TotalExam TotalExam(SqlConnection connection)
        {
            TotalExam adminDash3 = new TotalExam();
            Response response = new Response();
            try
            {
                connection.Open();
                string query = @"SELECT COUNT(*) AS TotalExams
                             FROM IExam
                             WHERE ExamStatus = 0 OR ExamStatus = 1";

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

                            adminDash3.TotalExams = (int)reader["TotalExams"];
                            //response.Data = adminDash;
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
                response.StatusMessage = "An error occurred during Total Teachers View: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return adminDash3;
        }

        public TNotice GetTotalNotices(SqlConnection connection)
        {
            TNotice adminDash4 = new TNotice();
            Response response = new Response();
            try
            {
                connection.Open();
                string query = @"SELECT COUNT(*) AS TotalNotice
                             FROM Notice";

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

                            adminDash4.TotalNotice = (int)reader["TotalNotice"];
                            //response.Data = adminDash;
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
                response.StatusMessage = "An error occurred during Total Notices View: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return adminDash4;
        }

        public TSQn GetTotalSample(SqlConnection connection)
        {
            TSQn adminDash5 = new TSQn();
            Response response = new Response();
            try
            {
                connection.Open();
                string query = @"SELECT COUNT(*) AS TotalSample
                             FROM QuestionPaper";

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

                            adminDash5.TotalSample = (int)reader["TotalSample"];
                            //response.Data = adminDash;
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
                response.StatusMessage = "An error occurred during Total Notices View: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return adminDash5;
        }

        public List<Activeness> GetActiveness(SqlConnection connection)
        {
            List<Activeness> list = new List<Activeness>();
            Response response = new Response();
            try
            {
                connection.Open();
                string query = @"SELECT 
                    CASE 
                        WHEN u.Type = 'Teacher' THEN 'Teacher'
                        WHEN u.Type = 'Student' THEN 'Student'
                        ELSE 'Admin'
                    END AS UserType,
                    COUNT(*) AS Count
                FROM 
                    (
                        SELECT m.SenderId, m.ReceiverId FROM MESSAGE m UNION
                        SELECT e.UserId, NULL FROM IExam e WHERE e.ExamStatus = 1 UNION
                        SELECT qp.UserId, NULL FROM QuestionPaper qp UNION
                        SELECT n.UserId, NULL FROM Notice n UNION
                        SELECT e.UserId, NULL FROM IExam e WHERE e.ExamStatus = 0 
                    ) AS actions
                    RIGHT JOIN SchoolUser u ON actions.SenderId = u.UserId OR actions.ReceiverId = u.UserId
                GROUP BY 
                    CASE 
                        WHEN u.Type = 'Teacher' THEN 'Teacher'
                        WHEN u.Type = 'Student' THEN 'Student'
                        ELSE 'Admin'
                    END";



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

                            Activeness adminDash6 = new Activeness();
                            adminDash6.UserType = (string)reader["UserType"];
                            adminDash6.Count = (int)reader["Count"];
                            //response.Data = adminDash;
                            list.Add(adminDash6);

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
                response.StatusMessage = "An error occurred during Activeness: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return list;
        }



    }

    
}

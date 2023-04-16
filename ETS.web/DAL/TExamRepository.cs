using ETS.web.Model.TExam;
using ETSystem.Interface;
using ETSystem.Model;
using ETSystem.Model.Message;
using ETSystem.Model.Notice;
using ETSystem.Model.TExam;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Security.Claims;

namespace ETS.web.DAL
{
    public class TExamRepository : ITExamRepository
    {

        public Response Create(TeacherExam teacherExam, SqlConnection connection)
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
                string queryExam = "INSERT INTO IExam ( UserId, SubjectId, Class, FullMark, PassMark, ExamDate, StartTime, ExamDuration, ExamDescription)" +
                    "VALUES ( @UserId, @SubjectId, @Class, @FullMark, @PassMark, @ExamDate, @StartTime, @ExamDuration, @ExamDescription)";

                using (SqlCommand cmdUser = new SqlCommand(queryExam, connection))
                {
                    //Add values to the parameters in the query.
                    //cmdUser.Parameters.AddWithValue("@NoticeId", notice.NoticeId);
                    cmdUser.Parameters.AddWithValue("@UserId", teacherExam.UserId);
                    cmdUser.Parameters.AddWithValue("@SubjectId", teacherExam.SubjectId);
                    cmdUser.Parameters.AddWithValue("@Class", teacherExam.Class);
                    cmdUser.Parameters.AddWithValue("@FullMark", teacherExam.FullMark);
                    cmdUser.Parameters.AddWithValue("@PassMark", teacherExam.PassMark);
                    cmdUser.Parameters.AddWithValue("@ExamDate", teacherExam.ExamDate);
                    cmdUser.Parameters.AddWithValue("@StartTime", teacherExam.StartTime);
                    cmdUser.Parameters.AddWithValue("@ExamDuration", teacherExam.ExamDuration);
                    cmdUser.Parameters.AddWithValue("@ExamDescription", teacherExam.ExamDescription);


                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Insert into Exam failed");
                    }
                    else
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Creation Successful";
                        return response;
                    }
                }
            }

            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Exam Creation: " + ex.Message;
            }
            finally
            {
                response.StatusCode = 200;
                response.StatusMessage = "Exam Creation Successful";
                connection.Close();
            }

            return response;
        }

        public Response UpdateExam(UpdateExam updateExam, SqlConnection connection)
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
                string queryUExam = "UPDATE IExam SET  Class  = @Class , FullMark  = @FullMark , PassMark  = @PassMark, ExamDate = @ExamDate, StartTime = @StartTime, ExamDuration = @ExamDuration, ExamDescription = @ExamDescription WHERE IExamId  = @IExamId ";

                using (SqlCommand cmdUser = new SqlCommand(queryUExam, connection))
                {
                    //Add values to the parameters in the query.
                    cmdUser.Parameters.AddWithValue("@IExamId", updateExam.IExamId);
                    //cmdUser.Parameters.AddWithValue("@SubjectId", updateExam.SubjectId);
                    cmdUser.Parameters.AddWithValue("@Class", updateExam.Class);
                    cmdUser.Parameters.AddWithValue("@FullMark", updateExam.FullMark);
                    cmdUser.Parameters.AddWithValue("@PassMark", updateExam.PassMark);
                    cmdUser.Parameters.AddWithValue("@ExamDate", updateExam.ExamDate);
                    cmdUser.Parameters.AddWithValue("@StartTime", updateExam.StartTime);
                    cmdUser.Parameters.AddWithValue("@ExamDuration", updateExam.ExamDuration);
                    cmdUser.Parameters.AddWithValue("@ExamDescription", updateExam.ExamDescription);


                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Update into Exam Failed");
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
                response.StatusMessage = "An error occurred during Exam Update: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return response;
        }


        //View of Student
        public List<ViewExam> GetByClass(int Class, SqlConnection connection)
        {
            Response response = new Response();
            List<ViewExam> viewExams = new List<ViewExam>();

            string query = $@"SELECT IExam.IExamId, IExam.Class, IExam.FullMark, IExam.PassMark, IExam.ExamDate, IExam.StartTime, IExam.ExamDuration, IExam.ExamDescription, Subjects.SubjectName
            FROM IExam
            JOIN Subjects ON IExam.SubjectId = Subjects.SubjectId
            WHERE IExam.Class = '{Class}'
            ORDER BY IExam.ExamDate ASC
            ";

            try
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    //cmd.Parameters.AddWithValue("@Class", Class);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ViewExam viewExam = new ViewExam
                        {
                            IExamId = (int)reader["IExamId"],
                            Class = (int)reader["Class"],
                            FullMark = (int)reader["FullMark"],
                            PassMark = (int)reader["PassMark"],
                            ExamDate = (DateTime)reader["ExamDate"],
                            StartTime = (TimeSpan)reader["StartTime"],
                            ExamDescription = (string)reader["ExamDescription"],
                            ExamDuration = (int)reader["ExamDuration"]
                        };

                        viewExams.Add(viewExam);
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode= 500;
                response.StatusMessage = "An error occured during Examination View of Students:"+ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return viewExams;
        }

        //View of Teacher
        public List<ViewExam> GetAll(SqlConnection connection)
        {
            List<ViewExam> list = new List<ViewExam>();
            Response response = new Response();
            try
            {

                //Open the connection to the database.
                connection.Open();

                //string queryExam = $@"SELECT Class, FullMark, PassMark, ExamDate, StartTime, ExamDuration, ExamDescription 
                //  FROM IExam 
                //  ORDER BY ExamDate DESC"; 
                string queryExam = $@"SELECT IExam.IExamId,IExam.Class, IExam.FullMark, IExam.PassMark, IExam.ExamDate, IExam.StartTime, IExam.ExamDuration, IExam.ExamDescription,IExam.SubjectId, Subjects.SubjectName, IExam.ExamStatus
                FROM IExam
                JOIN Subjects ON IExam.SubjectId = Subjects.SubjectId
                ORDER BY IExam.ExamDate DESC";




                using (SqlCommand cmdUser = new SqlCommand(queryExam, connection))
                {
                    SqlDataReader reader = cmdUser.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        response.StatusCode = 401;
                        response.StatusMessage = "Exam Failed";
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            ViewExam texam = new ViewExam();
                            texam.IExamId = (int)reader["IExamId"];
                            texam.SubjectId = (int)reader["SubjectId"];
                            texam.Class = (int)reader["Class"];
                            texam.FullMark = (int)reader["FullMark"];
                            texam.PassMark = (int)reader["PassMark"];
                            texam.ExamDate = (DateTime)reader["ExamDate"];
                            texam.StartTime = (TimeSpan)reader["StartTime"];
                            texam.ExamDescription = (string)reader["ExamDescription"];
                            texam.SubjectName = (string)reader["SubjectName"];
                            texam.ExamDuration = (int)reader["ExamDuration"];
                            texam.ExamStatus = (int)reader["ExamStatus"];
                            list.Add(texam);
                        }
                        //Close the reader if a matching record is found.
                        reader.Close();

                    }


                }

            }

            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Exam View of Teachers :" + ex.Message;
            }
            finally
            {

                connection.Close();
            }

            return list;
        }

        public Response Delete(int IExamId, SqlConnection connection)
        {
            Exam exam = new Exam();
            Response response = new Response();
            if (connection == null)
            {
                response.StatusCode = 500;
                response.StatusMessage = "SQL Connection is null";
                return response;
            }
            try
            {
                connection.Open();
                string queryExam = $"Delete FROM IExam WHERE IExamId = {IExamId}";

                using (SqlCommand cmdUser = new SqlCommand(queryExam, connection))
                {
                    //Add values to the parameters in the query.
                    //cmdUser.Parameters.AddWithValue("@NoticeId", notice.NoticeId);



                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Delete Exam table failed");
                    }
                    else
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Deletion Successful";
                        return response;
                    }
                }
            }

            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Exam Delete: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return response;
        }


        public Response UpdateExamStatus(int IExamId, SqlConnection connection)
        {
            ExamSt examSt = new ExamSt();
            Response response = new Response();
            if (connection == null)
            {
                response.StatusCode = 500;
                response.StatusMessage = "SQL Connection is null";
            }
            try
            {
                //Open the connection to the database.
                connection.Open();
                string queryUExam = $@"UPDATE IExam SET  ExamStatus  = 1  WHERE IExamId  = {IExamId} ";

                using (SqlCommand cmdUser = new SqlCommand(queryUExam, connection))
                {
                    ////Add values to the parameters in the query.
                    //UpdateExam updateExam = new UpdateExam();
                    //cmdUser.Parameters.AddWithValue("@IExamId", updateExam.IExamId);
                    //cmdUser.Parameters.AddWithValue("@SubjectId", updateExam.SubjectId);
                    //cmdUser.Parameters.AddWithValue("@Class", updateExam.Class);
                    //cmdUser.Parameters.AddWithValue("@FullMark", updateExam.FullMark);
                    //cmdUser.Parameters.AddWithValue("@PassMark", updateExam.PassMark);
                    //cmdUser.Parameters.AddWithValue("@ExamDate", updateExam.ExamDate);
                    //cmdUser.Parameters.AddWithValue("@StartTime", updateExam.StartTime);
                    //cmdUser.Parameters.AddWithValue("@ExamDuration", updateExam.ExamDuration);
                    //cmdUser.Parameters.AddWithValue("@ExamDescription", updateExam.ExamDescription);


                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Update into Exam Failed");
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
                response.StatusMessage = "An error occurred during Exam Update: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return response;
        }


        public Response UpdateExamStatusFalse(int IExamId, SqlConnection connection)
        {
            ExamSt examSt = new ExamSt();
            Response response = new Response();
            if (connection == null)
            {
                response.StatusCode = 500;
                response.StatusMessage = "SQL Connection is null";
            }
            try
            {
                //Open the connection to the database.
                connection.Open();
                string queryUExam = $@"UPDATE IExam SET  ExamStatus  = 0  WHERE IExamId  = {IExamId} ";

                using (SqlCommand cmdUser = new SqlCommand(queryUExam, connection))
                {
                    ////Add values to the parameters in the query.
                    //UpdateExam updateExam = new UpdateExam();
                    //cmdUser.Parameters.AddWithValue("@IExamId", updateExam.IExamId);
                    //cmdUser.Parameters.AddWithValue("@SubjectId", updateExam.SubjectId);
                    //cmdUser.Parameters.AddWithValue("@Class", updateExam.Class);
                    //cmdUser.Parameters.AddWithValue("@FullMark", updateExam.FullMark);
                    //cmdUser.Parameters.AddWithValue("@PassMark", updateExam.PassMark);
                    //cmdUser.Parameters.AddWithValue("@ExamDate", updateExam.ExamDate);
                    //cmdUser.Parameters.AddWithValue("@StartTime", updateExam.StartTime);
                    //cmdUser.Parameters.AddWithValue("@ExamDuration", updateExam.ExamDuration);
                    //cmdUser.Parameters.AddWithValue("@ExamDescription", updateExam.ExamDescription);


                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Update into Exam Failed");
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
                response.StatusMessage = "An error occurred during Exam Update: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return response;
        }


    }
}

using ETSystem.Model.SampleQuestion;
using ETSystem.Model;
using System.Data.SqlClient;
using ETS.web.Model.EQuestions;
using ETS.web.Interface;
using ETSystem.Model.Notice;
using ETS.web.Model.TExam;

namespace ETS.web.DAL
{
    public class EQuestionsRepository : IEQuestionsRepository
    {
        public Response Create(CreateQuestions createQuestions, SqlConnection connection)
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
                string queryQuestions = "INSERT INTO EQuestions ( IExamId, IMark, Question, OptionA, OptionB, OptionC, OptionD, CorrectAnswer ) VALUES(@IExamId, @IMark, @Question, @OptionA, @OptionB, @OptionC, @OptionD, @CorrectAnswer)";

                using (SqlCommand cmdUser = new SqlCommand(queryQuestions, connection))
                {
                    //Add values to the parameters in the query.
                    cmdUser.Parameters.AddWithValue("@IExamId", createQuestions.IExamId);
                    cmdUser.Parameters.AddWithValue("@IMark", createQuestions.IMark);
                    cmdUser.Parameters.AddWithValue("@Question", createQuestions.Question);
                    cmdUser.Parameters.AddWithValue("@OptionA", createQuestions.OptionA);
                    cmdUser.Parameters.AddWithValue("@OptionB", createQuestions.OptionB);
                    cmdUser.Parameters.AddWithValue("@OptionC", createQuestions.OptionC);
                    cmdUser.Parameters.AddWithValue("@OptionD", createQuestions.OptionD);
                    cmdUser.Parameters.AddWithValue("@CorrectAnswer", createQuestions.CorrectAnswer);


                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Insert into Exam Question Failed");
                    }
                    else
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Exam Question Creation Successful";
                        return response;
                    }
                }
            }

            catch (SqlException ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Exam Question Creation: " + ex.Message;
            }
            finally
            {

                connection.Close();
            }

            return response;
        }

        public Response Update(UpdateQuestions updateQuestions, SqlConnection connection)
        {
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
                string queryUpdate = "UPDATE EQuestions SET  IMark = @IMark, Question = @Question, OptionA = @OptionA, OptionB = @OptionB, OptionC = @OptionC, OptionD = @OptionD, CorrectAnswer = @CorrectAnswer WHERE EQuestionId = @EQuestionId ";

                using (SqlCommand cmdUser = new SqlCommand(queryUpdate, connection))
                {
                    //Add values to the parameters in the query.
                    cmdUser.Parameters.AddWithValue("@EQuestionId", updateQuestions.EQuestionId);
                    cmdUser.Parameters.AddWithValue("@IMark", updateQuestions.IMark);
                    cmdUser.Parameters.AddWithValue("@Question", updateQuestions.Question);
                    cmdUser.Parameters.AddWithValue("@OptionA", updateQuestions.OptionA);
                    cmdUser.Parameters.AddWithValue("@OptionB", updateQuestions.OptionB);
                    cmdUser.Parameters.AddWithValue("@OptionC", updateQuestions.OptionC);
                    cmdUser.Parameters.AddWithValue("@OptionD", updateQuestions.OptionD);
                    cmdUser.Parameters.AddWithValue("@CorrectAnswer", updateQuestions.CorrectAnswer);


                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Update into Exam Question Failed");
                    }
                    else
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Updation Successful";
                    }
                }
            }

            catch (SqlException ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Exam Question Updation: " + ex.Message;
            }
            finally
            {

                connection.Close();
            }

            return response;

        }



        //Question View for Teacher
        public List<ViewEQuestions> ViewQuestions(int IExamId, SqlConnection connection)
        {
            Response response = new Response();
            List<ViewEQuestions> list = new List<ViewEQuestions>();

            try
            {
                connection.Open();

                string queryEQuestion = $@"SELECT * FROM EQuestions WHERE IExamId = {IExamId} AND QuestionStatus = 0 OR QuestionStatus = 1";

                using (SqlCommand cmdUser = new SqlCommand(queryEQuestion, connection))
                {
                    SqlDataReader reader = cmdUser.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        response.StatusCode = 401;
                        response.StatusMessage = "Exam Questions Failed";
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            ViewEQuestions viewQuestions = new ViewEQuestions();

                            viewQuestions.IMark = (int)reader["IMark"];
                            viewQuestions.EQuestionId = (int)reader["EQuestionId"];
                            viewQuestions.IExamId = (int)reader["IExamId"];
                            viewQuestions.Question = (string)reader["Question"];
                            viewQuestions.OptionA = (string)reader["OptionA"];
                            viewQuestions.OptionB = (string)reader["OptionB"];
                            viewQuestions.OptionC = (string)reader["OptionC"];
                            viewQuestions.OptionD = (string)reader["OptionD"];
                            viewQuestions.CorrectAnswer = (string)reader["CorrectAnswer"];
                            viewQuestions.QuestionStatus = (int)reader["QuestionStatus"];
                            list.Add(viewQuestions);
                        }

                        reader.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Exam Question View: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return list;
        }


        public Response Delete(int EQuestionId, SqlConnection connection)
        {
            ViewEQuestions viewEQuestions = new ViewEQuestions();
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
                string queryQn = $"Delete FROM EQuestions WHERE EQuestionId = {EQuestionId}";

                using (SqlCommand cmdUser = new SqlCommand(queryQn, connection))
                {
                    //Add values to the parameters in the query.
                    //cmdUser.Parameters.AddWithValue("@NoticeId", notice.NoticeId);



                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Delete Exam Question  failed");
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
                response.StatusMessage = "An error occurred during Exam Question Delete: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return response;
        }


        //Question View for Students.
        public List<ViewEQuestions> ViewQuestion(int IExamId, SqlConnection connection)
        {
            Response response = new Response();
            List<ViewEQuestions> list = new List<ViewEQuestions>();

            try
            {
                connection.Open();

                string queryEQuestion = $@"SELECT * FROM EQuestions WHERE IExamId = {IExamId}";

                using (SqlCommand cmdUser = new SqlCommand(queryEQuestion, connection))
                {
                    SqlDataReader reader = cmdUser.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        response.StatusCode = 401;
                        response.StatusMessage = "Exam Questions Failed";
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            ViewEQuestions viewQuestions = new ViewEQuestions();

                            viewQuestions.IMark = (int)reader["IMark"];
                            viewQuestions.IExamId = (int)reader["IExamId"];
                            viewQuestions.EQuestionId = (int)reader["EQuestionId"];
                            viewQuestions.Question = (string)reader["Question"];
                            viewQuestions.OptionA = (string)reader["OptionA"];
                            viewQuestions.OptionB = (string)reader["OptionB"];
                            viewQuestions.OptionC = (string)reader["OptionC"];
                            viewQuestions.OptionD = (string)reader["OptionD"];
                            viewQuestions.CorrectAnswer = (string)reader["CorrectAnswer"];
                            viewQuestions.QuestionStatus = (int)reader["QuestionStatus"];
                            list.Add(viewQuestions);
                        }

                        reader.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Exam Question View: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return list;
        }

        public ViewExams GetById(int IExamId, SqlConnection connection)
        {
            Response response = new Response();
            ViewExams viewExams = new ViewExams();

            string query = $@"SELECT IExam.IExamId, IExam.Class, IExam.FullMark, IExam.PassMark, IExam.ExamDate, IExam.StartTime, IExam.ExamDuration, IExam.ExamDescription, Subjects.SubjectName
            FROM IExam
            JOIN Subjects ON IExam.SubjectId = Subjects.SubjectId
            WHERE IExam.IExamId = '{IExamId}'
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

                        viewExams.IExamId = (int)reader["IExamId"];
                        viewExams.Class = (int)reader["Class"];
                        viewExams.FullMark = (int)reader["FullMark"];
                        viewExams.PassMark = (int)reader["PassMark"];
                        viewExams.ExamDate = (DateTime)reader["ExamDate"];
                        viewExams.StartTime = (TimeSpan)reader["StartTime"];
                        viewExams.ExamDescription = (string)reader["ExamDescription"];
                        viewExams.ExamDuration = (int)reader["ExamDuration"];


                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occured during Examination View:" + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return viewExams;
        }

        public Response UpdateQnStatus (QnStatus qnStatus, SqlConnection connection)
        {
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
                string queryUpdateQn = $@"UPDATE EQuestions SET QuestionStatus = 1 WHERE IExamId = @IExamId";

                using (SqlCommand cmdUser = new SqlCommand(queryUpdateQn, connection))
                {
                    //Add values to the parameters in the query.
                    cmdUser.Parameters.AddWithValue("@IExamId", qnStatus.IExamId);

                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Update into Exam Question  Status Failed");
                    }
                    else
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Updation Status Successful";
                    }
                }
            }

            catch (SqlException ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Exam Question Status Updation: " + ex.Message;
            }
            finally
            {

                connection.Close();
            }

            return response;

        }

        public Response UpdateQnStatusF(QnStatus qnStatus, SqlConnection connection)
        {
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
                string queryUpdateQn = $@"UPDATE EQuestions SET QuestionStatus = 0 WHERE IExamId = @IExamId";

                using (SqlCommand cmdUser = new SqlCommand(queryUpdateQn, connection))
                {
                    //Add values to the parameters in the query.
                    cmdUser.Parameters.AddWithValue("@IExamId", qnStatus.IExamId);

                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Update into Exam Question  Status Failed");
                    }
                    else
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Updation Status Successful";
                    }
                }
            }

            catch (SqlException ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Exam Question Status Updation: " + ex.Message;
            }
            finally
            {

                connection.Close();
            }

            return response;

        }



    }
}

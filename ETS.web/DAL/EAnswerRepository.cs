using ETS.web.Interface;
using ETS.web.Model.EAnswer;
using ETS.web.Model.EQuestions;
using ETSystem.Model;
using ETSystem.Model.Message;
using ETSystem.Model.User;
using System.Data.SqlClient;

namespace ETS.web.DAL
{
    public class EAnswerRepository : IEAnswerRepository
    {
        public Response Create(CreateAnswer createAnswer, SqlConnection connection)
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
                connection.Open();
                foreach (var answer in createAnswer.AnswerList)
                {
                    //Open the connection to the database.
                    //connection.Open();
                    string queryQuestions = "INSERT INTO Answer ( UserId, EQuestionId, Answer,IsSubmitted ) VALUES (@UserId, @EQuestionId, @Answer,@IsSubmitted)";

                    using (SqlCommand cmdUser = new SqlCommand(queryQuestions, connection))
                    {

                        cmdUser.Parameters.AddWithValue("@UserId", createAnswer.UserId);
                        cmdUser.Parameters.AddWithValue("@EQuestionId", answer.EQuestionId);
                        cmdUser.Parameters.AddWithValue("@Answer", answer.Answer);
                        cmdUser.Parameters.AddWithValue("@IsSubmitted", answer.IsSubmitted);

                        //Add values to the parameters in the query.
                        //cmdUser.Parameters.AddWithValue("@UserId", createAnswer.UserId);
                        //cmdUser.Parameters.AddWithValue("@EQuestionId", createAnswer.EQuestionId);
                        //cmdUser.Parameters.AddWithValue("@Answer", createAnswer.Answer);


                        int j = cmdUser.ExecuteNonQuery();

                        if (j <= 0)
                        {
                            //Throw an exception if the insertion failed.
                            throw new Exception("Insert into Exam Answer Failed");
                        }
                        else
                        {
                            response.StatusCode = 200;
                            response.StatusMessage = "Exam Answer Creation Successful";

                        }
                    }
                
                }
            }

            catch (SqlException ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Exam Answer Creation: " + ex.Message;
            }
            finally
            {

                connection.Close();
            }

            return response;
        }

        public List<ViewAnswer> GetById(int IExamId, int UserId, SqlConnection connection)
        {
            Response response = new Response();
            List<ViewAnswer> list = new List<ViewAnswer>();

            //string query = $@"SELECT eq.Question,eq.IMark, eq.OptionA, eq.OptionB, eq.OptionC, eq.OptionD, eq.CorrectAnswer, a.Answer
            //FROM EQuestions eq
            //INNER JOIN Answer a ON eq.EQuestionId = a.EQuestionId
            //WHERE eq.IExamId = {IExamId} AND a.UserId = {UserId}";

            string query = $@"SELECT s.SubjectName, i.Class, i.FullMark, i.PassMark, i.ExamDate, i.StartTime, i.ExamDuration, i.ExamDescription, eq.Question, eq.IMark, eq.OptionA, eq.OptionB, eq.OptionC, eq.OptionD, eq.CorrectAnswer, a.Answer
                FROM IExam i
                INNER JOIN EQuestions eq ON i.IExamId = eq.IExamId
                INNER JOIN Answer a ON eq.EQuestionId = a.EQuestionId
                INNER JOIN Subjects s ON i.SubjectId = s.SubjectId
                WHERE i.IExamId = {IExamId} AND a.UserId = {UserId}";

            try
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ViewAnswer viewAns = new ViewAnswer();


                        viewAns.Question = (string)reader["Question"];
                        viewAns.IMark = (int)reader["IMark"];
                        viewAns.OptionA = (string)reader["OptionA"];
                        viewAns.OptionB = (string)reader["OptionB"];
                        viewAns.OptionC = (string)reader["OptionC"];
                        viewAns.OptionD = (string)reader["OptionD"];
                        viewAns.Answer = (string)reader["Answer"];
                        viewAns.CorrectAnswer = (string)reader["CorrectAnswer"];
                        //viewAns.Answer = (char)reader["Answer"];
                        list.Add(viewAns);

                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during view Answer: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return list;
        }


        //public List<ViewAnswer> GetById(int IExamId, int UserId, SqlConnection connection)
        //{
        //    Response response = new Response();
        //    List<ViewAnswer> list = new List<ViewAnswer>();
        //    Dictionary<int, ExamDetails> examDetailsMap = new Dictionary<int, ExamDetails>();

        //    string query = @"SELECT i.IExamId, s.SubjectName, i.Class, i.FullMark, i.PassMark, i.ExamDate, i.StartTime, i.ExamDuration, i.ExamDescription, eq.Question, eq.IMark, eq.OptionA, eq.OptionB, eq.OptionC, eq.OptionD, eq.CorrectAnswer, a.Answer
        //FROM IExam i
        //INNER JOIN EQuestions eq ON i.IExamId = eq.IExamId
        //INNER JOIN Answer a ON eq.EQuestionId = a.EQuestionId
        //INNER JOIN Subjects s ON i.SubjectId = s.SubjectId
        //WHERE i.IExamId = @IExamId AND a.UserId = @UserId";

        //    try
        //    {
        //        connection.Open();

        //        using (SqlCommand cmd = new SqlCommand(query, connection))
        //        {
        //            cmd.Parameters.AddWithValue("@IExamId", IExamId);
        //            cmd.Parameters.AddWithValue("@UserId", UserId);

        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {

        //                    ExamDetails examDetails;
        //                    if (!examDetailsMap.TryGetValue(examId, out examDetails))
        //                    {
        //                        ExamDetails examDetails = new ExamDetails();
        //                        {
        //                            //viewAns.Question = (string)reader["Question"];
        //                            examDetails.SubjectName = (string)reader["SubjectName"];
        //                            examDetails.Class = (int)reader["Class"];
        //                            examDetails.FullMark = (int)reader["FullMark"];
        //                            examDetails.PassMark = (int)reader["PassMark"];
        //                            examDetails.ExamDate = (string)reader["ExamDate"];
        //                            examDetails.StartTime = (string)reader["StartTime"];
        //                            examDetails.ExamDuration = (string)reader["ExamDuration"];
        //                            examDetails.ExamDescription = (string)reader["ExamDescription"];
        //                        };

        //                        examDetailsMap.Add(examDetails);
        //                    }

        //                    ViewAnswer viewAns = new ViewAnswer
        //                    {
        //                        Question = reader.GetString(reader.GetOrdinal("Question")),
        //                        IMark = reader.GetInt32(reader.GetOrdinal("IMark")),
        //                        OptionA = reader.GetString(reader.GetOrdinal("OptionA")),
        //                        OptionB = reader.GetString(reader.GetOrdinal("OptionB")),
        //                        OptionC = reader.GetString(reader.GetOrdinal("OptionC")),
        //                        OptionD = reader.GetString(reader.GetOrdinal("OptionD")),
        //                        Answer = reader.GetString(reader.GetOrdinal("Answer")),
        //                        CorrectAnswer = reader.GetString(reader.GetOrdinal("CorrectAnswer")),
        //                        ExamDetails = examDetails
        //                    };

        //                    list.Add(viewAns);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.StatusCode = 500;
        //        response.StatusMessage = "An error occurred during view Answer: " + ex.Message;
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }

        //    return list;
        //}


        //public Check CheckAnswer(int UserId, int EQuestionId, SqlConnection connection)
        //{
        //    Response response = new Response();
        //    Check check = new Check();
        //    if (connection == null)
        //    {
        //        response.StatusCode = 500;
        //        response.StatusMessage = "SQL Connection is null";
        //    }
        //    try
        //    {
        //        //Open the connection to the database.
        //        connection.Open();
        //        string queryAnswer = $@"SELECT EQuestionId, UserId FROM Answer WHERE UserId = {UserId}  AND EQuestionId = {EQuestionId}  AND IsSubmitted = 1 ";

        //        using (SqlCommand cmdUser = new SqlCommand(queryAnswer, connection))
        //        {
        //            //Add values to the parameters in the query.
        //            //cmdUser.Parameters.AddWithValue("@UserId", check.UserId);
        //            //cmdUser.Parameters.AddWithValue("@EQuestionId", check.EQuestionId);
        //            //cmdUser.Parameters.AddWithValue("@Answer", createAnswer.Answer);


        //            int j = cmdUser.ExecuteNonQuery();
        //            if (j <= 0)
        //            {
        //                //Throw an exception if the insertion failed.
        //                throw new Exception("Insert into Exam Answer Failed");
        //            }
        //            else
        //            {
        //                response.StatusCode = 200;
        //                response.StatusMessage = "Exam Answer Creation Successful";
        //            }
        //        }
        //    }

        //    catch (SqlException ex)
        //    {
        //        response.StatusCode = 500;
        //        response.StatusMessage = "An error occurred during Exam Answer Creation: " + ex.Message;
        //    }
        //    finally
        //    {

        //        connection.Close();
        //    }

        //    return check;
        //}


    }
}

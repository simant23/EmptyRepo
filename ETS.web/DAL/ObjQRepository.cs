using ETS.web.Model.Question;
using ETSystem.Model;
using ETSystem.Model.Notice;
using ETSystem.Model.Question;
using ETSystem.Model.QuestionPaper;
using ETSystem.Model.SampleQuestion;
using ETSystem.Repository;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ETS.web.DAL
{
    public class ObjQRepository : IObjQRepository
    {
        //Create Question
        public Response Create(ObjQ objective, SqlConnection connection)
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
                string queryObjective = "INSERT INTO Questions ( PaperId, Marks, QuestionText, A, B, C, D, Answer ) VALUES(@PaperId, @Marks, @QuestionText, @A, @B, @C, @D, @Answer)";

                using (SqlCommand cmdUser = new SqlCommand(queryObjective, connection))
                {
                    //Add values to the parameters in the query.
                    //cmdUser.Parameters.AddWithValue("@QuestionId", objective.QuestionId);
                    //cmdUser.Parameters.AddWithValue("@UserId", objective.UserId);
                    cmdUser.Parameters.AddWithValue("@PaperId", objective.PaperId);
                    cmdUser.Parameters.AddWithValue("@Marks", objective.Marks);
                    //cmdUser.Parameters.AddWithValue("@SubjectId", objective.SubjectId);
                    //cmdUser.Parameters.AddWithValue("@QuestionNumber", objective.QuestionNumber);
                    cmdUser.Parameters.AddWithValue("@QuestionText", objective.QuestionText);
                    cmdUser.Parameters.AddWithValue("@A", objective.A);
                    cmdUser.Parameters.AddWithValue("@B", objective.B);
                    cmdUser.Parameters.AddWithValue("@C", objective.C);
                    cmdUser.Parameters.AddWithValue("@D", objective.D);
                    cmdUser.Parameters.AddWithValue("@Answer", objective.Answer);


                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Insert into Objective Question table failed");
                    }
                    else
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Objective Question Creation Successful";
                        return response;
                    }
                }
            }

            catch (SqlException ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Sample Question Creation: " + ex.Message;
            }
            finally
            {

                connection.Close();
            }

            return response;
        }
        public List<ViewQuestions> ViewQuestionsById(int PaperId, SqlConnection connection)
        {
            Response response = new Response();
            List<ViewQuestions> viewQuestionsList = new List<ViewQuestions>();

            try
            {
                connection.Open();

                string queryQuestion = $@"SELECT 
            q.Marks,
            q.QuestionText,
            q.A,
            q.B,
            q.C,
            q.D,
            q.Answer,
            q.QuestionId
        FROM Questions q
        WHERE q.PaperId = {PaperId}";

                using (SqlCommand cmdUser = new SqlCommand(queryQuestion, connection))
                {
                    SqlDataReader reader = cmdUser.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        response.StatusCode = 401;
                        response.StatusMessage = "Questions Failed";
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            ViewQuestions viewQuestions = new ViewQuestions();

                            viewQuestions.Marks = (int)reader["Marks"];
                            viewQuestions.QuestionId = (int)reader["QuestionId"];
                            viewQuestions.QuestionText = (string)reader["QuestionText"];
                            viewQuestions.A = (string)reader["A"];
                            viewQuestions.B = (string)reader["B"];
                            viewQuestions.C = (string)reader["C"];
                            viewQuestions.D = (string)reader["D"];
                            viewQuestions.Answer = (string)reader["Answer"];

                            viewQuestionsList.Add(viewQuestions);
                        }

                        reader.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occured during Sample Question View:"+ex.Message;

            }
            finally
            {
                connection.Close();
            }

            return viewQuestionsList;
        }

        public Response UpdateQ(UpdateQ obj, SqlConnection connection)
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
                string queryUpdate = "UPDATE Questions SET  Marks = @Marks, QuestionText = @QuestionText, A = @A, B = @B, C = @C, D = @D, Answer = @Answer WHERE QuestionId = @QuestionId";

                using (SqlCommand cmdUser = new SqlCommand(queryUpdate, connection))
                {
                    //Add values to the parameters in the query.
                    cmdUser.Parameters.AddWithValue("@QuestionId", obj.QuestionId);
                    cmdUser.Parameters.AddWithValue("@Marks", obj.Marks);
                    cmdUser.Parameters.AddWithValue("@QuestionText", obj.QuestionText);
                    cmdUser.Parameters.AddWithValue("@A", obj.A);
                    cmdUser.Parameters.AddWithValue("@B", obj.B);
                    cmdUser.Parameters.AddWithValue("@C", obj.C);
                    cmdUser.Parameters.AddWithValue("@D", obj.D);
                    cmdUser.Parameters.AddWithValue("@Answer",obj.Answer);


                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Update into Question table failed");
                    }
                    else
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Updation Successful";
                        return response;
                    }
                }
            }

            catch (SqlException ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Objective Question Updation: " + ex.Message;
            }
            finally
            {

                connection.Close();
            }

            return response;

        }

        public Response DeleteQn(int QuestionId, SqlConnection connection)
        {
            ViewQuestions viewQn = new ViewQuestions();
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
                string queryDelete = $"Delete FROM Questions WHERE QuestionId = {QuestionId}";

                using (SqlCommand cmdUser = new SqlCommand(queryDelete, connection))
                {
                    //Add values to the parameters in the query.
                    //cmdUser.Parameters.AddWithValue("@NoticeId", notice.NoticeId);



                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Delete Question failed");
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
                response.StatusMessage = "An error occurred during Question Delete: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return response;
        }








    }


}

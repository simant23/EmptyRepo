using ETSystem.Model;
using ETSystem.Model.Notice;
using ETSystem.Model.Question;
using ETSystem.Model.QuestionPaper;
using ETSystem.Model.SampleQuestion;
using ETSystem.Repository;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ETS.web.DAL
{
    public class QuestionPaperRepository : IQuestionPaperRepository
    {
        //Create Question Paper
        public Response CreatePaper(QuestionPaper questionPaper, SqlConnection connection)
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
                // Open the connection to the database
                connection.Open();

                // Define the SQL query to insert a new record into the QuestionPaper table
                string queryPaper = "INSERT INTO QuestionPaper (SubjectId, UserId, Class, Description, TotalMarks, TimeAllowedMinutes, CreatedDate) " +
                                    "VALUES (@SubjectId, @UserId, @Class, @Description, @TotalMarks, @TimeAllowedMinutes, @CreatedDate); " +
                                    "SELECT CAST(SCOPE_IDENTITY() AS INT)";

                // Create a new SqlCommand object with the SQL query and the SQL connection
                using (SqlCommand cmd = new SqlCommand(queryPaper, connection))
                {
                    // Set the parameter values for the SQL query
                    cmd.Parameters.AddWithValue("@SubjectId", questionPaper.SubjectId);
                    cmd.Parameters.AddWithValue("@UserId", questionPaper.UserId);
                    cmd.Parameters.AddWithValue("@Class", questionPaper.Class);
                    cmd.Parameters.AddWithValue("@Description", questionPaper.Description);
                    cmd.Parameters.AddWithValue("@TotalMarks", questionPaper.TotalMarks);
                    cmd.Parameters.AddWithValue("@TimeAllowedMinutes", questionPaper.TimeAllowedMinutes);
                    cmd.Parameters.AddWithValue("@CreatedDate", questionPaper.CreatedDate);

                    // Execute the SQL query and retrieve the newly inserted PaperId
                    int paperId = (int)cmd.ExecuteScalar();

                    // Set the response status and message
                    response.StatusCode = 200;
                    response.StatusMessage = "Question paper created successfully";
                    return response;

                    // Set the PaperId value in the response object
                    //questionPaper.PaperId = paperId;
                    //response.PaperId = paperId;
                }
            }
            catch (SqlException ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Question Paper Creation: " + ex.Message;
            }
            finally
            {
                // Close the database connection


                connection.Close();

            }

            return response;
        }

        public List<ViewPaper> ViewPaperById(int Class, SqlConnection connection)
        {
            List<ViewPaper> list = new List<ViewPaper>();
            Response response = new Response();
            try
            {
                //Open the connection to the database.
                connection.Open();
                string queryPaper = $@"SELECT 
                    s.SubjectName, 
                    qp.PaperId,                   
                    qp.Class,
                    qp.Description, 
                    qp.TotalMarks, 
                    qp.TimeAllowedMinutes, 
                    qp.CreatedDate
                FROM QuestionPaper qp
                JOIN Subjects s ON qp.SubjectId = s.SubjectId
                JOIN SchoolUser su ON qp.UserId = su.UserId
                WHERE  qp.Class = {Class} ";

                using (SqlCommand cmdUser = new SqlCommand(queryPaper, connection))
                {
                    //Add values to the parameters in the query.
                    //cmdUser.Parameters.AddWithValue("@PaperId", PaperId);




                    SqlDataReader reader = cmdUser.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        response.StatusCode = 401;
                        response.StatusMessage = "Question Paper Failed";
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            //Notice notice = new NoticeNoticeId
                            ViewPaper viewPaper = new ViewPaper();
                            viewPaper.PaperId = (int)reader["PaperId"];
                            viewPaper.Class = (int)reader["Class"];
                            viewPaper.SubjectName = (string)reader["SubjectName"];
                            //viewPaper.CreatedBy = (string)reader["CreatedBy"];
                            viewPaper.CreatedDate = (DateTime)reader["CreatedDate"];
                            viewPaper.Description = (string)reader["Description"];
                            viewPaper.TotalMarks = (int)reader["TotalMarks"];
                            viewPaper.TimeAllowedMinutes = (int)reader["TimeAllowedMinutes"];
                            list.Add(viewPaper);

                        }
                        //Close the reader if a matching record is found.
                        reader.Close();
                    }
                }


            }

            catch (Exception ex)
            {
                response.StatusCode = 500;  
                response.StatusMessage = "An error occured during Question Paper View:"+ex.Message;
            }
            finally
            {

                connection.Close();
            }

            return list;
        }

        public List<ViewPaper> GetAll(SqlConnection connection)
        {
            List<ViewPaper> list = new List<ViewPaper>();
            Response response = new Response();
            try
            {
                // Notice notice= new Notice();

                //Open the connection to the database.
                connection.Open();
                string queryQuestion = "SELECT s.SubjectName, qp.Class, qp.UserId,qp.PaperId,qp.Description, qp.TotalMarks, qp.TimeAllowedMinutes, qp.CreatedDate FROM QuestionPaper qp JOIN Subjects s ON qp.SubjectId = s.SubjectId JOIN SchoolUser su ON qp.UserId = su.UserId ORDER BY CreatedDate DESC";

                using (SqlCommand cmdUser = new SqlCommand(queryQuestion, connection))
                {
                    //Add values to the parameters in the query.
                    SqlDataReader reader = cmdUser.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        response.StatusCode = 401;
                        response.StatusMessage = "Question Paper Failed";
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            ViewPaper questionpaper = new ViewPaper();
                            questionpaper.SubjectName = (string)reader["SubjectName"];
                            questionpaper.Class = (int)reader["Class"];
                            questionpaper.UserId = (int)reader["UserId"];
                            //questionpaper.CreatedBy = (string)reader["CreatedBy"];
                            questionpaper.PaperId = (int)reader["PaperId"];
                            questionpaper.Description = (string)reader["Description"];
                            questionpaper.TotalMarks = (int)reader["TotalMarks"];
                            questionpaper.TimeAllowedMinutes = (int)reader["TimeAllowedMinutes"];
                            questionpaper.CreatedDate = (DateTime)reader["CreatedDate"];
                            list.Add(questionpaper);
                        }
                        //Close the reader if a matching record is found.
                        reader.Close();

                    }


                }

            }

            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Question Paper List View: " + ex.Message;
            }
            finally
            {

                connection.Close();
            }

            return list;
        }

        public Response UpdatePaper(Paper paper, SqlConnection connection)
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
                string queryp = "UPDATE QuestionPaper SET Description = @Description,UserId = @UserId, TotalMarks = @TotalMarks, TimeAllowedMinutes = @TimeAllowedMinutes, Class = @Class  WHERE PaperId = @PaperId";

                using (SqlCommand cmdUser = new SqlCommand(queryp, connection))
                {
                    //Add values to the parameters in the query.
                    cmdUser.Parameters.AddWithValue("@PaperId", paper.PaperId);
                    cmdUser.Parameters.AddWithValue("@UserId", paper.UserId);
                    cmdUser.Parameters.AddWithValue("@Class", paper.Class);
                    //cmdUser.Parameters.AddWithValue("@SubjectId", paper.SubjectId);
                    cmdUser.Parameters.AddWithValue("@Description", paper.Description);
                    cmdUser.Parameters.AddWithValue("@TotalMarks", paper.TotalMarks);
                    cmdUser.Parameters.AddWithValue("@TimeAllowedMinutes", paper.TimeAllowedMinutes);


                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Update into Question Paper  failed");
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
                response.StatusMessage = "An error occurred during Question Paper Update: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return response;
        }
        public Response DeletePaper(int PaperId, SqlConnection connection)
        {
            ViewPaper viewPaper = new ViewPaper();
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
                string queryDelete = $"Delete FROM QuestionPaper WHERE PaperId = {PaperId}";

                using (SqlCommand cmdUser = new SqlCommand(queryDelete, connection))
                {
                    //Add values to the parameters in the query.
                    //cmdUser.Parameters.AddWithValue("@NoticeId", notice.NoticeId);



                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Delete Paper table failed");
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
                response.StatusMessage = "An error occurred during Question Paper Delete: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return response;
        }



    }
}

using ETS.web.Interface;
using ETS.web.Model.EAnswer;
using ETS.web.Model.EDetails;
using ETS.web.Model.EQuestions;
using ETSystem.Model;
using System.Data.SqlClient;

namespace ETS.web.DAL
{
    public class EDetailsRepository : IEDetailsRepository
    {
        public Response Create(CEDetails cEDetails, SqlConnection connection)
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
                string queryDetails = "INSERT INTO QPattern ( IExamId, Chapter, TQuestions, MarkAllocated ) VALUES (@IExamId, @Chapter, @TQuestions, @MarkAllocated)";

                using (SqlCommand cmdUser = new SqlCommand(queryDetails, connection))
                {
                    //Add values to the parameters in the query.
                    cmdUser.Parameters.AddWithValue("@IExamId", cEDetails.IExamId);
                    cmdUser.Parameters.AddWithValue("@Chapter", cEDetails.Chapter);
                    cmdUser.Parameters.AddWithValue("@TQuestions", cEDetails.TQuestions);
                    cmdUser.Parameters.AddWithValue("@MarkAllocated", cEDetails.MarkAllocated);


                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Insert into Exam Details Failed");
                    }
                    else
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Exam Details Creation Successful";
                        return response;
                    }
                }
            }

            catch (SqlException ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Exam Details Creation: " + ex.Message;
            }
            finally
            {

                connection.Close();
            }

            return response;
        }

        public Response Update(UEDetails uEDetails, SqlConnection connection)
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
                string queryUpdate = "UPDATE QPattern SET  Chapter = @Chapter, TQuestions = @TQuestions, MarkAllocated = @MarkAllocated WHERE QPatternId = @QPatternId";

                using (SqlCommand cmdUser = new SqlCommand(queryUpdate, connection))
                {
                    //Add values to the parameters in the query.
                    cmdUser.Parameters.AddWithValue("@QPatternId", uEDetails.QPatternId);
                    cmdUser.Parameters.AddWithValue("@Chapter", uEDetails.Chapter);
                    cmdUser.Parameters.AddWithValue("@TQuestions", uEDetails.TQuestions);
                    cmdUser.Parameters.AddWithValue("@MarkAllocated", uEDetails.MarkAllocated);


                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Update into Exam Details Failed");
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
                response.StatusMessage = "An error occurred during Exam Details Updation: " + ex.Message;
            }
            finally
            {

                connection.Close();
            }

            return response;

        }

        public List<VEDetails> View(int IExamId, SqlConnection connection)
        {
            Response response = new Response();
            List<VEDetails> list = new List<VEDetails>();

            try
            {
                connection.Open();

                string queryEDet = $@"SELECT 
                    Chapter,
                    TQuestions,
                    MarkAllocated,
                    QPatternId
                FROM QPattern 
                WHERE IExamId = {IExamId}";

                using (SqlCommand cmdUser = new SqlCommand(queryEDet, connection))
                {
                    SqlDataReader reader = cmdUser.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        response.StatusCode = 401;
                        response.StatusMessage = "Exam Details Failed";
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            VEDetails vEDetails = new VEDetails()
                            {
                                QPatternId = (int)reader["QPatternId"],
                                Chapter = (string)reader["Chapter"],
                                TQuestions = (int)reader["TQuestions"],
                                MarkAllocated = (int)reader["MarkAllocated"],
                            };
                            list.Add(vEDetails);

                            
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Exam Details View: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return list;
        }
    }
}

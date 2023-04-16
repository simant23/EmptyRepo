using ETSystem.Model;
using ETSystem.Model.Message;
using ETSystem.Model.Notice;
using ETSystem.Repository;
using System.Data.SqlClient;

namespace ETS.web.DAL
{
    public class MessageRepository : IMessageRepository
    {
        public List<UserDetails> GetAll(SqlConnection connection)
        {
            List<UserDetails> list = new List<UserDetails>();
            Response response = new Response();
            //if (connection == null)
            //{
            //    response.StatusCode = 500;
            //    response.StatusMessage = "SQL Connection is null";
            //    return response;
            //}
            try
            {
                // Notice notice= new Notice();

                //Open the connection to the database.
                connection.Open();
                //string queryNotice = "Select * from Notice";
                //string queryNotice = "SELECT n.NoticeId, n.Title, n.Content, n.PostedAt, u.FullName, u.Type FROM Notice n INNER JOIN SchoolUser u ON n.UserId = u.UserId";
                string queryMsg = "SELECT FullName, Type, UserId, EmailId FROM SchoolUser";



                using (SqlCommand cmdUser = new SqlCommand(queryMsg, connection))
                {
                    //Add values to the parameters in the query.
                    //cmdUser.Parameters.AddWithValue("@NoticeId", notice.NoticeId);




                    SqlDataReader reader = cmdUser.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        response.StatusCode = 401;
                        response.StatusMessage = "User Details Failed";
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            UserDetails userDetails = new UserDetails();
                            userDetails.UserId = (int)reader["UserId"];
                            userDetails.FullName = (string)reader["FullName"];
                            userDetails.Type = (string)reader["Type"];
                            userDetails.EmailId = (string)reader["EmailId"];
                            //notice.Content = (string)reader["Content"];
                            //notice.PostedAt = (DateTime)reader["PostedAt"];
                            //notice.FullName = (string)reader["FullName"];
                            //notice.Type = (string)reader["Type"];
                            //notice.UserId = (int)reader["UserId"];
                            list.Add(userDetails);
                        }
                        //Close the reader if a matching record is found.
                        reader.Close();

                    }


                }

            }

            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Using List View: " + ex.Message;
            }
            finally
            {

                connection.Close();
            }

            return list;
        }

        public Response Create(SendMsg sendMsg, SqlConnection connection)
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
                string queryMsg = "INSERT INTO MESSAGE ( SenderId, ReceiverId, Text)" +
                    "VALUES ( @SenderId, @ReceiverId, @Text)";

                using (SqlCommand cmdUser = new SqlCommand(queryMsg, connection))
                {
                    //Add values to the parameters in the query.
                    //cmdUser.Parameters.AddWithValue("@NoticeId", notice.NoticeId);
                    cmdUser.Parameters.AddWithValue("@SenderId", sendMsg.SenderId);
                    cmdUser.Parameters.AddWithValue("@ReceiverId", sendMsg.ReceiverId);
                    cmdUser.Parameters.AddWithValue("@Text", sendMsg.Text);
                    //cmdUser.Parameters.AddWithValue("@UserId", seb.UserId);


                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Insert into Message table failed");
                    }
                    else
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Message Creation Successful";
                        return response;
                    }
                }
            }

            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Message Creation: " + ex.Message;
            }
            finally
            {
                response.StatusCode = 200;
                response.StatusMessage = "Message Creation Successful";
                connection.Close();
            }

            return response;
        }

        public List<ReadMsg> GetByUId(int senderId, int receiverId, SqlConnection connection)
        {
            Response response = new Response();
            List<ReadMsg> messages = new List<ReadMsg>();

            string query = $@"SELECT MessageId, SenderId, ReceiverId, Text, DateAndTime 
                          FROM MESSAGE 
                          WHERE SenderId = '{senderId}' AND ReceiverId = '{receiverId}' 
                          OR SenderId = '{receiverId}' AND ReceiverId = '{senderId}'";

            try
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ReadMsg message = new ReadMsg
                        {
                            MessageId = (int)reader["MessageId"],
                            SenderId = (int)reader["SenderId"],
                            ReceiverId = (int)reader["ReceiverId"],
                            Text = (string)reader["Text"],
                            DateAndTime = (DateTime)reader["DateAndTime"]
                        };

                        messages.Add(message);
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occured during Message View:"+ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return messages;
        }
    }



}

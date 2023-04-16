
using ETSystem.Model;
using ETSystem.Model.Notice;
using ETSystem.Repository;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ETS.web.DAL
{
    public class NoticeRepository : INoticeRepository
    {
        public Response Create(CreateNotice cnotice, SqlConnection connection)
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
                string queryNotice = "INSERT INTO Notice ( Title, Content, PostedAt, UserId)" +
                    "VALUES ( @Title, @Content, @PostedAt, @UserId)";

                using (SqlCommand cmdUser = new SqlCommand(queryNotice, connection))
                {
                    //Add values to the parameters in the query.
                    cmdUser.Parameters.AddWithValue("@Title", cnotice.Title);
                    cmdUser.Parameters.AddWithValue("@Content", cnotice.Content);
                    cmdUser.Parameters.AddWithValue("@PostedAt", cnotice.PostedAt);
                    cmdUser.Parameters.AddWithValue("@UserId", cnotice.UserId);


                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Insert into Notice table failed");
                    }
                    else
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Creation S4ccessful";
                        return response;
                    }
                }
            }

            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Notice Creation: " + ex.Message;
            }
            finally
            {
                response.StatusCode = 200;
                response.StatusMessage = "Creation Successful";
                connection.Close();
            }

            return response;
        }

        public List<Notice> GetAll(SqlConnection connection)
        {
            List<Notice> list = new List<Notice>();
            Response response = new Response();
            try
            {

                //Open the connection to the database.
                connection.Open();
                string queryNotice = "SELECT n.NoticeId, n.Title, n.Content, n.PostedAt, u.FullName, u.Type, u.UserId FROM Notice n INNER JOIN SchoolUser u ON n.UserId = u.UserId ORDER BY n.PostedAt DESC";



                using (SqlCommand cmdUser = new SqlCommand(queryNotice, connection))
                {
                    //Add values to the parameters in the query.
                    //cmdUser.Parameters.AddWithValue("@NoticeId", notice.NoticeId);




                    SqlDataReader reader = cmdUser.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        response.StatusCode = 401;
                        response.StatusMessage = "Notice Failed";
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            Notice notice = new Notice();
                            notice.NoticeId = (int)reader["NoticeId"];
                            notice.Title = (string)reader["Title"];
                            notice.Content = (string)reader["Content"];
                            notice.PostedAt = (DateTime)reader["PostedAt"];
                            notice.FullName = (string)reader["FullName"];
                            notice.Type = (string)reader["Type"];
                            notice.UserId = (int)reader["UserId"];
                            list.Add(notice);
                        }
                        //Close the reader if a matching record is found.
                        reader.Close();

                    }


                }

            }

            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Notice View: " + ex.Message;
            }
            finally
            {

                connection.Close();
            }

            return list;
        }
        public Notice GetById(int NoticeId, SqlConnection connection)
        {
            Notice notice = new Notice();
            Response response = new Response();
            try
            {
                connection.Open();
                string queryNotice = $@"SELECT n.NoticeId, n.Title, n.Content, n.PostedAt, u.FullName, u.Type, u.UserId FROM Notice n INNER JOIN SchoolUser u ON n.UserId = u.UserId WHERE NoticeId = {NoticeId}";

                using (SqlCommand cmdUser = new SqlCommand(queryNotice, connection))
                {
                    //Add values to the parameters in the query.
                    SqlDataReader reader = cmdUser.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        reader.Close();
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            //Notice notice = new NoticeNoticeId
                            notice.NoticeId = (int)reader["NoticeId"];
                            notice.Title = (string)reader["Title"];
                            notice.Content = (string)reader["Content"];
                            notice.PostedAt = (DateTime)reader["PostedAt"];
                            notice.Type = (string)reader["Type"];
                            notice.FullName = (string)reader["FullName"];
                            notice.UserId = (int)reader["UserId"];

                        }
                        //Close the reader if a matching record is found.
                        reader.Close();
                    }
                }


            }

            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occured during Notice View:"+ex.Message;
            }
            finally
            {

                connection.Close();
            }

            return notice;
        }

        public Response Update(CreateNotice cnotice, SqlConnection connection)
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
                string queryNotice = "UPDATE Notice SET Title = @Title, Content = @Content, PostedAt = @PostedAt, UserId = @UserId  WHERE NoticeId = @NoticeId";

                using (SqlCommand cmdUser = new SqlCommand(queryNotice, connection))
                {
                    //Add values to the parameters in the query.
                    cmdUser.Parameters.AddWithValue("@NoticeId", cnotice.NoticeId);
                    cmdUser.Parameters.AddWithValue("@Title", cnotice.Title);
                    cmdUser.Parameters.AddWithValue("@Content", cnotice.Content);
                    cmdUser.Parameters.AddWithValue("@PostedAt", cnotice.PostedAt);
                    cmdUser.Parameters.AddWithValue("@UserId", cnotice.UserId);


                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Update into Notice table failed");
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
                response.StatusMessage = "An error occurred during Notice Update: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return response;
        }

        public Response Delete(int NoticeId, SqlConnection connection)
        {
            Notice notice = new Notice();
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
                string queryNotice = $"Delete FROM Notice WHERE NoticeId = {NoticeId}";

                using (SqlCommand cmdUser = new SqlCommand(queryNotice, connection))
                {
                    //Add values to the parameters in the query.
                    //cmdUser.Parameters.AddWithValue("@NoticeId", notice.NoticeId);



                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Delete Notice table failed");
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
                response.StatusMessage = "An error occurred during Notice Delete: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return response;
        }


    }


}



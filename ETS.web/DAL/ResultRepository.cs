using ETSystem.Model.Notice;
using ETSystem.Model;
using System.Data.SqlClient;
using ETS.web.Interface;
using ETS.web.Model.Result;
using System.Collections.Generic;
using ETS.web.Model.Dashboard;

namespace ETS.web.DAL
{
    public class ResultRepository : IResultRepository
    {
        public List<AnswerResultView> Get(int UserId, int IExamId, SqlConnection connection)
        {
            List<AnswerResultView> list = new List<AnswerResultView>();
            Response response = new Response();
            try
            {

                //Open the connection to the database.
                connection.Open();
                string queryResult = $@"select E.Question,A.Answer,E.CorrectAnswer,e.IMark from Answer A inner join EQuestions E on A.EQuestionId=E.EQuestionId where A.UserId= {UserId} and E.IExamId= {IExamId} ORDER BY E.EQuestionId ASC";



                using (SqlCommand cmdUser = new SqlCommand(queryResult, connection))
                {
                    //Add values to the parameters in the query.
                    //cmdUser.Parameters.AddWithValue("@NoticeId", notice.NoticeId);




                    SqlDataReader reader = cmdUser.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        response.StatusCode = 401;
                        response.StatusMessage = "Reult Failed";
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            AnswerResultView AresultView = new AnswerResultView();
                            AresultView.Question = (string)reader["Question"];
                            AresultView.Answer = (string)reader["Answer"];
                            AresultView.CorrectAnswer = (string)reader["CorrectAnswer"];
                            AresultView.IMark = (int)reader["IMark"];
                            //resultView.FullName = (string)reader["FullName"];
                            //resultView.Type = (string)reader["Type"];
                            //resultView.UserId = (int)reader["UserId"];
                            list.Add(AresultView);
                        }
                        //Close the reader if a matching record is found.
                        reader.Close();

                    }


                }

            }

            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Result View: " + ex.Message;
            }
            finally
            {

                connection.Close();
            }

            return list;
        }


        public Mark GetFullMarkpassmark(int IExamId, SqlConnection connection)
        {
            Mark mark = new Mark();
            Response response = new Response();
            try
            {
                connection.Open();
                string queryMark = $@"SELECT FullMark, PassMark FROM IExam WHERE IExamId = {IExamId}";

                using (SqlCommand cmdUser = new SqlCommand(queryMark, connection))
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
                            mark.FullMark = (int)reader["FullMark"];
                            mark.PassMark = (int)reader["PassMark"];

                        }
                        //Close the reader if a matching record is found.
                        reader.Close();
                    }
                }


            }

            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occured during Marks View:" + ex.Message;
            }
            finally
            {

                connection.Close();
            }

            return mark;
        }

        public Markobtained GetObtainedMark(int UserId, int IExamId, SqlConnection connection)
        {
            Markobtained markobtained = new Markobtained();
            Response response = new Response();
            try
            {
                connection.Open();
                string queryoMark = $@"SELECT SUM(E.IMark) AS MarkObtained, I.FullMark, I.PassMark, I.Class, S.SubjectName, I.ExamDescription, I.ExamDate,
                    CASE 
                        WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 90.0 THEN 'A+'
                        WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 80.0 THEN 'A'
                        WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 70.0 THEN 'B+'
                        WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 60.0 THEN 'B'
                        WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 50.0 THEN 'C'
                        WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 40.0 THEN 'C+'
                        ELSE 'No Grade'
                    END AS Grade
                FROM Answer A
                INNER JOIN EQuestions E ON A.EQuestionId = E.EQuestionId AND A.Answer = E.CorrectAnswer
                INNER JOIN IExam I ON E.IExamId = I.IExamId
                INNER JOIN Subjects S ON I.SubjectId = S.SubjectId
                WHERE A.UserId = {UserId} AND E.IExamId = {IExamId}
                GROUP BY I.Class, S.SubjectName, I.ExamDescription, I.ExamDate, I.FullMark, I.PassMark";

                using (SqlCommand cmdUser = new SqlCommand(queryoMark, connection))
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
                            //markobtained.IMark = (int)reader["MarkObtained"];
                            markobtained.ExamDescription = (string)reader["ExamDescription"];
                            markobtained.Class = (int)reader["Class"];
                            markobtained.SubjectName = (string)reader["SubjectName"];
                            markobtained.FullMark = (int)reader["FullMark"];
                            markobtained.PassMark = (int)reader["PassMark"];
                            markobtained.MarkObtained = (int)reader["MarkObtained"];
                            markobtained.ExamDate = (DateTime)reader["ExamDate"];
                            markobtained.Grade = (string)reader["Grade"];

                        }
                        //Close the reader if a matching record is found.
                        reader.Close();
                    }
                }


            }

            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occured during Marks Obtained:" + ex.Message;
            }
            finally
            {

                connection.Close();
            }

            return markobtained;
        }

        //To view the list of result of an exam the specific user has attended
        public List<ResultList> GetResultList(int UserId, SqlConnection connection)
        {
            List<ResultList> list = new List<ResultList>();
            Response response = new Response();
            try
            {
                connection.Open();
                string queryoMark = $@"SELECT SUM(E.IMark) AS MarkObtained, I.FullMark, I.PassMark, I.Class, S.SubjectName, I.ExamDescription, I.ExamDate,I.IExamId,A.UserId,
                    CASE 
                        WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 90.0 THEN 'A+'
                        WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 80.0 THEN 'A'
                        WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 70.0 THEN 'B+'
                        WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 60.0 THEN 'B'
                        WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 50.0 THEN 'C'
                        WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 40.0 THEN 'C+'
                        ELSE 'No Grade'
                    END AS Grade
                FROM Answer A
                INNER JOIN EQuestions E ON A.EQuestionId = E.EQuestionId AND A.Answer = E.CorrectAnswer
                INNER JOIN IExam I ON E.IExamId = I.IExamId
                INNER JOIN Subjects S ON I.SubjectId = S.SubjectId
                WHERE A.UserId = {UserId}
                GROUP BY I.Class, S.SubjectName, I.ExamDescription, I.ExamDate, I.FullMark, I.PassMark,I.IExamId,A.UserId";

                using (SqlCommand cmdUser = new SqlCommand(queryoMark, connection))
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
                            ResultList resultList = new ResultList();
                            //markobtained.IMark = (int)reader["MarkObtained"];
                            resultList.ExamDescription = (string)reader["ExamDescription"];
                            resultList.Class = (int)reader["Class"];
                            resultList.SubjectName = (string)reader["SubjectName"];
                            resultList.FullMark = (int)reader["FullMark"];
                            resultList.PassMark = (int)reader["PassMark"];
                            resultList.MarkObtained = (int)reader["MarkObtained"];
                            resultList.ExamDate = (DateTime)reader["ExamDate"];
                            resultList.Grade = (string)reader["Grade"];
                            resultList.UserId = (int)reader["UserId"];
                            resultList.IExamId = (int)reader["IExamId"];
                            list.Add(resultList);

                        }
                        //Close the reader if a matching record is found.
                        reader.Close();
                    }
                }


            }

            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occured during Marks Obtained:" + ex.Message;
            }
            finally
            {

                connection.Close();
            }

            return list;
        }

        //For Progress Graph
        public List<GraphStudent> GetResultGraph(int UserId, SqlConnection connection)
        {
            List<GraphStudent> list = new List<GraphStudent>();
            Response response = new Response();
            try
            {
                connection.Open();
                string queryoMark = $@"SELECT 
                (SUM(E.IMark) * 100 / I.FullMark) AS MarkObtained,
                S.SubjectName
                            FROM Answer A
                            INNER JOIN EQuestions E ON A.EQuestionId = E.EQuestionId AND A.Answer = E.CorrectAnswer
                            INNER JOIN IExam I ON E.IExamId = I.IExamId
                            INNER JOIN Subjects S ON I.SubjectId = S.SubjectId
                            WHERE A.UserId = {UserId}
                            GROUP BY I.Class, S.SubjectName, I.ExamDescription, I.ExamDate, I.FullMark, I.PassMark,I.IExamId,A.UserId";

                using (SqlCommand cmdUser = new SqlCommand(queryoMark, connection))
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
                            GraphStudent resultList = new GraphStudent();
                            //markobtained.IMark = (int)reader["MarkObtained"];
                            resultList.SubjectName = (string)reader["SubjectName"];
                            resultList.MarkObtained = (int)reader["MarkObtained"];
                            list.Add(resultList);

                        }
                        //Close the reader if a matching record is found.
                        reader.Close();
                    }
                }


            }

            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occured during Marks Obtained:" + ex.Message;
            }
            finally
            {

                connection.Close();
            }

            return list;
        }

        public List<ResultListTeacher> GetResultListTeacher(int IExamId, SqlConnection connection)
        {
            List<ResultListTeacher> list = new List<ResultListTeacher>();
            Response response = new Response();
            try
            {
                connection.Open();
                string queryoMark = $@"SELECT SUM(E.IMark) AS MarkObtained, I.FullMark, I.PassMark, I.Class, S.SubjectName, I.ExamDescription, I.ExamDate,I.IExamId,SU.FullName,
                    CASE 
                        WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 90.0 THEN 'A+'
                        WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 80.0 THEN 'A'
                        WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 70.0 THEN 'B+'
                        WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 60.0 THEN 'B'
                        WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 50.0 THEN 'C'
                        WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 40.0 THEN 'C+'
                        ELSE 'No Grade'
                    END AS Grade
                FROM Answer A
                INNER JOIN EQuestions E ON A.EQuestionId = E.EQuestionId AND A.Answer = E.CorrectAnswer
                INNER JOIN IExam I ON E.IExamId = I.IExamId
                INNER JOIN Subjects S ON I.SubjectId = S.SubjectId
				inner join SchoolUser SU on SU.UserId=A.UserId
                WHERE  E.IExamId={IExamId}
                GROUP BY I.Class, S.SubjectName, I.ExamDescription, I.ExamDate, I.FullMark, I.PassMark,I.IExamId,SU.FullName";

                using (SqlCommand cmdUser = new SqlCommand(queryoMark, connection))
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
                            ResultListTeacher resultListTeacher = new ResultListTeacher();
                            //markobtained.IMark = (int)reader["MarkObtained"];
                            resultListTeacher.FullName = (string)reader["FullName"];
                            resultListTeacher.ExamDescription = (string)reader["ExamDescription"];
                            resultListTeacher.Class = (int)reader["Class"];
                            resultListTeacher.SubjectName = (string)reader["SubjectName"];
                            resultListTeacher.FullMark = (int)reader["FullMark"];
                            resultListTeacher.PassMark = (int)reader["PassMark"];
                            resultListTeacher.MarkObtained = (int)reader["MarkObtained"];
                            resultListTeacher.ExamDate = (DateTime)reader["ExamDate"];
                            resultListTeacher.Grade = (string)reader["Grade"];
                            resultListTeacher.IExamId = (int)reader["IExamId"];
                            list.Add(resultListTeacher);

                        }
                        //Close the reader if a matching record is found.
                        reader.Close();
                    }
                }


            }

            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occured during Marks Obtained:" + ex.Message;
            }
            finally
            {

                connection.Close();
            }

            return list;
        }



        //public List<Markobtained> GetObtainedMarkList(int UserId, SqlConnection connection)
        //{
        //    List<Markobtained> list = new List<Markobtained>();
        //    Response response = new Response();
        //    try
        //    {
        //        connection.Open();
        //        string queryoMark = $@"SELECT SUM(E.IMark) AS MarkObtained, I.FullMark, I.PassMark, I.Class, S.SubjectName, I.ExamDescription, I.ExamDate,
        //            CASE 
        //                WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 90.0 THEN 'A+'
        //                WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 80.0 THEN 'A'
        //                WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 70.0 THEN 'B+'
        //                WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 60.0 THEN 'B'
        //                WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 50.0 THEN 'C'
        //                WHEN (SUM(E.IMark) * 100.0 / I.FullMark) >= 40.0 THEN 'C+'
        //                ELSE 'No Grade'
        //            END AS Grade
        //        FROM Answer A
        //        INNER JOIN EQuestions E ON A.EQuestionId = E.EQuestionId AND A.Answer = E.CorrectAnswer
        //        INNER JOIN IExam I ON E.IExamId = I.IExamId
        //        INNER JOIN Subjects S ON I.SubjectId = S.SubjectId
        //        WHERE A.UserId = {UserId} 
        //        GROUP BY I.Class, S.SubjectName, I.ExamDescription, I.ExamDate, I.FullMark, I.PassMark";

        //        using (SqlCommand cmdUser = new SqlCommand(queryoMark, connection))
        //        {
        //            //Add values to the parameters in the query.
        //            SqlDataReader reader = cmdUser.ExecuteReader();
        //            if (!reader.HasRows)
        //            {
        //                reader.Close();
        //            }
        //            else
        //            {
        //                while (reader.Read())
        //                {
        //                    Markobtained markobtained = new Markobtained();
        //                    //markobtained.IMark = (int)reader["MarkObtained"];
        //                    markobtained.ExamDescription = (string)reader["ExamDescription"];
        //                    markobtained.Class = (int)reader["Class"];
        //                    markobtained.SubjectName = (string)reader["SubjectName"];
        //                    markobtained.FullMark = (int)reader["FullMark"];
        //                    markobtained.PassMark = (int)reader["PassMark"];
        //                    markobtained.MarkObtained = (int)reader["MarkObtained"];
        //                    markobtained.ExamDate = (DateTime)reader["ExamDate"];
        //                    markobtained.Grade = (string)reader["Grade"];
        //                    list.Add(markobtained);

        //                }
        //                //Close the reader if a matching record is found.
        //                reader.Close();
        //            }
        //        }


        //    }

        //    catch (Exception ex)
        //    {
        //        response.StatusCode = 500;
        //        response.StatusMessage = "An error occured during Marks Obtained:" + ex.Message;
        //    }
        //    finally
        //    {

        //        connection.Close();
        //    }

        //    return list;
        //}




    }
        
}

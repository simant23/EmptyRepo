using ETSystem.Model;
using ETSystem.Model.LOGINREG;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ETS.web.DAL
{
    public class LOGINREG_DAL
    {
        public Response SRegistration(SREG_Model student, SqlConnection connection)
        {
            Response response = new Response();

            //Check if the SQL connection object is null.
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

                //SQL query to insert a new record into the SchoolUser table.
                string queryUser = "INSERT INTO SchoolUser(FullName, EmailId, Phone, Gender, Address, Type, Password, IsActive, IsApproved) " +
                                    "VALUES (@FullName, @EmailId, @Phone, @Gender, @Address, 'Student', @Password, 1, 0)";

                //Execute the query using a SqlCommand object.
                using (SqlCommand cmdUser = new SqlCommand(queryUser, connection))
                {
                    //Add values to the parameters in the query.
                    cmdUser.Parameters.AddWithValue("@FullName", student.FullName);
                    cmdUser.Parameters.AddWithValue("@EmailId", student.EmailId);
                    cmdUser.Parameters.AddWithValue("@Phone", student.Phone);
                    cmdUser.Parameters.AddWithValue("@Gender", student.Gender);
                    cmdUser.Parameters.AddWithValue("@Address", student.Address);
                    cmdUser.Parameters.AddWithValue("@Password", student.Password);

                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("Insert into SchoolUser table failed");
                    }
                }

                //SQL query to insert a new record into the Student table.
                string queryStudent = "INSERT INTO Student(UserId, StudentId, Class, GuardianName, DOB, EnrollmentDate) " +
                                       "VALUES ((SELECT UserId FROM SchoolUser WHERE EmailId = @EmailId), @StudentId, @Class, @GuardianName, @DOB, @EnrollmentDate)";

                //Execute the query using a SqlCommand object.
                using (SqlCommand cmdStudent = new SqlCommand(queryStudent, connection))
                {
                    cmdStudent.Parameters.AddWithValue("@EmailId", student.EmailId);
                    cmdStudent.Parameters.AddWithValue("@StudentId", student.StudentId);
                    cmdStudent.Parameters.AddWithValue("@GuardianName", student.GuardianName);
                    cmdStudent.Parameters.AddWithValue("@Class", student.Class);
                    cmdStudent.Parameters.AddWithValue("@DOB", student.DOB);
                    cmdStudent.Parameters.AddWithValue("@EnrollmentDate", student.EnrollmentDate);

                    int k = cmdStudent.ExecuteNonQuery();
                    if (k <= 0)
                    {
                        throw new Exception("Insert into Student table failed");

                    }
                    else
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Registration Successful";
                    }
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during registration: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return response;
        }

        public Response TRegistration(TREG_Model teacher, SqlConnection connection)
        {
            Response response = new Response();

            //Check if the SQL connection object is null.
            if (connection == null)
            {
                response.StatusCode = 500;
                response.StatusMessage = "SQL Connection is null";
                return response;
            }

            try
            {
                //Open the connection to the database.
                using (connection)
                {
                    connection.Open();

                    //SQL query to insert a new record into the SchoolUser table.
                    string queryUser = "INSERT INTO SchoolUser(FullName, EmailId, Phone, Gender, Address, Type, Password, IsActive, IsApproved) " +
                                        "VALUES (@FullName, @EmailId, @Phone, @Gender, @Address, 'Teacher', @Password, 1, 0)";

                    //Execute the query using a SqlCommand object.
                    using (SqlCommand cmdUser = new SqlCommand(queryUser, connection))
                    {
                        //Add values to the parameters in the query.
                        cmdUser.Parameters.AddWithValue("@FullName", teacher.FullName);
                        cmdUser.Parameters.AddWithValue("@EmailId", teacher.EmailId);
                        cmdUser.Parameters.AddWithValue("@Phone", teacher.Phone);
                        cmdUser.Parameters.AddWithValue("@Gender", teacher.Gender);
                        cmdUser.Parameters.AddWithValue("@Address", teacher.Address);
                        cmdUser.Parameters.AddWithValue("@Password", teacher.Password);

                        int j = cmdUser.ExecuteNonQuery();
                        if (j <= 0)
                        {
                            //Throw an exception if the insertion failed.
                            throw new Exception("Insert into SchoolUser table failed");
                        }
                        else
                        {
                            //SQL query to insert a new record into the Teacher table.
                            string queryTeacher = "INSERT INTO Teacher(UserId, TeacherId, Qualification, Major, HireDate,SubTeacher, TExperience) " +
                                                   "VALUES ((SELECT UserId FROM SchoolUser WHERE EmailId = @EmailId), @TeacherId, @Qualification, @Major, @HireDate, @SubTeacher, @TExperience)";

                            //Execute the query using a SqlCommand object.
                            using (SqlCommand cmdTeacher = new SqlCommand(queryTeacher, connection))
                            {
                                cmdTeacher.Parameters.AddWithValue("@EmailId", teacher.EmailId);
                                cmdTeacher.Parameters.AddWithValue("@TeacherId", teacher.TeacherId);
                                cmdTeacher.Parameters.AddWithValue("@Qualification", teacher.Qualification);
                                cmdTeacher.Parameters.AddWithValue("@Major", teacher.Major);
                                cmdTeacher.Parameters.AddWithValue("@HireDate", teacher.HireDate);
                                cmdTeacher.Parameters.AddWithValue("@SubTeacher", teacher.SubTeacher);
                                cmdTeacher.Parameters.AddWithValue("@TExperience", teacher.TExperience);

                                int k = cmdTeacher.ExecuteNonQuery();
                                if (k <= 0)
                                {
                                    throw new Exception("Insert into Teacher table failed");
                                }
                                else
                                {
                                    response.StatusCode = 200;
                                    response.StatusMessage = "Registration Successful";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during registration: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return response;

        }

        public Response Login(LOGIN_Model login, SqlConnection connection)
        {
            Response response = new Response();

            //Check if the SQL connection object is null.
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

                //SQL query to select a record from the SchoolUser table based on the email, password, type, and active status.
                string queryUser = "SELECT * FROM SchoolUser WHERE EmailId = @EmailId AND Password = @Password AND Type = @Type AND IsActive = 1 AND IsApproved = 1";


                //Execute the query using a SqlCommand object.
                using (SqlCommand cmdUser = new SqlCommand(queryUser, connection))
                {
                    //Add values to the parameters in the query.
                    cmdUser.Parameters.AddWithValue("@EmailId", login.EmailId);
                    cmdUser.Parameters.AddWithValue("@Type", login.Type);
                    cmdUser.Parameters.AddWithValue("@Password", login.Password);

                    //Execute the query and store the result in a SqlDataReader object.
                    SqlDataReader reader = cmdUser.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        //Close the reader if no matching record is found.
                        reader.Close();
                        response.StatusCode = 401;
                        response.StatusMessage = "Login Failed";
                    }
                    else
                    {
                        //Close the reader if a matching record is found.
                        reader.Close();
                        response.StatusCode = 200;
                        response.StatusMessage = "Login Successful";
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Login: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return response;
        }

        public Response UserApprove(UserApproval userApproval, SqlConnection connection)
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
                string queryApproval = "UPDATE SchoolUser SET IsApproved = @IsApproved WHERE  UserId = @UserId";

                using (SqlCommand cmdUser = new SqlCommand(queryApproval, connection))
                {
                    //Add values to the parameters in the query.
                    cmdUser.Parameters.AddWithValue("@UserId", userApproval.UserId);
                    cmdUser.Parameters.AddWithValue("@IsApproved", userApproval.IsApproved);


                    int j = cmdUser.ExecuteNonQuery();
                    if (j <= 0)
                    {
                        //Throw an exception if the insertion failed.
                        throw new Exception("User Approval failed");
                    }
                    else
                    {

                        response.StatusCode = 200;
                        response.StatusMessage = "User Approval Successful";
                        return response;
                    }
                }
            }

            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during User Approval: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return response;
        }

        public Response ChangePassword(ChangePwdl cPassword, SqlConnection connection)
        {
            Response response = new Response();

            //Check if the SQL connection object is null.
            if (connection == null)
            {
                response.StatusCode = 500;
                response.StatusMessage = "SQL Connection is null";
                return response;
            }

            try
            {
                //Open the connection to the database.
                using (connection)
                {
                    connection.Open();

                    //SQL query to insert a new record into the SchoolUser table.
                    string queryUser = "UPDATE SchoolUser SET Password = @NewPassword WHERE  EmailId = @EmailId AND Password = @OldPassword  AND IsActive = 1 AND IsApproved = 1";

                    //Execute the query using a SqlCommand object.
                    using (SqlCommand cmdUser = new SqlCommand(queryUser, connection))
                    {
                        //Add values to the parameters in the query.
                        cmdUser.Parameters.AddWithValue("@EmailId", cPassword.EmailId);
                        cmdUser.Parameters.AddWithValue("@OldPassword", cPassword.OldPassword);
                        cmdUser.Parameters.AddWithValue("@NewPassword", cPassword.NewPassword);

                        int j = cmdUser.ExecuteNonQuery();
                        if (j <= 0)
                        {
                            //Throw an exception if the insertion failed.
                            throw new Exception("Password Change Failed");
                        }

                        else
                        {

                            response.StatusCode = 200;
                            response.StatusMessage = "Password Successfully Changed";

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Passsword Change: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return response;

        }

        public Response ForgetPassword(ForgetPwd forgetPwd, SqlConnection connection)
        {
            Response response = new Response();

            //Check if the SQL connection object is null.
            if (connection == null)
            {
                response.StatusCode = 500;
                response.StatusMessage = "SQL Connection is null";
                return response;
            }

            try
            {
                //Open the connection to the database.
                using (connection)
                {
                    connection.Open();

                    //SQL query to insert a new record into the SchoolUser table.
                    string queryUser = "UPDATE SchoolUser SET Password = @NewPassword  WHERE  Phone = @Phone AND EmailId = @EmailId AND IsActive = 1 AND IsApproved = 1";

                    //Execute the query using a SqlCommand object.
                    using (SqlCommand cmdUser = new SqlCommand(queryUser, connection))
                    {
                        //Add values to the parameters in the query.
                        //cmdUser.Parameters.AddWithValue("@EmailId", cPassword.EmailId);
                        cmdUser.Parameters.AddWithValue("@Phone", forgetPwd.Phone);
                        cmdUser.Parameters.AddWithValue("@NewPassword", forgetPwd.NewPassword);
                        cmdUser.Parameters.AddWithValue("@EmailId", forgetPwd.EmailId);

                        int j = cmdUser.ExecuteNonQuery();
                        if (j <= 0)
                        {
                            //Throw an exception if the insertion failed.
                            throw new Exception("Password Update Failed");
                        }

                        else
                        {

                            response.StatusCode = 200;
                            response.StatusMessage = "Password Successfully Updated";

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occurred during Passsword Updation: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return response;
        }

        public Response StudentDetails(StudentDetails studentDetails, string connectionString)
        {
            Response response = new Response();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT su.UserId, su.EmailId, su.FullName, su.Phone, su.Gender, su.Address, s.StudentId, s.GuardianName, s.Class, s.DOB, s.EnrollmentDate " +
                                   "FROM SchoolUser su " +
                                   "INNER JOIN Student s ON su.UserId = s.UserId " +
                                   "WHERE su.EmailId = @EmailId AND su.IsActive = 1 AND su.IsApproved = 1";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmailId", studentDetails.EmailId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                studentDetails.UserId = reader.GetInt32(0);
                                studentDetails.EmailId = reader.GetString(1);
                                studentDetails.FullName = reader.GetString(2);
                                studentDetails.Phone = reader.GetString(3);
                                studentDetails.Gender = reader.GetString(4);
                                studentDetails.Address = reader.GetString(5);
                                studentDetails.StudentId = reader.GetInt32(6);
                                studentDetails.GuardianName = reader.GetString(7);
                                studentDetails.Class = reader.GetInt32(8);
                                studentDetails.DOB = reader.GetDateTime(9).Date;
                                string dobString = studentDetails.DOB.ToString("yyyy-MM-dd");

                                studentDetails.EnrollmentDate = reader.GetDateTime(10).Date;
                                string enrollmentDateString = studentDetails.EnrollmentDate.ToString("yyyy-MM-dd");



                                response.StatusCode = 200;
                                response.StatusMessage = "Success";
                                response.Result = studentDetails;
                            }
                            else
                            {
                                response.StatusCode = 404;
                                response.StatusMessage = "Record not found";
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occured during Student's Details View:"+ex.Message;
            }

            return response;
        }

        public Response TeachertDetails(TeacherDetails teacherDetails, string connectionString)
        {
            Response response = new Response();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT su.UserId, su.EmailId, su.FullName, su.Phone, su.Gender, su.Address, t.TeacherId, t.Qualification, t.Major, t.SubTeacher, t.TExperience " +
                                   "FROM SchoolUser su " +
                                   "INNER JOIN Teacher t ON su.UserId = t.UserId " +
                                   "WHERE su.EmailId = @EmailId AND su.IsActive = 1 AND su.IsApproved = 1";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmailId", teacherDetails.EmailId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                teacherDetails.UserId = reader.GetInt32(0);
                                teacherDetails.EmailId = reader.GetString(1);
                                teacherDetails.FullName = reader.GetString(2);
                                teacherDetails.Phone = reader.GetString(3);
                                teacherDetails.Gender = reader.GetString(4);
                                teacherDetails.Address = reader.GetString(5);
                                teacherDetails.TeacherId = reader.GetInt32(6);
                                teacherDetails.Qualification = reader.GetString(7);
                                teacherDetails.Major = reader.GetString(8);
                                teacherDetails.SubTeacher = reader.GetString(9);
                                teacherDetails.TExperience = reader.GetInt32(10);

                                response.StatusCode = 200;
                                response.StatusMessage = "Success";
                                response.teacherDetails = teacherDetails;
                            }
                            else
                            {
                                response.StatusCode = 404;
                                response.StatusMessage = "Record not found";
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occured during Teacher's Details View:"+ex.Message;
            }

            return response;
        }

        public Response AdminDetails(AdminDetails adminDetails, string connectionString)
        {
            Response response = new Response();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT UserId,EmailId,FullName,Phone,Address FROM SchoolUser WHERE EmailId = 'tirupatischool123@gmail.com' AND IsActive = 1 AND IsApproved = 1";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        //command.Parameters.AddWithValue("@EmailId", adminDetails.EmailId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                adminDetails.UserId = reader.GetInt32(0);
                                adminDetails.EmailId = reader.GetString(1);
                                adminDetails.FullName = reader.GetString(2);
                                adminDetails.Phone = reader.GetString(3);
                                adminDetails.Address = reader.GetString(4);

                                response.StatusCode = 200;
                                response.StatusMessage = "Success";
                                response.adminDetails = adminDetails;
                            }
                            else
                            {
                                response.StatusCode = 404;
                                response.StatusMessage = "Record not found";
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = "An error occured during Admin Details View:"+ex.Message;
            }

            return response;
        }



    }
}



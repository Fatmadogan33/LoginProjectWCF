using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using WcfService.LoginDatabase;
using System.Runtime.ConstrainedExecution;
using WcfService.LoginBusiness;
using log4net;

namespace WcfService.LoginDatabase
{
    public class UsersRepository
    {
        ILog log = log4net.LogManager.GetLogger(typeof(UsersRepository));
        SqlConnection conn;
        DatabaseConnector databaseConnector = new DatabaseConnector();

        private void connectionClose()
        {
            if (conn != null)
            {
                conn.Close();
            }
        }

        public int InsertUser(Users user)
        {
            try
            {
                conn = databaseConnector.ConnectToDb();
                conn.Open();

                using (SqlCommand insertCommand = conn.CreateCommand())
                {

                    insertCommand.CommandType = CommandType.Text;

                    insertCommand.CommandText = "INSERT INTO Users(username,email,name,surname,password,status) VALUES(@username, @email, @name, @surname, @password, @status)";
                    insertCommand.Parameters.AddWithValue("username", user.username);
                    insertCommand.Parameters.AddWithValue("email", user.email);
                    insertCommand.Parameters.AddWithValue("name", user.name);
                    insertCommand.Parameters.AddWithValue("surname", user.surname);
                    insertCommand.Parameters.AddWithValue("password", user.password);
                    insertCommand.Parameters.AddWithValue("status", Constants.ACTIVE);

                    return insertCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("InsertUser - "+  Constants.DB_ERROR + "{0}", ex.Message));
                throw ex;
            }
            finally
            {
                connectionClose();
            }
        }

        public int UpdateUserPassword(Users user)
        {
            try
            {
                conn = databaseConnector.ConnectToDb();
                conn.Open();

                using (SqlCommand updateCommand = conn.CreateCommand())
                {

                    updateCommand.CommandType = CommandType.Text;
                    updateCommand.CommandText = "UPDATE Users SET password=@password WHERE username=@username and email=@email";
                    updateCommand.Parameters.AddWithValue("username", user.username);
                    updateCommand.Parameters.AddWithValue("email", user.email);
                    updateCommand.Parameters.AddWithValue("password", user.password);

                    return updateCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("UpdateUserPassword - "+  Constants.DB_ERROR + "{0}", ex.Message));
                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    connectionClose();
                }
            }

        }

        public int UpdateUserStatus(Users user)
        {
            try
            {
                conn = databaseConnector.ConnectToDb();
                conn.Open();

                using (SqlCommand updateCommand = conn.CreateCommand())
                {
                    updateCommand.CommandType = CommandType.Text;
                    updateCommand.CommandText = "UPDATE Users SET status=@status WHERE username=@username and email=@email and password=@password";
                    updateCommand.Parameters.AddWithValue("username", user.username);
                    updateCommand.Parameters.AddWithValue("email", user.email);
                    updateCommand.Parameters.AddWithValue("password", user.password);
                    updateCommand.Parameters.AddWithValue("status", user.status);

                    return updateCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("UpdateUserStatus - "+  Constants.DB_ERROR + "{0}", ex.Message));
                return -1;
            }
            finally
            {
                connectionClose();
            }
        }

        public int UpdateUserEmail(Users user)
        {
            try
            {

                conn = databaseConnector.ConnectToDb();
                conn.Open();

                using (SqlCommand updateCommand = conn.CreateCommand())
                {
                    updateCommand.CommandType = CommandType.Text;
                    updateCommand.CommandText = "UPDATE Users SET email=@email WHERE username=@username and status=@status";
                    updateCommand.Parameters.AddWithValue("username", user.username);
                    updateCommand.Parameters.AddWithValue("email", user.email);
                    updateCommand.Parameters.AddWithValue("status", Constants.ACTIVE);

                    return updateCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("UpdateUserEmail - "+  Constants.DB_ERROR + "{0}", ex.Message));
                throw;
            }
            finally
            {
                connectionClose();
            }

        }


        public Users GetUserByNameAndEmail(string username, string email)
        {
            try
            {
                conn = databaseConnector.ConnectToDb();
                conn.Open();

                var user = new Users();

                using (SqlCommand selectCommand = conn.CreateCommand())
                {

                    selectCommand.CommandText = "SELECT * FROM Users WHERE username=@username and email=@email and status=@status";
                    selectCommand.Parameters.AddWithValue("username", username);
                    selectCommand.Parameters.AddWithValue("email", email);
                    selectCommand.Parameters.AddWithValue("status", Constants.ACTIVE);


                    var reader = selectCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        user.username = reader.GetString(reader.GetOrdinal("username"));
                        user.email = reader.GetString(reader.GetOrdinal("email"));
                        user.name = reader.GetString(reader.GetOrdinal("name"));
                        user.surname = reader.GetString(reader.GetOrdinal("surname"));
                        user.password = reader.GetString(reader.GetOrdinal("password"));
                    }

                    return user;
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("GetUserByNameAndEmail - "+  Constants.DB_ERROR + "{0}", ex.Message));
                throw ex;
            }
            finally
            {
                connectionClose();
            }

        }

        public Boolean CheckUserWithUsername(string username)
        {
            try
            {
                conn = databaseConnector.ConnectToDb();
                conn.Open();
                var user = new Users();

                using (SqlCommand selectCommand = conn.CreateCommand())
                {
                    selectCommand.CommandText = "SELECT username FROM Users WHERE username=@username";
                    selectCommand.Parameters.AddWithValue("username", username);


                    var reader = selectCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        user.username = reader.GetString(reader.GetOrdinal("username"));
                    }
                    if (user.username != null)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("CheckUserWithUsername - "+  Constants.DB_ERROR + "{0}", ex.Message));
                throw ex;
            }
            finally
            {
                connectionClose();
            }
        }

        public Boolean CheckUsernamePassword(string username, string hashPassword)
        {
            try
            {
                conn = databaseConnector.ConnectToDb();
                conn.Open();

                var user = new Users();

                using (SqlCommand selectCommand = conn.CreateCommand())
                {
                    selectCommand.CommandText = "SELECT username FROM Users WHERE username=@username and password=@hashPassword and status=@status";
                    selectCommand.Parameters.AddWithValue("username", username);
                    selectCommand.Parameters.AddWithValue("hashPassword", hashPassword);
                    selectCommand.Parameters.AddWithValue("status", Constants.ACTIVE);


                    var reader = selectCommand.ExecuteReader();



                    while (reader.Read())
                    {
                        user.username = reader.GetString(reader.GetOrdinal("username"));
                    }
                    if (user.username != null)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("CheckUsernamePassword- "+  Constants.DB_ERROR + "{0}", ex.Message));
                throw ex;
            }
            finally
            {
                connectionClose();
            }
        }
    }
}
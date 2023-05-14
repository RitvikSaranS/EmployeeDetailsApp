using EmployeeDetailsAPI.Interfaces;
using EmployeeDetailsAPI.loggingFunctions;
using EmployeeDetailsAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetailsAPI.DbConnection
{
    public class RegAndLogin : IRegAndLogin
    {
        private readonly string _connectionString =
            @"Server=13.232.139.178;Database=learning_Internship;Integrated Security=true;";
        private readonly SqlConnection _connection;
        public RegAndLogin()
        {
            _connection = new SqlConnection(_connectionString);
        }
        public string GetHash(string username)
        {
            _connection.Open();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM loginandreg WHERE username='{username}'", _connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];
                return dataTable.Rows[0]["password_hash"].ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                _connection.Close();
            }
            return null;
        }

        public void SetHash(Register user)
        {
            _connection.Open();
            try
            {
                SqlCommand command =
                    new SqlCommand(
                        @"INSERT INTO loginandreg (username, password_hash, EMPID) 
                        VALUES(@username, @passwordHash, @empId);"
                        , _connection);
                command.Parameters.AddWithValue("@username", user.username);
                command.Parameters.AddWithValue("@passwordHash", user.passwordHash);
                command.Parameters.AddWithValue("@empId", user.empId);
                var rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} rows affected.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        public void updateHash(Register user)
        {
            _connection.Open();
            try
            {
                SqlCommand command = new SqlCommand(
                    @"UPDATE loginandreg SET password_hash = @passwordHash WHERE EMPID=@empid"
                    , _connection);
                command.Parameters.AddWithValue("@passwordHash", user.passwordHash);
                command.Parameters.AddWithValue("@empid", user.empId);
                var rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} rows affected.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using EmployeeDetailsAPI.Interfaces;
using EmployeeDetailsAPI.loggingFunctions;
using EmployeeDetailsAPI.Models;

namespace EmployeeDetailsAPI.DbConnection
{
    public class DepartmentDB : IDeptDB
    {
        private readonly string _connectionString = @"Server=13.232.139.178;Database=learning_Internship;Integrated Security=true;";
        private readonly SqlConnection _connection;
        private readonly List<Department> _users = new List<Department>();
        private readonly static DepartmentErrorLogger _depterrorlog = new DepartmentErrorLogger();
        public DepartmentDB()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM DEPT_TABLE", _connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];
                foreach (DataRow row in dataTable.Rows)
                {
                    _users.Add(new Department(){
                        DeptId = (int)row["DEPT_ID"],
                        DeptName = (string)row["DEPT_Name"]
                    });
                }
            }
            catch (Exception ex)
            {
                _depterrorlog.logError($"Database error : GET ALL : {ex}");
            }
            finally
            {
                _connection.Close();
            }
        }

        public void delData(int id)
        {
            _connection.Open();
            try
            {
                SqlCommand command = new SqlCommand($"DELETE FROM DEPT_TABLE WHERE DEPT_ID = @id", _connection);
                command.Parameters.AddWithValue("@id", id);
                var rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} rows affected.");
                for (int i = 0; i < +_users.Count; i++)
                {
                    if (_users[i].DeptId == id)
                    {
                        _users.Remove(_users[i]);
                    }
                }

            }
            catch (Exception ex)
            {
                _depterrorlog.logError($"Database error : DEL : {ex}");
            }
            finally
            {
                _connection.Close();
            }
        }

        public object getData()
        {
            return _users;
        }

        public Department getData(int id)
        {
            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i].DeptId == id)
                {
                    return _users[i];
                }
            }
            return null;
        }

        public void postData(Department department)
        {
            _connection.Open();
            try
            {
                SqlCommand command =
                    new SqlCommand(
                        @"INSERT INTO DEPT_TABLE (DEPT_ID, DEPT_Name) 
                        VALUES(@DeptId, @DeptName);"
                        , _connection);
                command.Parameters.AddWithValue("@DeptId", department.DeptId);
                command.Parameters.AddWithValue("@DeptName", department.DeptName);
                var rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} rows affected.");
                _users.Add(department);
            }
            catch (Exception ex)
            {
                _depterrorlog.logError($"Database error : POST : {ex}");
            }
            finally
            {
                _connection.Close();
            }
        }

        public void putData(int id, Department department)
        {
            _connection.Open();
            try
            {
                SqlCommand command = new SqlCommand(
                    @"UPDATE EMP_TABLE SET DEPT_ID = @DeptId, DEPT_Name = @DeptName WHERE DEPT_ID=@id"
                    , _connection);
                command.Parameters.AddWithValue("@DeptId", department.DeptId);
                command.Parameters.AddWithValue("@DeptName", department.DeptName);
                var rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} rows affected.");
                for (int i = 0; i < _users.Count; i++)
                {
                    if (_users[i].DeptId == id)
                    {
                        _users[i] = department;
                    }
                }
            }
            catch (Exception ex)
            {
                _depterrorlog.logError($"Database error : PUT : {ex}");
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}

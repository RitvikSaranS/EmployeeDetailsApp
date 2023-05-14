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
    public class EmployeeDB : IEmpDB
    {
        private readonly string _connectionString = 
            @"Server=13.232.139.178;Database=learning_Internship;Integrated Security=true;";
        private readonly SqlConnection _connection;
        private readonly List<Employee> _users = new List<Employee>();
        private readonly static EmployeeErrorLogger _emperrorlog = new EmployeeErrorLogger();
        public EmployeeDB()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM EMP_TABLE", _connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];
                foreach (DataRow row in dataTable.Rows)
                {
                    _users.Add(new Employee() {
                        EMPID = (string)row["EMPID"],
                        Name = (string)row["Name"],
                        Age = (int)row["Age"],
                        Gender = (string)row["Gender"],
                        Dob = (DateTime)row["DOB"],
                        Salary = (decimal)row["Salary"],
                        Address = (string)row["Address"],
                        DeptId = (int)row["DEPT_ID"],
                        CreatedBy = (string)row["CreatedBy"],
                        CreatedDate = (DateTime)row["CreatedDate"]
                    });
                }
            }
            catch (Exception ex)
            {
                _emperrorlog.logError($"Database error : GET ALL : {ex}");
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
                string empid = "EMP" + (id + "").PadLeft(4, '0');
                SqlCommand command = new SqlCommand($"DELETE FROM EMP_TABLE WHERE EMPID = @empid", _connection);
                command.Parameters.AddWithValue("@empid", empid);
                var rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} rows affected.");
                for(int i = 0; i < +_users.Count; i++)
                {
                    if(_users[i].EMPID == empid)
                    {
                        _users.Remove(_users[i]);
                    }
                }
                
            }
            catch (Exception ex)
            {
                _emperrorlog.logError($"Database error : DEL : {ex}");
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
        public Employee getData(int id)
        {
            for (int i = 0; i < _users.Count; i++)
            {
                if (int.Parse(_users[i].EMPID.Substring(3)) == id)
                {
                    return _users[i];
                }
            }
            return null;
        }

        public void postData(Employee employee)
        {
            _connection.Open();
            try
            {
                SqlCommand command = 
                    new SqlCommand(
                        @"INSERT INTO EMP_TABLE (Name, Age, Gender, DOB, Salary, Address, DEPT_ID, CreatedBy, CreatedDate) 
                        VALUES(@Name, @Age, @Gender, @Dob, @Salary, @Address, @DeptId, @CreatedBy, @CreatedDate); "
                        , _connection);
                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@Age", employee.Age);
                command.Parameters.AddWithValue("@Gender", employee.Gender);
                command.Parameters.AddWithValue("@Dob", employee.Dob);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.Parameters.AddWithValue("@Address", employee.Address);
                command.Parameters.AddWithValue("@DeptId", employee.DeptId);
                command.Parameters.AddWithValue("@CreatedBy", employee.CreatedBy);
                command.Parameters.AddWithValue("@CreatedDate", employee.CreatedDate);
                var rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} rows affected.");
                _users.Add(employee);
            }
            catch (Exception ex)
            {
                _emperrorlog.logError($"Database error : POST : {ex}");
            }
            finally
            {
                _connection.Close();
            }
        }

        public void putData(int id, Employee employee)
        {
            _connection.Open();
            try
            {
                string empid = "EMP" + (id + "").PadLeft(4, '0');
                SqlCommand command = new SqlCommand(
                    @"UPDATE EMP_TABLE SET Name = @Name, Age = @Age, Gender = @Gender, DOB = @Dob, Salary = @Salary, 
                    Address = @Address, DEPT_ID = @DeptId, CreatedBy = @CreatedBy, CreatedDate = @CreatedDate WHERE EMPID=@empid"
                    , _connection);
                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@Age", employee.Age);
                command.Parameters.AddWithValue("@Gender", employee.Gender);
                command.Parameters.AddWithValue("@Dob", employee.Dob);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.Parameters.AddWithValue("@Address", employee.Address);
                command.Parameters.AddWithValue("@DeptId", employee.DeptId);
                command.Parameters.AddWithValue("@CreatedBy", employee.CreatedBy);
                command.Parameters.AddWithValue("@CreatedDate", employee.CreatedDate);
                command.Parameters.AddWithValue("@empid", empid);
                var rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} rows affected.");
                for(int i = 0; i < _users.Count; i++)
                {
                    if(_users[i].EMPID == empid)
                    {
                        _users[i] = employee;
                    }
                }
            }
            catch (Exception ex)
            {
                _emperrorlog.logError($"Database error : PUT : {ex}");
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}

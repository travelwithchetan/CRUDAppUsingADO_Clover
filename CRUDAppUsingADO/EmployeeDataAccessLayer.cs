using CRUDAppUsingADO.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace CRUDAppUsingADO
{
    public class EmployeeDataAccessLayer
    {
        private readonly IMemoryCache _cache;

        string cs = ConnectionString.dbcs;

        
        public EmployeeDataAccessLayer(IMemoryCache cache)
        {
            _cache = cache; 
        }
        #region GetAllEmployee
        public List<Employees> GetAllEmployees()
        {
            const string cachekey = "EmployeeList";

            if (_cache.TryGetValue(cachekey, out List<Employees> employees))
            {
                return employees;//return from cache
            }

            employees = new List<Employees>();


            //List<Employees> emplist = new List<Employees>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetAllEployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Employees emp = new Employees();
                    emp.id = Convert.ToInt32(reader["id"]);
                    emp.name = reader["name"].ToString() ?? "";
                    emp.gender = reader["gender"].ToString() ?? "";
                    emp.age = Convert.ToInt32(reader["age"]);
                    emp.designation = reader["designation"].ToString() ?? "";
                    emp.city = reader["city"].ToString();
                    employees.Add(emp);

               }
                _cache.Set(cachekey,employees, TimeSpan.FromMinutes(5));
                return employees;
            }
        }
        #endregion
        #region AddEmployee
        public void AddEmployee(Employees emp)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spAddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@name", emp.name);
                cmd.Parameters.AddWithValue("@gender", emp.gender);
                cmd.Parameters.AddWithValue("@age", emp.age);
                cmd.Parameters.AddWithValue("@designation", emp.designation);
                cmd.Parameters.AddWithValue("@city", emp.city);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        #endregion
        #region Wrong UpdateEmployee


        #endregion
        #region View for Edit and Method for Edit Update
        public Employees getEmployeeById(int id)
        {
            string cachekey = $"Employees_{id}";

            if (_cache.TryGetValue(cachekey, out Employees emp))
            {
                return emp; 
            }
            //Employees emp = new Employees();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select * from employee where Id=@id", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {

                        emp.id = Convert.ToInt32(reader["id"]);
                        emp.name = reader["name"].ToString() ?? "";
                        emp.gender = reader["gender"].ToString() ?? "";
                        emp.age = Convert.ToInt32(reader["age"]);
                        emp.designation = reader["designation"].ToString() ?? "";
                        emp.city = reader["city"].ToString() ?? "";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                _cache.Set(cachekey,emp,TimeSpan.FromMinutes(5));
                return emp;
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void UpdateEmployee(Employees emp)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", emp.id);
                cmd.Parameters.AddWithValue("@name", emp.name);
                cmd.Parameters.AddWithValue("@gender", emp.gender);
                cmd.Parameters.AddWithValue("@age", emp.age);
                cmd.Parameters.AddWithValue("@designation", emp.designation);
                cmd.Parameters.AddWithValue("@city", emp.city);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        #endregion
        #region Employee Details
        public Employees GetEmployeeDetails(int id)
        {
            Employees emp = new Employees();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select * from employee", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {

                        emp.id = Convert.ToInt32(reader["id"]);
                        emp.name = reader["name"].ToString() ?? "";
                        emp.gender = reader["gender"].ToString() ?? "";
                        emp.age = Convert.ToInt32(reader["age"]);
                        emp.designation = reader["designation"].ToString() ?? "";
                        emp.city = reader["city"].ToString() ?? "";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return emp;
            }
        }
        #endregion

        #region Delete Employee
        public void DeleteEmployee(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        #endregion
        #region Login_Logout
        public bool ValidateUser(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spValidateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                return reader.HasRows;   // TRUE = valid user
            }
        }
        #endregion

        #region SignUp Functionality
        public bool UserExists(string username)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Users WHERE Username=@username", con);
                cmd.Parameters.AddWithValue("@username", username);

                con.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public void RegisterUser(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Users (Username, Password) VALUES (@u, @p)", con);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", password);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        #endregion 


    }
}

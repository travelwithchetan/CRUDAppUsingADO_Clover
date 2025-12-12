using CRUDAppUsingADO.Models;
using CRUDAppUsingADO.Unit_Testing;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace CRUDAppUsingADO
{
    public class EmployeeDataAccessLayer: IEmployeeDataAccessLayer
    {
        string cs = ConnectionString.dbcs;
        #region GetAllEmployee
        public List<Employees> GetAllEmployees()
        {
            List<Employees> emplist = new List<Employees>();
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
                    emplist.Add(emp);

                }
                return emplist;
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
            Employees emp = new Employees();
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

        #region Dropdownlist Method fro database call

        public void UpdateDesignation(int id, string designation)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Employee SET designation = @designation WHERE id = @id", con);
                cmd.Parameters.AddWithValue("@designation", designation);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                cmd.ExecuteNonQuery();
                
            }
        }

        #endregion


    }
}

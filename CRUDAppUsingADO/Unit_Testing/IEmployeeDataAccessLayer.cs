using CRUDAppUsingADO.Models;

namespace CRUDAppUsingADO.Unit_Testing
{
    public interface IEmployeeDataAccessLayer
    {
        List<Employees> GetAllEmployees();
        void AddEmployee(Employees emp);
        Employees getEmployeeById(int id);
        void UpdateEmployee(Employees emp);
        Employees GetEmployeeDetails(int id);
        void DeleteEmployee(int id);
        void UpdateDesignation(int id, string designation);
    }

}

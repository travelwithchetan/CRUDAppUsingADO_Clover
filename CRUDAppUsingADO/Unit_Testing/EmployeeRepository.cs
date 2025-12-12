using CRUDAppUsingADO.Models;

namespace CRUDAppUsingADO.Unit_Testing
{
    public class EmployeeRepository: IEmployeeDataAccessLayer
    {
        private readonly EmployeeDataAccessLayer _dal;
        public EmployeeRepository(EmployeeDataAccessLayer dal)
        {
            _dal = dal;
        }

        public void AddEmployee(Employees emp)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public List<Employees> GetAllEmployees()
        {
            throw new NotImplementedException();
        }

        public Employees getEmployeeById(int id)
        {
            throw new NotImplementedException();
        }

        public Employees GetEmployeeDetails(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateDesignation(int id, string designation)
        {
            throw new NotImplementedException();
        }

        public void UpdateEmployee(Employees emp)
        {
            throw new NotImplementedException();
        }


        //__________________________________
        //     public List<Employees> GetAllEmployees() => _dal.GetAllEmployees();
        //public void AddEmployee(Employees emp) => _dal.AddEmployee(emp);
        //public Employees GetEmployeeById(int id) => _dal.getEmployeeById(id);
        //public void UpdateEmployee(Employees emp) => _dal.UpdateEmployee(emp);
        //public Employees GetEmployeeDetails(int id) => _dal.GetEmployeeDetails(id);
        //public void DeleteEmployee(int id) => _dal.DeleteEmployee(id);
        //public void UpdateDesignation(int id, string designation) => _dal.UpdateDesignation(id, designation);
    }
}

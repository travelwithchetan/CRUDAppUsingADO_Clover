using System.Diagnostics;
using CRUDAppUsingADO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CRUDAppUsingADO.Controllers
{
    public class HomeController : BaseController /*Controller*/
    {
        private readonly EmployeeDataAccessLayer _dal;
        public HomeController(IMemoryCache cache)
        {
            _dal = new EmployeeDataAccessLayer(cache);
        }
        
        

        public IActionResult Index()
        {
            
            if (HttpContext.Session.GetString("username") == null)
                return RedirectToAction("Login", "Account");
            var employees=_dal.GetAllEmployees();
            return View(employees);
        
        }
        [HttpGet]
        public IActionResult Create()
        {
           return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employees emp)
        {
            try 
            {
                _dal.AddEmployee(emp);
                return RedirectToAction("Index");
            } 
            catch
            {
                return View();
            }
            
        }

        public IActionResult Edit(int id)
        {
            
                Employees emp = _dal.getEmployeeById(id);


            return View(emp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employees emp)
        {
            try
            {
                _dal.UpdateEmployee(emp);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Employees emp = _dal.GetEmployeeDetails(id);
            return View(emp);
        }




        public IActionResult Delete(int id)
        {
            Employees emp = _dal.GetEmployeeDetails(id);
            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employees emp) 
        {
            try 
            {
                _dal.DeleteEmployee(emp.id);

                return RedirectToAction("Index");
            } 
            catch 
            {
                return View();
            }
            
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

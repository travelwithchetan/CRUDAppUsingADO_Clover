using System.Diagnostics;
using CRUDAppUsingADO.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDAppUsingADO.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeeDataAccessLayer _dal;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger,EmployeeDataAccessLayer dal)
        {
           
            _logger = logger;
            _dal = dal;

            try
            {
                int x = 10;
                int y = 0;
                var result = x / y;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }


        public IActionResult LoggingIndex() 
        {
            _logger.LogInformation("Index Action Called");
            
            return View();
        }
        public void GetEmployees()
        {
            _logger.LogInformation("Fetching employee list...");
        }


        public IActionResult Index()
        {
            _logger.LogInformation("Index method called at {time}", DateTime.Now);
            var employees=_dal.GetAllEmployees();
            return View(employees);
        
        }
        [HttpGet]
        public IActionResult Create()
        {
            _logger.LogInformation("Create GET called at {time}", DateTime.Now);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employees emp)
        {
            _logger.LogInformation("Create POST called at {time}", DateTime.Now);
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

        [HttpPost]
        public IActionResult UpdateDesignation(int id, string designation)
        {
            _dal.UpdateDesignation(id, designation);
            return RedirectToAction("Index");
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

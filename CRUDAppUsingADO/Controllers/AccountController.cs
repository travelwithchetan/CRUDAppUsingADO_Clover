namespace CRUDAppUsingADO.Controllers
{
    using CRUDAppUsingADO.Models;
    using Microsoft.AspNetCore.Mvc;

    public class AccountController : Controller
    {
        
        EmployeeDataAccessLayer _dal=new EmployeeDataAccessLayer();

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (_dal.ValidateUser(model.Username, model.Password))
            {
                HttpContext.Session.SetString("username", model.Username);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid username or password";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }

}

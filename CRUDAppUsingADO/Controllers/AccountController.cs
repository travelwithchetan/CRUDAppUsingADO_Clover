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

        //public IActionResult Logout()
        //{
        //    HttpContext.Session.Clear();   // removes all session data
        //    return RedirectToAction("Login", "Account");
        //}
        #region SignUp Functionality Action Method
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(SignupViewModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                ViewBag.Error = "Passwords do not match";
                return View();
            }

            if (_dal.UserExists(model.Username))
            {
                ViewBag.Error = "User already exists";
                return View();
            }

            _dal.RegisterUser(model.Username, model.Password);
            return RedirectToAction("Login");
        }

        #endregion
    }

}

using Microsoft.AspNetCore.Mvc;

namespace CRUDAppUsingADO.Controllers
{
    public class BaseController :Controller 
    {
        protected bool IsLoggedIn()
        {
            return HttpContext.Session.GetString("username") != null;
        }
    }
}

using System.Web.Mvc;
using Training.Umbraco.WebSite.UI.Models;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace Training.Umbraco.WebSite.UI.Controllers
{
    public class LoginController : SurfaceController
    {
  

        public ActionResult Login(LoginModel model)
        {
            return CurrentUmbracoPage();
        }

        public ActionResult Logout()
        {
  

            return Redirect("/");
        }
    }
}
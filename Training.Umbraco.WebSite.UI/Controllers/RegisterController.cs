using System.Web.Mvc;
using Training.Umbraco.WebSite.UI.Models;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace Training.Umbraco.WebSite.UI.Controllers
{
    public class RegisterController : SurfaceController
    {
        public ActionResult Register(StoreRegisterModel model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();



            return Redirect("/");
        }

     
    }
}
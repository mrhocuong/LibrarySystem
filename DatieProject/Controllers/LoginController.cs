using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DatieProject.Models;

namespace DatieProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly LibrarySystemEntities _dBLibrarySystemEntities = new LibrarySystemEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HomePage()
        {
            return View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("HomePage", "Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Login");
            var checkLogin =
                _dBLibrarySystemEntities.Accounts.FirstOrDefault(
                    x => x.Account1.Equals(model.UserName) && x.Password.Equals(model.Password));
            var user = new ApplicationUser();
            if (checkLogin != null)
            {
                if ((bool) checkLogin.IsActive)
                {
                    var info = new Info
                    {
                        Username = checkLogin.Account1,
                        IsActive = (bool) checkLogin.IsActive,
                        IsAdminMaster = (bool) checkLogin.IsMasterAdmin
                    };
                    user.Info = info;
                    Session["User"] = user;
                    return RedirectToLocal(returnUrl);
                }
                ViewBag.Error = "This account is deactivated!";
            }
            else
            {
                ViewBag.Error = "Invalid username or password. Try Again!";
            }
            // If we got this far, something failed, redisplay form
            return RedirectToAction("Index", "Login");
        }

        //
        // POST: /Account/LogOff
        public ActionResult LogOff()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}
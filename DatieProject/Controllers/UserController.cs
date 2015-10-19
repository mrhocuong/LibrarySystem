using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Web.Mvc;
using DatieProject.Models;

namespace DatieProject.Controllers
{
    public class UserController : Controller
    {
        private readonly ComonData _comonData = new ComonData();
        private readonly LibrarySystemEntities _datieDb = new LibrarySystemEntities();
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            var data = _datieDb.Accounts.ToList();
            var dt = new List<UserModel>();
            data.ForEach(x =>
            {
                var tmp = new UserModel
                {
                    Account = x.Account1,
                    Id = x.Id,
                    Name = x.Name,
                    Class = x.Class,
                    Major = _comonData.GetMajor((int) x.Major),
                    Period = x.Period,
                    Phone = x.Phone,
                    Email = x.Email,
                    //TypeUser = _comonData.GetTypeUser((int) x.TypeUser),
                    TypeUser =x.TypeUser.ToString(),
                    CreateDate = Convert.ToDateTime(x.CreateDate).Date.ToString("dd-MM-yyyy"),
                    IsActive = x.IsActive
                };
                dt.Add(tmp);
            });

            return Json(new {data = dt.OrderBy(x => x.Account)}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeStatus(string id, string status)
        {
            //Check id is avaliable on database
            var data = _datieDb.Accounts.ToList().Find(x => x.Account1.Equals(id));
            var session = (ApplicationUser) Session["User"];
            if (!session.Info.IsAdminMaster || data == null)
                return Json(new {success = false}, JsonRequestBehavior.AllowGet);
            // if (data == null) return Json(new {success = false}, JsonRequestBehavior.AllowGet);
            //check status can change (deactivete to active)
            if (status.Equals("Active"))
            {
                //Check user login is admin master. Only admin master can active admin account
                data.IsActive = true;
            }
            //else, change active to deactive
            else
            {
                //Check user login is admin master. Only admin master can deactivate admin account
                data.IsActive = false;
            }
            _datieDb.Entry(data).State = EntityState.Modified;
            var check = _datieDb.SaveChanges();
            return Json(check > 0 ? new {success = true} : new {success = false}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUser(string id)
        {
            var data = _datieDb.Accounts.FirstOrDefault(x => x.Account1 == id);
            if (data!=null)
            {
                var dt = new User()
                {
                    NameSt = data.Name,
                    Class = data.Class,
                    Account = data.Account1,
                    Id = data.Id,
                    Period = Convert.ToInt32(data.Period),
                    Phone = data.Phone,
                    Email = data.Email,
                    TypeUser = (int) data.TypeUser,
                    MajorSt = (int) data.Major,
                };
                return Json(new { data = dt, success = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
    }
}

public class User
{
    public string Account { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string NameSt { get; set; }
    public string Class { get; set; }
    public int MajorSt { get; set; }
    public int TypeUser { get; set; }
    public string Id { get; set; }
    public int Period { get; set; }
}
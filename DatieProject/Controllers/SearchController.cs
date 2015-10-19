using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatieProject.Models;

namespace DatieProject.Controllers
{
    public class SearchController : Controller
    {
        private readonly LibrarySystemEntities _dbLibrarySystemEntities = new LibrarySystemEntities();
        private readonly ComonData _comonData = new ComonData();
        //
        // GET: /Search/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            var dataBook = _dbLibrarySystemEntities.Books.Where(x=>x.IsDelete==false).ToList();
            var listBook = new List<BookModel>();
            dataBook.ForEach(x =>
            {
                var dt = new BookModel()
                {
                    ISBN = x.ISBN,
                    Author = x.Author,
                    Name = x.Name,
                    TypeBook = _comonData.GetTypeBook((int)x.TypeBook),
                    AvailableInVault = (int) x.AvailableInVault
                };
                listBook.Add(dt);
            });
            return Json(new {data = listBook}, JsonRequestBehavior.AllowGet);
        }

       
        public JsonResult SearchBook(string searchKey)
        {
            throw new Exception();
        }
	}
}


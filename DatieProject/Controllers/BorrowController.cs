using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DatieProject.Models;

namespace DatieProject.Controllers
{
    public class BorrowController : Controller
    {
        private readonly ComonData _comonData = new ComonData();
        private readonly LibrarySystemEntities _datieDb = new LibrarySystemEntities();
        //
        // GET: /Borrow/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            var listBook = new List<BookModel>();
            return Json(new {data = listBook}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDatabyStudentId(string id)
        {
            var data = _datieDb.Accounts.FirstOrDefault(x => x.Id == id);
            var listBookModel = new List<BookModel>();
            if (data != null)
            {
                //get order
                var order = _datieDb.Orders.Where(x => x.Account == data.Account1).ToList();
                var account = _datieDb.Accounts.FirstOrDefault(x => x.Account1 == data.Account1);
                var user = new BookModel
                {
                    Phone = account.Phone,
                    Email = account.Email,
                    NameSt = account.Name,
                    Class = account.Class,
                    MajorSt = _comonData.GetMajor((int) account.Major),
                    TypeUser = _comonData.GetTypeUser((int) account.TypeUser)
                };
                listBookModel.Add(user);
                order.ForEach(x =>
                {
                    var listOrder = x.OrderDetails.ToList();
                    listOrder.ForEach(t =>
                    {
                        var tmp = _datieDb.BookDetails.FirstOrDefault(bk => bk.Id == t.BookId);
                        var isbn = tmp != null ? tmp.ISBN : "";
                        var book = _datieDb.Books.FirstOrDefault(bok => bok.ISBN == isbn);
                        var dt = new BookModel
                        {
                            ID = t.BookId,
                            Name = book.Name,
                            Author = book.Author,
                            BorrowDate = Convert.ToDateTime(x.DateBorrow).Date.ToString(),
                            ReturnDate = Convert.ToDateTime(x.DateReturn).Date.ToString(),
                            Status = _comonData.GetStatus((int) x.Status)
                        };
                        listBookModel.Add(dt);
                    });
                });
            }
            return Json(new {data = listBookModel}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDatabyBookId(string id)
        {
            var data = _datieDb.OrderDetails.FirstOrDefault(x => x.BookId == id);
            var listBookModel = new List<BookModel>();
            if (data != null)
            {
                //get order

                var order = _datieDb.Orders.FirstOrDefault(x => x.Id == data.OrderId);
                var account = _datieDb.Accounts.FirstOrDefault(x => x.Account1 == order.Account);
                var user = new BookModel
                {
                    Phone = account.Phone,
                    Email = account.Email,
                    NameSt = account.Name,
                    Class = account.Class,
                    MajorSt = _comonData.GetMajor((int)account.Major),
                    TypeUser = _comonData.GetTypeUser((int)account.TypeUser)
                };
                listBookModel.Add(user);
                var listOrder = order.OrderDetails.ToList();
                listOrder.ForEach(t =>
                {
                    var tmp = _datieDb.BookDetails.FirstOrDefault(bk => bk.Id == t.BookId);
                    var isbn = tmp != null ? tmp.ISBN : "";
                    var book = _datieDb.Books.FirstOrDefault(bok => bok.ISBN == isbn);
                    var dt = new BookModel
                    {
                        ID = t.BookId,
                        Name = book.Name,
                        Author = book.Author,
                        BorrowDate = Convert.ToDateTime(order.DateBorrow).Date.ToString(),
                        ReturnDate = Convert.ToDateTime(order.DateReturn).Date.ToString(),
                        Status = _comonData.GetStatus((int) order.Status)
                    };
                    listBookModel.Add(dt);
                });
            }
            return Json(new {data = listBookModel}, JsonRequestBehavior.AllowGet);
        }
    }
}
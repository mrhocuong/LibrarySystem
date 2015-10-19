using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using DatieProject.Models;

namespace DatieProject.Controllers
{
    public class DatieController : Controller
    {
        private readonly LibrarySystemEntities _datieDb = new LibrarySystemEntities();
        private readonly ComonData _comonData = new ComonData();

        //
        // GET: /Datie/
        public ActionResult Index()
        {
            return View();
        }

        //#region Add

        public PartialViewResult AddShop()
        {
            var model = new BookModel();
          
            return PartialView(model);
        }


        //[HttpPost]
        //public JsonResult Add(DatieModel model)
        //{
        //    var myFile = Request.Files;
        //    var data = new tbl_Shop
        //    {
        //        name = model.ShopName,
        //        address = model.ShopAddress,
        //        description = model.ShopDescription,
        //        averageRate = Convert.ToDouble(model.ShopRate),
        //        phone = model.ShopPhone,
        //        price_medium = Convert.ToDouble(model.ShopPriceMid),
        //        time_medium = Convert.ToDouble(model.ShopTimeMid),
        //        id_district = Convert.ToInt32(model.DistrictId),
        //        id_food = Convert.ToInt32(model.FoodId),
        //        isDelete = false
        //    };
        //    _datieDb.tbl_Shop.Add(data);
        //    var check = _datieDb.SaveChanges();
        //    return Json(check > 0 ? new {success = true} : new {success = false}, JsonRequestBehavior.AllowGet);
        //}

        //public bool UploadImage(HttpPostedFileBase file)
        //{
        //    const string apiKey = "6d3483e6373d658";
        //    const string apiSecret = "07e2d23520b7c8d17288665358a881b26780d7bc";
        //    var imageData = new byte[file.ContentLength];
        //    const int MAX_URI_LENGTH = 32766;
        //    var base64img = Convert.ToBase64String(imageData);
        //    var sb = new StringBuilder();
        //    for (var i = 0; i < base64img.Length; i += MAX_URI_LENGTH)
        //    {
        //        sb.Append(Uri.EscapeDataString(base64img.Substring(i, Math.Min(MAX_URI_LENGTH, base64img.Length - i))));
        //    }

        //    var uploadRequestString = "client_id" + apiKey + "client_secret" + apiSecret + "&title=" + "imageTitle" +
        //                              "&caption=" + "img" + "&image=" + sb;

        //    var webRequest = (HttpWebRequest) WebRequest.Create("https://api.imgur.com/3/upload.xml");
        //    //Authorization with project ID
        //    webRequest.Headers.Add("Authorization", "Client-ID e8d2e289bf6fbba");
        //    webRequest.Method = "POST";
        //    webRequest.ContentType = "application/x-www-form-urlencoded";
        //    webRequest.ServicePoint.Expect100Continue = false;

        //    var streamWriter = new StreamWriter(webRequest.GetRequestStream());

        //    streamWriter.Write(uploadRequestString);
        //    streamWriter.Close();

        //    var response = webRequest.GetResponse();
        //    var responseStream = response.GetResponseStream();
        //    var responseReader = new StreamReader(responseStream);
        //    var responseString = responseReader.ReadToEnd();


        //    var regex = new Regex("<link>(.*)</link>");
        //    //regex.ToString();
        //    var txtLink = regex.Match(responseString).Groups[1].ToString();
        //    return true;
        //}

        //#endregion

        #region Edit

        public PartialViewResult EditShop(string id)
        {
            var data = _datieDb.BookDetails.FirstOrDefault(x => x.Id == id);
            if (data != null)
            {
                var tmp = _datieDb.Books.FirstOrDefault(y => y.ISBN == data.ISBN);
                var model = new BookModel()
                {
                    ISBN = data.ISBN,
                    ID = data.Id,
                    Author = tmp.Author,
                    Name = tmp.Name,
                    TypeBook = _comonData.GetTypeBook((int)tmp.TypeBook),
                    AvailableInVault = (int)tmp.AvailableInVault,
                    Description = tmp.Description,
                    Major = _comonData.GetMajor((int)tmp.Major),
                    IsDelete = (bool)tmp.IsDelete,
                    IsBorrow = (bool)data.IsBorrowed
                    
                };
                return PartialView(model);
            }
            return PartialView();
        }

        //[HttpPost]
        //public JsonResult EditShop(DatieModel model)
        //{
        //    var data = _datieDb.tbl_Shop.ToList().Find(x => x.id_shop.ToString() == model.ShopId);
        //    if (data == null) return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        //    data.name = model.ShopName;
        //    data.address = model.ShopAddress;
        //    data.description = model.ShopDescription;
        //    data.phone = model.ShopPhone;
        //    data.price_medium = Convert.ToDouble(model.ShopPriceMid);
        //    data.time_medium = Convert.ToDouble(model.ShopTimeMid);
        //    data.id_district = Convert.ToInt32(model.DistrictId);
        //    data.id_food = Convert.ToInt32(model.FoodId);
        //    data.isDelete = model.ShopIsDeleted;
        //    _datieDb.Entry(data).State = EntityState.Modified;
        //    var check = _datieDb.SaveChanges();
        //    return Json(check <= 0 ? new { success = false } : new { success = true }, JsonRequestBehavior.AllowGet);
        //}

        #endregion

        #region Get Data

        public JsonResult GetData()
        {
            var data = _datieDb.BookDetails.ToList();
            var listBook = new List<BookModel>();
            data.ForEach(x =>
            {
                var tmp = _datieDb.Books.FirstOrDefault(y => y.ISBN == x.ISBN);
                var dt = new BookModel()
                {
                    ISBN = x.ISBN,
                    ID = x.Id,
                    Author = tmp.Author,
                    Name = tmp.Name,
                    TypeBook = _comonData.GetTypeBook((int)tmp.TypeBook),
                    AvailableInVault = (int)tmp.AvailableInVault,
                    Description = tmp.Description,
                    Major = _comonData.GetMajor((int)tmp.Major),
                    IsDelete = (bool)x.IsDelete,
                    IsBorrow = (bool)x.IsBorrowed
                };
                listBook.Add(dt);
            });
            return Json(new { data = listBook.OrderBy(x => x.ISBN) }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
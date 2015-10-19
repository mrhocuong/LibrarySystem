using System;
using System.Collections.Generic;

namespace DatieProject.Models
{
    public class DatieModel
    {
        public string ShopId { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public string DistrictId { get; set; }
        public string FoodId { get; set; }
        public string ShopPhone { get; set; }
        public string ShopDescription { get; set; }
        public string ShopPriceMid { get; set; }
        public string ShopTimeMid { get; set; }
        public bool ShopIsDeleted { get; set; }
        public string ShopRate { get; set; }
        public IEnumerable<DistrictModel> DistrictList { get; set; }
        public IEnumerable<FoodModel> Food { get; set; }
        public string[] ImgCollection { get; set; }
        public List<ImageModel> Image { get; set; }
    }

    public class ImageModel
    {
        public int ImgId { get; set; }
        public string ImgLink { get; set; }
    }

    public class BookModel
    {
        public string ISBN { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Major { get; set; }
        public string TypeBook { get; set; }
        public int AvailableInVault { get; set; }
        public int Amount { get; set; }
        public bool IsDelete { get; set; }
        public bool IsBorrow { get; set; }
        public IEnumerable<TypeBook> TypeBookList { get; set; }
        public IEnumerable<MajorType> MajorTypeList { get; set; }
        public string BorrowDate { get; set; }
        public string ReturnDate { get; set; }
        public string Status { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string NameSt { get; set; }
        public string Class { get; set; }
        public string MajorSt { get; set; }
      
        public string TypeUser { get; set; }

    }
}
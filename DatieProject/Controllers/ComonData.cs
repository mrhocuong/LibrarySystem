using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatieProject.Controllers
{
    public class ComonData
    {
        private readonly LibrarySystemEntities _dbLibrarySystemEntities = new LibrarySystemEntities();

        public string GetTypeBook(int id)
        {
            var data = _dbLibrarySystemEntities.TypeBooks.FirstOrDefault(x => x.Id == id);
            return data != null ? data.TypeBook1 : "";
        }

        public string GetMajor(int id)
        {
            var firstOrDefault = _dbLibrarySystemEntities.MajorTypes.FirstOrDefault(x => x.Id == id);
            if (firstOrDefault != null)
                return firstOrDefault.MajorType1;
            return "";
        }

        public string GetTypeUser(int id)
        {
            var firstOrDefault = _dbLibrarySystemEntities.TypeUsers.FirstOrDefault(x => x.Id == id);
            if (firstOrDefault != null)
                return firstOrDefault.TypeUser1;
            return "";
        }

        public string GetStatus(int id)
        {
            var firstOrDefault = _dbLibrarySystemEntities.Status.FirstOrDefault(x => x.Id == id);
            if (firstOrDefault != null)
                return firstOrDefault.Status1;
            return "";
        }
    }
}
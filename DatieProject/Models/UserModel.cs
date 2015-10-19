namespace DatieProject.Models
{
    public class UserModel
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CreateDate { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public string Major { get; set; }
        public string Period { get; set; }
        public string TypeUser { get; set; }
        public bool? IsActive { get; set; }
    }
}
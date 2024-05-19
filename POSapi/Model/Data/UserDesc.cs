using System.ComponentModel.DataAnnotations;

namespace POSapi.Model.Data
{
    public class UserDesc
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}

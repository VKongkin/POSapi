using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POSapi.Model.Data
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string ImgagePath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POSapi.Model.Data
{
    public class Vendor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorID { get; set; }
        public string VendorName { get; set; }
        public string VendorPhone { get; set; }
        public string VendorDescription { get; set;}
        public string VendorEmail { get; set;}
        public string VendorAddress { get; set;}
        public int isActive { get; set; }
        public string ImgagePath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace POSapi.Model.Data
{
    public class PurchaseOrder
    {
        [Key]
        public int PoId { get; set; }
        public DateTime PoDate { get; set; }
        public int VendorId { get; set; }
        public int UserId { get; set; }
        public string Reference { get; set; }
        public float PoAmount { get; set; }
        public List<PODetail> Details { get; set; }
        
    }
}

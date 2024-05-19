using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POSapi.Model.Data
{
    public class PODetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PoDetailId { get; set; }
        
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public float Price { get; set; }
        public float Amount { get; set; }
        public int PoId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
    }
}

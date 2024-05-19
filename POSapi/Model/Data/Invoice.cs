using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POSapi.Model.Data
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set;}
        public DateTime InvoiceDate { get; set;}
        public int CustomerId { get; set; }
        public float InvoiceAmount { get; set; }
        public float InvoiceDiscount { get; set; }
        public float InvoiceDeposit { get; set; }
        public float InvoiceBalance { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}

using POSapi.Model.Data;

namespace POSapi.Model.Request
{
    public class InvoiceRequest
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int CustomerId { get; set; }
        public float InvoiceAmount { get; set; }
        public float InvoiceDiscount { get; set; }
        public float InvoiceDeposit { get; set; }
        public float InvoiceBalance { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public List<RequestInvoiceItem> InvoiceItems { get; set; }
    }
}

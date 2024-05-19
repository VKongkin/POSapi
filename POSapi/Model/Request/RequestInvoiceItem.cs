namespace POSapi.Model.Request
{
    public class RequestInvoiceItem
    {
        public int InvoiceItemId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public float Price { get; set; }
        public float Amount { get; set; }
        public int InvoiceId { get; set; }
    }
}

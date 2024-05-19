namespace POSapi.Model.Request
{
    public class PODetailRequest
    {
        public int PoDetailId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public float Price { get; set; }
        public float Amount { get; set; }
        public int PoId { get; set; }
    }
}

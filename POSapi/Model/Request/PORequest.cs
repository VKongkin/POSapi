using POSapi.Model.Data;

namespace POSapi.Model.Request
{
    public class PORequest
    {
        public int PoId { get; set; }
        public DateTime PoDate { get; set; }
        public int VendorId { get; set; }
        public int UserId { get; set; }
        public string Reference { get; set; }
        public float PoAmount { get; set; }
        public string CUD {  get; set; }
        public List<PODetailRequest> Details { get; set; }
    }
}

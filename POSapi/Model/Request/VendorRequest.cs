namespace POSapi.Model.Request
{
    public class VendorRequest
    {
        public int VendorID { get; set; }
        public string VendorName { get; set; }
        public string VendorPhone { get; set; }
        public string VendorDescription { get; set; }
        public string VendorEmail { get; set; }
        public string VendorAddress { get; set; } 
        public int isActive { get; set; }
        public string CUD {  get; set; }
    }
}

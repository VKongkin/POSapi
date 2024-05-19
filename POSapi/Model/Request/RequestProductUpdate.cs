namespace POSapi.Model.Request
{
    public class RequestProductUpdate
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        //public Category Category { get; set; }
        public int skuID { get; set; }
        public int Qty { get; set; }
        public float UnitCost { get; set; }
        public float UnitPrice { get; set; }
        public int isActive { get; set; }
        public int VendorID { get; set; }
        public string ImagePath { get; set; }
    }
}

namespace POSapi.Model.Request
{
    public class RoleRequest
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public int IsActive { get; set; }
        public string CreateBy { get; set; }
    }
}

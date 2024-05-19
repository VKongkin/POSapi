namespace POSapi.Model.Request
{
    public class UserRequest
    {
        public int UserDescId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Status { get; set; } // 1 Active, 0 Inactive, 2 Suspend
        public string CreateBy { get; set; }
        public int RoleId { get; set; }
        public string CUD { get; set; } // C: Create, U: Update, D: Delete
    }
}

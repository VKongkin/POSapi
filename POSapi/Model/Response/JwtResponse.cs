namespace POSapi.Model.Response
{
    public class JwtResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string JwtToken { get; set; }
        public int ExpireIn { get; set; }
    }
}

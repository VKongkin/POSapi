using System.ComponentModel.DataAnnotations;

namespace POSapi.Model.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage ="Please Enter Your Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please Enter Your Password")]
        public string Password { get; set; }
    }
}

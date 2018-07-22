using System.ComponentModel.DataAnnotations;

namespace IdentityApp.Models
{
    public class SigninModel
    {
        [Required(ErrorMessage ="Need to write the username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Need to write the password")]
        public string Password { get; set; }
    }
}

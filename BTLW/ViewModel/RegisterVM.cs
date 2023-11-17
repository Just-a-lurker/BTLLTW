using System.Web;
using System.Linq;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace BTLW.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Username cannot be blank")]
        public string TenTK { get; set; }
        [Required(ErrorMessage = "Password cannot be blank")]
        public string MatKhau { get; set; }
        [Required(ErrorMessage = "Confirm Password cannot be blank")]
        [Compare("MatKhau", ErrorMessage ="Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; }

    }
}

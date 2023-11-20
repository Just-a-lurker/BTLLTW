using System.Web;
using System.Linq;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace BTLW.ViewModel
{
    public class RegisterVM
    {
        [Key]
        public int  MaTK { get; set; }
        [Required(ErrorMessage ="Username cannot be blank")]
        [RegularExpression(@"^[a-zA-Z0-9]{3,16}$", ErrorMessage = "Username errored")]
        public string TenTK { get; set; }
        [Required(ErrorMessage = "Password cannot be blank")]
        public string MatKhau { get; set; }
        
        [Required(ErrorMessage = "Confirm Password cannot be blank")]
        [Compare("MatKhau", ErrorMessage ="Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "TOA cannot be blank")]
        [RegularExpression(@"^[01]$", ErrorMessage = "Sai roi chi co 0 vs 1 thoi bich")]
        public bool LoaiTK { get; set; }    

    }
}

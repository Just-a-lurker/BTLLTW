using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BTLW.Models
{
    public class User
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MaTK { get; set; }
        [Required(ErrorMessage = "TenTK cannot be blank")]
        [StringLength(50, MinimumLength = 3)]
        public string TenTK { get; set; }

        [Required(ErrorMessage = "MatKhau cannot be blank")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]

        public string MatKhau { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "ConfirmPassword cannot be blank")]
        [System.ComponentModel.DataAnnotations.Compare("MatKhau")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "LoaiTK cannot be blank")]
        [RegularExpression(@"^[01]$", ErrorMessage = "Sai roi chi co 0 vs 1 thoi bich")]
        public string LoaiTK { get; set; }

    }
}

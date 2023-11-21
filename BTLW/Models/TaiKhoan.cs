using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTLW.Models;

public partial class TaiKhoan
{
    public int MaTk { get; set; }
    [Required(ErrorMessage = "TenTK cannot be blank")]
    [StringLength(50, MinimumLength = 3)]
    public string? TenTk { get; set; }
    [Required(ErrorMessage = "MatKhau cannot be blank")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
    public string? MatKhau { get; set; }

    public bool? LoaiTk { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NegareshNo.Core.ViewModels.Role
{
    public class RoleEditVM
    {
        [Key]
        public int RoleId { get; set; }

        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست!")]
        public string RoleTitle { get; set; }

        public int[] PermmisionsId { get; set; }
    }
}

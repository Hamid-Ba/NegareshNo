using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NegareshNo.Core.ViewModels.Consultant
{
    public class ConsultantGroupIndexVM
    {
        [Key]
        public int GroupId { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [MaxLength(25)]
        public string GroupTitle { get; set; }

        [Display(Name = "توضیح مختصر")]
        [MaxLength(150)]
        public string Summery { get; set; }
    }
}

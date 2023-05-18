using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NegareshNo.Data.Model.Consulting
{
    public class ConsultingGroup
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

        [Display(Name = "تصویر گروه")]
        public string ImageName { get; set; }

        [Display(Name = "حذف شده ؟")]
        public bool IsDelete { get; set; }

        #region Relations
        public List<Consultant_Group> Consultant_Groups { get; set; }
        #endregion
    }
}

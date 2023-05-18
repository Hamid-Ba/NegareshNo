using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NegareshNo.Core.ViewModels.Consultant
{
    public class ArticleAdminIndexVM
    {
        [Key]
        public int ArticleId { get; set; }

        [Display(Name = "مشاور")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        public string ConsultantName { get; set; }

        [Display(Name = "عنوان مقاله")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [MaxLength(30)]
        public string Title { get; set; }

        [Display(Name = "خلاصه ای از مقاله")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [MaxLength(150)]
        public string Summery { get; set; }

        [Display(Name = "بر روی سایت منتشر شود؟")]
        public bool IsPublished { get; set; }

        [Display(Name = "تاریخ انتشار مقاله")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [DataType(DataType.DateTime)]
        public DateTime PublishedDate { get; set; }
    }
}

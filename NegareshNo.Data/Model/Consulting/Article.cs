using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NegareshNo.Data.Model.Consulting
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }

        [Display(Name = "کد مشاور")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        public int ConsultingId { get; set; }

        [Display(Name = "عنوان مقاله")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [MaxLength(30)]
        public string Title { get; set; }

        [Display(Name = "خلاصه ای از مقاله")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [MaxLength(150)]
        public string Summery { get; set; }

        [Display(Name = "متن مقاله")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "تاریخ انتشار مقاله")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [DataType(DataType.DateTime)]
        public DateTime PublishedDate { get; set; }

        [Display(Name = "بر روی سایت منتشر شود؟")]
        public bool IsPublished { get; set; }

        [Display(Name = "حذف شده؟")]
        public bool IsDelete { get; set; }

        #region Relation

        [ForeignKey("ConsultingId")]
        public Consultant Consultant { get; set; }

        #endregion
    }
}

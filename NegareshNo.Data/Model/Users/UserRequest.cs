using NegareshNo.Data.Model.Consulting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NegareshNo.Data.Model.Users
{
    public class UserRequest
    {
        [Key]
        public int RequestId { get; set; }

        [Display(Name = "شناسه نوع مشاوره")]
        [Required]
        public int GroupId { get; set; }

        [Display(Name = "کد مشاور")]
        [Required]
        public int ConsultantId { get; set; }

        //[Display(Name = "کد کاربری")]
        //public int? UserId { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [MaxLength(50)]
        public string FullName { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "شماره تلفن")]
        [DataType(DataType.PhoneNumber)]
        public string TellPhoneNumber { get; set; }

        [Display(Name = "موضوع")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "زمان ثبت  شده درخواست")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [DataType(DataType.DateTime)]
        public DateTime RegistrationConsultingTime { get; set; }

        [Display(Name = "زمان داد شده")]
        [DataType(DataType.DateTime)]
        public DateTime GivenTime { get; set; }

        [Display(Name = "آیا مشاوره تکمیل شده ؟")]
        public bool IsDone { get; set; }

        [Display(Name = "آیا وقت تنظیم شده؟")]
        public bool HasTime { get; set; }

        [Display(Name = "حذف شده ؟")]
        public bool IsDelete { get; set; }

        #region Relation
        [ForeignKey("ConsultantId")]
        public Consultant Consultant { get; set; }

        [ForeignKey("GroupId")]
        public ConsultingGroup ConsultingGroup { get; set; }

        //[ForeignKey("UserId")]
        //public User User { get; set; }
        #endregion
    }
}

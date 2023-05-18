using NegareshNo.Data.Model.Access;
using NegareshNo.Data.Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NegareshNo.Data.Model.Consulting
{
    public class Consultant
    {
        [Key]
        public int ConsultantId { get; set; }

        [Display(Name = "نام کامل")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [MaxLength(30)]
        public string ConsultantFullName { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [MaxLength(30)]
        public string Password { get; set; }

        [Display(Name = "رزومه")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [MaxLength(200)]
        public string CareerSummary { get; set; }

        [Display(Name = "درباره من")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [DataType(DataType.MultilineText)]
        public string CareerRecord { get; set; }

        [Display(Name = "تصویر")]
        public string ImageName { get; set; }

        [Display(Name = "شغل")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [MaxLength(15)]
        public string Job { get; set; }

        [Display(Name = "متولد")]
        [DataType(DataType.DateTime)]
        public DateTime Birthday { get; set; }

        [Display(Name = "وضعیت تاهل")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        public bool Status { get; set; }

        [Display(Name = "پست الکترونیک")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "شماره تماس")]
        [DataType(DataType.PhoneNumber)]
        public string MobilePhone { get; set; }

        [Display(Name = "وب سایت")]
        public string Website { get; set; }

        [Display(Name = "محل زندگی")]
        public string City { get; set; }

        [Display(Name = "سوابق تحصیلی")]
        [DataType(DataType.MultilineText)]
        public string EducationalRecord { get; set; }

        [Display(Name = "سوابق شغلی")]
        [DataType(DataType.MultilineText)]
        public string WorkRecord { get; set; }

        [Display(Name = "دوره ها و گواهینامه ها")]
        [DataType(DataType.MultilineText)]
        public string LiceneRecord { get; set; }

        [Display(Name = "مهارت ها")]
        public string Skills { get; set; }

        [Display(Name = "حذف شده؟")]
        public bool IsDelete { get; set; }

        #region Relation
        public List<Article> Articles { get; set; }
        public List<UserRequest> UserRequests { get; set; }
        public List<Role_Consultant> Role_Consultants { get; set; }
        public List<Consultant_Group> Consultant_Groups { get; set; }
        #endregion
    }
}

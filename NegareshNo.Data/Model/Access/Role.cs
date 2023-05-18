using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NegareshNo.Data.Model.Access
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست")]
        [MaxLength(30)]
        public string RoleTitle { get; set; }

        [Display(Name = "حذف شده ؟")]
        public bool IsDelete { get; set; }

        #region Relations
        public List<Role_Permmision> Role_Permmisions { get; set; }
        public List<Role_Consultant> Role_Consultants { get; set; }
        //public List<Users.User> Users { get; set; }
        #endregion
    }
}

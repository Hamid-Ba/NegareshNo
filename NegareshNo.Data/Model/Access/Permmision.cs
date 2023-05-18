using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NegareshNo.Data.Model.Access
{
    public class Permmision
    {
        [Key]
        public int PermmisionId { get; set; }

        [Display(Name = "عنوان دسترسی")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست!")]
        public string PermmisionTitle { get; set; }

        public int? ParentId { get; set; }

        #region Relations
        public List<Role_Permmision> Role_Permmisions { get; set; }
        #endregion
    }
}

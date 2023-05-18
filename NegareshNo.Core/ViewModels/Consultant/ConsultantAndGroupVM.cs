using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NegareshNo.Core.ViewModels.Consultant
{
    public class ConsultantAndGroupIndexVM
    {
        public int ConsultantId { get; set; }
        public string[] GroupId { get; set; }

        public string ConsultantName { get; set; }
    }

    public class ConsultantAndGroupEditVM
    {
        [Display(Name = "نام مشاور")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامیست!")]
        public string ConsultantName { get; set; }

        public int ConsultantId { get; set; }

        public int[] GroupsId{ get; set; }
    }
}

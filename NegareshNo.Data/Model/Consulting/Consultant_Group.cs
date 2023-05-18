using System;
using System.Collections.Generic;
using System.Text;

namespace NegareshNo.Data.Model.Consulting
{
    public class Consultant_Group
    {
        public int ConsultantId { get; set; }
        public int GroupId { get; set; }

        public Consultant Consultant{ get; set; }
        public ConsultingGroup Group { get; set; }
    }
}

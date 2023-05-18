using NegareshNo.Data.Model.Consulting;
using System;
using System.Collections.Generic;
using System.Text;

namespace NegareshNo.Data.Model.Access
{
    public class Role_Consultant
    {
        public int RoleId { get; set; }
        public int ConsultantId { get; set; }

        public Role Role { get; set; }
        public Consultant Consultant { get; set; }
    }
}

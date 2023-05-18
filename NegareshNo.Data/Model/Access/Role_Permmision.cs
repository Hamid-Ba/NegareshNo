using System;
using System.Collections.Generic;
using System.Text;

namespace NegareshNo.Data.Model.Access
{
    public class Role_Permmision
    {
        public int RoleId { get; set; }
        public int PermmisionId { get; set; }

        public Role Role { get; set; }
        public Permmision Permmision { get; set; }
    }
}

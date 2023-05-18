using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NegareshNo.Data.Model.Access;
using System;
using System.Collections.Generic;
using System.Text;

namespace NegareshNo.Data.Mapping
{
    public class Role_PermmisionMap : IEntityTypeConfiguration<Role_Permmision>
    {
        public void Configure(EntityTypeBuilder<Role_Permmision> builder)
        {
            builder.HasKey(k => new { k.RoleId, k.PermmisionId });

            builder.HasOne(r => r.Role).
                WithMany(rp => rp.Role_Permmisions).
                HasForeignKey(f => f.RoleId);

            builder.HasOne(r => r.Permmision).
                WithMany(rp => rp.Role_Permmisions).
                HasForeignKey(f => f.PermmisionId);
        }
    }
}

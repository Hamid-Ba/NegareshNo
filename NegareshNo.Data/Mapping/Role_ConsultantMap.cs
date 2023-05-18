using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NegareshNo.Data.Model.Access;
using System;
using System.Collections.Generic;
using System.Text;

namespace NegareshNo.Data.Mapping
{
    public class Role_ConsultantMap : IEntityTypeConfiguration<Role_Consultant>
    {
        public void Configure(EntityTypeBuilder<Role_Consultant> builder)
        {
            builder.HasKey(k => new { k.RoleId, k.ConsultantId });

            builder.HasOne(r => r.Role).
                WithMany(rc => rc.Role_Consultants)
                .HasForeignKey(f => f.RoleId);

            builder.HasOne(c => c.Consultant).
                WithMany(rc => rc.Role_Consultants).
                HasForeignKey(f => f.ConsultantId);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NegareshNo.Data.Model.Consulting;
using System;
using System.Collections.Generic;
using System.Text;

namespace NegareshNo.Data.Mapping
{
    public class Consultant_GroupMap : IEntityTypeConfiguration<Consultant_Group>
    {
        public void Configure(EntityTypeBuilder<Consultant_Group> builder)
        {
            builder.HasKey(k => new { k.ConsultantId, k.GroupId });

            builder.HasOne(c => c.Consultant).
                WithMany(cg => cg.Consultant_Groups).
                HasForeignKey(f => f.ConsultantId);

            builder.HasOne(g => g.Group).
                WithMany(cg => cg.Consultant_Groups).
                HasForeignKey(f => f.GroupId);
        }
    }
}

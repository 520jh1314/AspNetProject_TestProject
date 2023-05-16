// -----------------------------------------------------------------------------
// Generate By Furion Tools v4.8.7.36
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using QProject.Core;

namespace QProject.Core;

public partial class ProjectFileMean : IEntity<MasterDbContextLocator>, IEntityTypeBuilder<ProjectFileMean, MasterDbContextLocator>
{
    public string FCID { get; set; }

    public string FatherNode { get; set; }

    public string ListName { get; set; }

    public string ListMenuCode { get; set; }

    public int? MenuLevel { get; set; }

    public DateTime? CreateTime { get; set; }

    public DateTime? EndTime { get; set; }

        public void Configure(EntityTypeBuilder<ProjectFileMean> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasKey(e => e.FCID);

            entityBuilder.Property(e => e.FCID)
                .HasMaxLength(50)
                .IsUnicode(false);
            entityBuilder.Property(e => e.CreateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entityBuilder.Property(e => e.EndTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entityBuilder.Property(e => e.FatherNode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entityBuilder.Property(e => e.ListMenuCode)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entityBuilder.Property(e => e.ListName)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode(false);
        }
}

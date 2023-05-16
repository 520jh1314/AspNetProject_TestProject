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

public partial class ProjectFileInfo : IEntity<MasterDbContextLocator>, IEntityTypeBuilder<ProjectFileInfo, MasterDbContextLocator>
{
    public string FIID { get; set; }

    /// <summary>
    /// 文件ID
    /// </summary>
    public string FCID { get; set; }

    public string FlieName { get; set; }

    public string UploadName { get; set; }

    public string Unit { get; set; }

    public string IdCardInform { get; set; }

    public DateTime? CreateTime1 { get; set; }

    public DateTime? EndTime { get; set; }

        public void Configure(EntityTypeBuilder<ProjectFileInfo> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasKey(e => e.FIID).HasName("PK_ProjectFileInform");

            entityBuilder.Property(e => e.FIID).HasMaxLength(100);
            entityBuilder.Property(e => e.CreateTime1)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entityBuilder.Property(e => e.EndTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entityBuilder.Property(e => e.FCID)
                .HasMaxLength(100)
                .HasComment("文件ID");
            entityBuilder.Property(e => e.FlieName).HasMaxLength(100);
            entityBuilder.Property(e => e.Unit).HasMaxLength(100);
            entityBuilder.Property(e => e.UploadName).HasMaxLength(100);
        }
}

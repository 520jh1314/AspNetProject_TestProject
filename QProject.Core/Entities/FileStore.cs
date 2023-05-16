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

public partial class FileStore : IEntity<MasterDbContextLocator>, IEntityTypeBuilder<FileStore, MasterDbContextLocator>
{
    /// <summary>
    /// 主键
    /// </summary>
    public string FCID { get; set; }

    /// <summary>
    /// 文件名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 文件后缀名
    /// </summary>
    public string SuffixName { get; set; }

    /// <summary>
    /// 文件路径
    /// </summary>
    public string FilePath { get; set; }

    /// <summary>
    /// 文件以byte格式存储
    /// </summary>
    public byte[] FilePathByte { get; set; }

    /// <summary>
    /// 是否删除(1 存在； 2 不存在)
    /// </summary>
    public int? IsDeleted { get; set; }

        public void Configure(EntityTypeBuilder<FileStore> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasKey(e => e.FCID);

            entityBuilder.Property(e => e.FCID)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("主键");
            entityBuilder.Property(e => e.FilePath)
                .IsUnicode(false)
                .HasComment("文件路径");
            entityBuilder.Property(e => e.FilePathByte).HasComment("文件以byte格式存储");
            entityBuilder.Property(e => e.IsDeleted).HasComment("是否删除");
            entityBuilder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("文件名称");
            entityBuilder.Property(e => e.SuffixName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("文件后缀名");
        }
}

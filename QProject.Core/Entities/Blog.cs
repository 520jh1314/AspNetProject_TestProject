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

public partial class Blog : IEntity<MasterDbContextLocator>, IEntityTypeBuilder<Blog, MasterDbContextLocator>
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public int CreateUserId { get; set; }

    public DateTime CreatedTime { get; set; }

    public virtual User CreateUser { get; set; }

    public virtual ICollection<UserBlog> UserBlog { get; } = new List<UserBlog>();

        public void Configure(EntityTypeBuilder<Blog> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasKey(e => e.Id).HasName("PK__Blog__3214EC07C0A44F96");

            entityBuilder.Property(e => e.Content)
                .IsRequired()
                .HasColumnType("text");
            entityBuilder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entityBuilder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(256);

            entityBuilder.HasOne(d => d.CreateUser).WithMany(p => p.Blog)
                .HasForeignKey(d => d.CreateUserId)
                .HasConstraintName("FK_Blog_User");
        }
}

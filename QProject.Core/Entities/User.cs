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

public partial class User : IEntity<MasterDbContextLocator>, IEntityTypeBuilder<User, MasterDbContextLocator>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime? Birthday { get; set; }

    public DateTime CreatedTime { get; set; }

    public virtual ICollection<Blog> Blog { get; } = new List<Blog>();

    public virtual ICollection<UserBlog> UserBlog { get; } = new List<UserBlog>();

        public void Configure(EntityTypeBuilder<User> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasKey(e => e.Id).HasName("PK__User__3214EC07687BC3C8");

            entityBuilder.Property(e => e.Birthday).HasColumnType("datetime");
            entityBuilder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entityBuilder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(64);
        }
}

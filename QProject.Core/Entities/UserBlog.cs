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

public partial class UserBlog : IEntity<MasterDbContextLocator>, IEntityTypeBuilder<UserBlog, MasterDbContextLocator>
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int BlogId { get; set; }

    public int? StarStarte { get; set; }

    public virtual Blog Blog { get; set; }

    public virtual User User { get; set; }

        public void Configure(EntityTypeBuilder<UserBlog> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasKey(e => e.Id).HasName("PK__UserBlog__3214EC07C7AD2AA9");

            entityBuilder.HasOne(d => d.Blog).WithMany(p => p.UserBlog)
                .HasForeignKey(d => d.BlogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserBlog_Blog");

            entityBuilder.HasOne(d => d.User).WithMany(p => p.UserBlog)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserBlog_User");
        }
}

using Data.DataConfig;
using Domain.Entities;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class DataContext: IdentityDbContext<User , Role, int,
                                               IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                               IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<TaskEntity> Task { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserRole>(UserRole =>
            {
                UserRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                UserRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

                UserRole.HasOne(ur => ur.User)
               .WithMany(r => r.UserRoles)
               .HasForeignKey(ur => ur.UserId)
               .IsRequired();
            });

            builder.Entity<TaskEntity>(new Configurations().ConfigureTask);            
            base.OnModelCreating(builder);
        }
     
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DB
{
    public partial class CoreContext : DbContext
    {
        public virtual DbSet<UserAccount> UserAccount { get; set; }

        public CoreContext(DbContextOptions<CoreContext> options) 
            : base(options)
        {
           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-RNAENQ8\SQLEXPRESS;Initial Catalog=Core;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.Property(e => e.Account).IsRequired();

                entity.Property(e => e.Pwd)
                    .IsRequired()
                    .HasMaxLength(20);
            });
        }
    }
}

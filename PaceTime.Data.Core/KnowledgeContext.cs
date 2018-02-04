using System;
using Microsoft.EntityFrameworkCore;
using PaceTime.Data.Core.Configurations;
using PaceTime.Domain.Models;

namespace PaceTime.Data.Core
{
    public class KnowledgeContext : DbContext
    {
        public KnowledgeContext(DbContextOptions<KnowledgeContext> options)
            : base(options)
        {
            Database.EnsureCreated();
            Database.Migrate();
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BookEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}

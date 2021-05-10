using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace books_api.Data.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book_Author>()
                .HasOne(b => b.Book)
                .WithMany(b1 => b1.Book_Authors)
                .HasForeignKey(fk => fk.BookId);

            modelBuilder.Entity<Book_Author>()
                .HasOne(b => b.Author)
                .WithMany(b1 => b1.Books_Authors)
                .HasForeignKey(fk => fk.AuthorId);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book_Author> Book_Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
    }
}

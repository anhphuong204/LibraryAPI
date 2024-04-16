using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Data
{
	public class LibraryDbContext : DbContext
	{
		public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
		{
		}

		public DbSet<Authors> Authors { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<Publishers> Publishers { get; set; }
		public DbSet<Book_Author> Book_Authors { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Book_Author>()
				.HasKey(ba => new { ba.BookId, ba.AuthorId });

			modelBuilder.Entity<Book_Author>()
				.HasOne(ba => ba.Book)
				.WithMany(b => b.Book_Authors)
				.HasForeignKey(ba => ba.BookId);

			modelBuilder.Entity<Book_Author>()
				.HasOne(ba => ba.Author)
				.WithMany(a => a.Book_Authors)
				.HasForeignKey(ba => ba.AuthorId)
				.HasPrincipalKey(a => a.Id); 

		}
	}
}

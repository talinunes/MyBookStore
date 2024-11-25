using Microsoft.EntityFrameworkCore;
using MyBookStore.Models;

namespace MyBookStore.Data
{
	public class MyBookStoreContext : DbContext
	{
		public MyBookStoreContext(DbContextOptions<MyBookStoreContext> options) : base(options)
		{
		}

		public DbSet<Genre> Genre { get; set; }
		public DbSet<Book> Book { get; set; }
	}
}

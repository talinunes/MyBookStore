using Microsoft.EntityFrameworkCore;
using MyBookStore.Data;
using MyBookStore.Models;

namespace MyBookStore.Services
{
	public class GenreService
	{
		private readonly MyBookStoreContext _context;
		public GenreService(MyBookStoreContext context)
		{
			_context = context;
		}

		public List<Genre> FindAll()
		{
			return _context.Genre.ToList();
		}
	}
}

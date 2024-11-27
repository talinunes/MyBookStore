using Microsoft.EntityFrameworkCore;
using MyBookStore.Data;
using MyBookStore.Models;
using MyBookStore.Services.Exceptions;

namespace MyBookStore.Services
{
    public class BookService
    {
        private readonly MyBookStoreContext _context;

        public BookService(MyBookStoreContext context)
        {
            _context = context;
        }

        public async Task<List<Book>>FindAllAsync()
        {
            return await _context.Book.Include(x => x.Genres).ToListAsync();
        }

        public async Task<Book> FindByIdAsync(int id)
        {
            return await _context.Book.Include( x=> x.Genres).FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task UpdateAsync(Book book)
        {
            bool hasAny = await _context.Book.AnyAsync(x => x.Id == book.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id não encontrado");
            }

            try
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcorrencyException(ex.Message);
            }
        }


        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Book.FindAsync(id);
                _context.Book.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }

        internal async Task InsertAsync(Book book)
        {
            throw new NotImplementedException();
        }
    }

}

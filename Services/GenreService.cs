using Microsoft.EntityFrameworkCore;
using MyBookStore.Data;
using MyBookStore.Models;
using MyBookStore.Services.Exceptions;
using System.Data;

namespace MyBookStore.Services
{
    public class GenreService
    {
        private readonly MyBookStoreContext _context;
        public GenreService(MyBookStoreContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> FindAllAsync()
        {
            return await _context.Genre.ToListAsync();
        }

        public async Task InsertAsync(Genre genre)
        {
            _context.Add(genre);
            await _context.SaveChangesAsync();
        }

        public async Task<Genre> FindByIdAsync(int id)
        {
            return await _context
                .Genre
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Genre> FindByIdEagerAsync(int id)
        {
            return await _context
                .Genre
                .Include(x => x.Books)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                Genre obj = await _context.Genre.FindAsync(id);
                _context.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }


        public async Task UpdateAsync(Genre genre)
        {
            bool hasAny = await _context.Genre.AnyAsync(x => x.Id == genre.Id);

            if (!hasAny)
            {
                throw new NotFoundException("Id não encontrado");
            }

            try
            {
                _context.Update(genre);
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }
    }
}
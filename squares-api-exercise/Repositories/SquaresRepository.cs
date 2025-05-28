using Microsoft.EntityFrameworkCore;
using squares_api_excercise.Data;
using squares_api_excercise.Interfaces;
using squares_api_excercise.Models;

namespace squares_api_excercise.Repositories
{
    public class SquaresRepository : ISquaresRepository
    {
        private readonly SquaresDbContext _context;
        public SquaresRepository(SquaresDbContext context)
        {
            _context = context;
        }

        public async Task<List<Square>> GetSquaresAsync()
        {
            return await _context.Squares
                .Include(s => s.P1)
                .Include(s => s.P2)
                .Include(s => s.P3)
                .Include(s => s.P4)
                .ToListAsync();
        }

        public async Task AddSquare(Square square)
        {
            Console.WriteLine("in square repo");
            _context.Squares.Add(square);
            await _context.SaveChangesAsync();
        }
    }
}

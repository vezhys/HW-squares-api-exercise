using Microsoft.EntityFrameworkCore;
using squares_api_excercise.Data;
using squares_api_excercise.Models;
using squares_api_excercise.Repositories.Interfaces;

namespace squares_api_excercise.Repositories
{
    public class PointsRepository : IPointsRepository
    {
        private readonly SquaresDbContext _context;
        public PointsRepository(SquaresDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Point>> GetAllAsync()
        {
            return await _context.Points.ToListAsync();
        }

        public async Task<Point?> GetByIdAsync(int id)
        {
            var point = await _context.Points.FindAsync(id);
            return point;
        }

        public async Task<Point> AddPoint(Point point)
        {

            await _context.Points.AddAsync(point);
            await _context.SaveChangesAsync();
            return point;
        }

        public async Task DeletePoint(int id)
        {
            var point = await _context.Points.FindAsync(id);
            _context.Points.Remove(point);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Point?>> AddMany(IEnumerable<Point> list)
        {
            await _context.Points.AddRangeAsync(list);
            await _context.SaveChangesAsync();
            return list;

        }

        public async Task<bool> PointExists(Point point)
        {
            return await _context.Points.AnyAsync(e => e.X == point.X && e.Y == point.Y);
        }
    }
}

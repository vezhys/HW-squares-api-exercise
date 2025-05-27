using Microsoft.EntityFrameworkCore;
using squares_api_excercise.Data;
using squares_api_excercise.Models;

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

            if (point == null)
            {
                return null;
            }

            return point;
        }

        public async Task AddPoint(Point point)
        {

            await _context.Points.AddAsync(point);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePoint(int id)
        {
            var point = await _context.Points.FindAsync(id);
            if (point != null)
            {
                _context.Points.Remove(point);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddMany(IEnumerable<Point> list)
        {
            if (list != null || list.Any())
            {
                await _context.Points.AddRangeAsync(list);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> PointExists(Point point)
        {
            if (point == null)
                return false;
            return await _context.Points.AnyAsync(e => e.X == point.X && e.Y == point.Y);
        }
    }
}

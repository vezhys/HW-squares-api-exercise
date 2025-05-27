using Microsoft.EntityFrameworkCore;
using squares_api_excercise.Models;

namespace squares_api_excercise.Repositories
{
    public interface IPointsRepository
    {
        Task<IEnumerable<Point>> GetAllAsync();

        Task<Point?> GetByIdAsync(int id);

        Task AddPoint(Point point);

        Task DeletePoint(int id);

        Task AddMany(IEnumerable<Point> list);

        Task <bool> PointExists(Point point);
       }
}

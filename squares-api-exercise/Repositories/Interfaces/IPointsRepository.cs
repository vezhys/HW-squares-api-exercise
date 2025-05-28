using Microsoft.EntityFrameworkCore;
using squares_api_excercise.Models;

namespace squares_api_excercise.Repositories.Interfaces
{
    public interface IPointsRepository
    {
        Task<IEnumerable<Point>> GetAllAsync();

        Task<Point?> GetByIdAsync(int id);

        Task<Point> AddPoint(Point point);

        Task DeletePoint(int id);

        Task<IEnumerable<Point?>> AddMany(IEnumerable<Point> list);

        Task <bool> PointExists(Point point);
       }
}

using squares_api_excercise.DTOs;
using squares_api_excercise.Models;
using squares_api_excercise.Repositories;

namespace squares_api_excercise.Services
{
    public class PointsService : IPointsService
    {
        public IPointsRepository _repository;
        public PointsService(IPointsRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<PointDTO>> GetPointsAsync()
        {
            var entities = await _repository.GetAllAsync();
            var dtos = entities.Select(e => new PointDTO
            {
                X = e.X,
                Y = e.Y
            }).ToList();
            return dtos;
        }

        public async Task<PointDTO?> GetByIdAsync(int id)
        {
            var point = await _repository.GetByIdAsync(id);

            if (point == null)
            {
                return null;
            }

            return new PointDTO { X = point.X, Y = point.Y };
        }

        public async Task<(bool success, string message)> AddPoint(PointDTO point)
        {

            if (point != null)
            {
                var entity = new Point { X = point.X, Y = point.Y };
                if (!await _repository.PointExists(entity))
                {
                    await _repository.AddPoint(entity);
                    return (true, "Point added successfully");
                }
                else
                {
                    return (false, "Such Point Already in database");
                }
            }
            else
            {
                return (false, "Point not provided");
            }
        }
        public async Task<(bool success, string message)> DeletePoint(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
            {
                    await _repository.DeletePoint(id);
                    return (true, "Deleted point successfully");
            }
            else
            {
                return (false, "no such point found in database");
            }
        }

        public async Task<int> PostManyPoints(ImportPointsRequest request)
        {
            if (request?.Points == null || request.Points.Count == 0)
            {
                return -1;
            }
            var points = request.Points.ToList();
            var pointsWithoutDuplicates = points.Distinct().ToList();
            var pointsToInsert = new List<Point>();

            foreach (var point in pointsWithoutDuplicates)
            {
                Point current = new Point(point.X, point.Y);
                bool exists = await _repository.PointExists(current);
                if (!exists)
                {
                    pointsToInsert.Add(current);
                }
            }
            if (points.Count > 0)
            {
                await _repository.AddMany(pointsToInsert);
            }
            return pointsToInsert.Count;
        }


    }
}

using NuGet.Protocol.Core.Types;
using squares_api_excercise.DTOs;
using squares_api_excercise.Models;

namespace squares_api_excercise.Interfaces
{
    public interface IPointsService
    {
        Task<IEnumerable<PointDTO>> GetPointsAsync();

        Task<PointDTO?> GetByIdAsync(int id);

        Task<Point?> AddPoint(PointDTO point);
        Task<bool> DeletePoint(int id);
        Task<int> PostManyPoints(ImportPointsRequest request);

    }
}

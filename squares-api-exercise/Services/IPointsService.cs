using NuGet.Protocol.Core.Types;
using squares_api_excercise.DTOs;

namespace squares_api_excercise.Services
{
    public interface IPointsService
    {
        Task<IEnumerable<PointDTO>> GetPointsAsync();

        Task<PointDTO?> GetByIdAsync(int id);

        Task<(bool success, string message)> AddPoint(PointDTO point);
        Task<(bool success, string message)> DeletePoint(int id);
        Task<int> PostManyPoints(ImportPointsRequest request);

    }
}

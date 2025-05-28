using NuGet.Protocol.Core.Types;
using squares_api_excercise.DTOs;
using squares_api_excercise.Models;
using System.Linq;

namespace squares_api_excercise.Interfaces
{
    public interface ISquaresService
    {
        Task<List<SquareDTO>> GetSquaresAsync();

        Task<int> FindSquares();
        Task<bool> SquareExistsAsync(int[] pointIds);

    }
}

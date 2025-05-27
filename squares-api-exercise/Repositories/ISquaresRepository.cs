using Microsoft.EntityFrameworkCore;
using squares_api_excercise.Models;

namespace squares_api_excercise.Repositories
{
    public interface ISquaresRepository
    {
        Task<List<Square>> GetSquaresAsync();
        Task AddSquare(Square square);

    }
}

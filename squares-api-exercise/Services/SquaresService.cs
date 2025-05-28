using squares_api_excercise.DTOs;
using squares_api_excercise.Models;
using squares_api_excercise.Repositories.Interfaces;
using squares_api_excercise.Services.Interfaces;

namespace squares_api_excercise.Services
{
    public class SquaresService : ISquaresService
    {
        public ISquaresRepository _repository;
        public IPointsRepository _pointsRepository;

        public SquaresService(ISquaresRepository repository, IPointsRepository pointsRepository)
        {
            _repository = repository;
            _pointsRepository = pointsRepository;
        }

        public async Task<List<SquareDTO>> GetSquaresAsync()
        {
            var entities = await _repository.GetSquaresAsync();
            var dtos = entities.Select(e => new SquareDTO
            {
                id = e.Id,
                P1 = new PointDTO { X = e.P1.X, Y = e.P1.Y },
                P2 = new PointDTO { X = e.P2.X, Y = e.P2.Y },
                P3 = new PointDTO { X = e.P3.X, Y = e.P3.Y },
                P4 = new PointDTO { X = e.P4.X, Y = e.P4.Y },
            }).ToList();
            return dtos;
        }

        public async Task<int> FindSquares()
        {
            List<Point> points = (await _pointsRepository.GetAllAsync()).ToList();

            var pointSet = new HashSet<(int x, int y)>(points.Select(p => (p.X, p.Y)));
            var seenSquares = new HashSet<string>();
            var result = new List<Square>();

            //check all pairs of points
            for (int i = 0; i < points.Count; i++)
            {
                for (int j = i + 1; j < points.Count; j++)
                {
                    if (!points[i].Equals(points[j]))
                    {
                        var p1 = points[i];
                        var p2 = points[j];

                        //calculate distance (side of square)
                        int dx = p2.X - p1.X;
                        int dy = p2.Y - p1.Y;

                        //calculate the needed points to form a square
                        var p3 = (x: p1.X - dy, y: p1.Y + dx);
                        var p4 = (x: p2.X - dy, y: p2.Y + dx);

                        //check if such points exist
                        if (pointSet.Contains(p3) && pointSet.Contains(p4))
                        {
                            var matchP3 = points.FirstOrDefault(p => p.X == p3.x && p.Y == p3.y);
                            var matchP4 = points.FirstOrDefault(p => p.X == p4.x && p.Y == p4.y);

                            if (matchP3 != null && matchP4 != null)
                            {
                                var squarePoints = new[] { p1, p2, matchP3, matchP4 };
                                var squareIds = new[] { p1.Id, p2.Id, matchP3.Id, matchP4.Id };

                                //sort points coordinates and construct a string for comparing
                                var canonical = string.Join(";", squarePoints
                                    .OrderBy(p => p.X)
                                    .ThenBy(p => p.Y)
                                    .Select(p => $"{p.X},{p.Y}"));

                                //check if its not been found before
                                if (!seenSquares.Contains(canonical))
                                {
                                    seenSquares.Add(canonical);

                                    //check if its not already in db
                                    if (!await SquareExistsAsync(squareIds))
                                    {
                                        var square = new Square([p1, p2, matchP3, matchP4]);
                                        await _repository.AddSquare(square);
                                        result.Add(square);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return (result.Count);
        }

        public async Task<bool> SquareExistsAsync(int[] pointIds)
        {
            var targetSet = new HashSet<int>(pointIds);
            var squares = await _repository.GetSquaresAsync();

            foreach (var square in squares)
            {
                var squareSet = new HashSet<int> { square.P1Id, square.P2Id, square.P3Id, square.P4Id };
                if (squareSet.SetEquals(targetSet))
                {
                    return true;
                }
            }

            return false;
        }
    }
}

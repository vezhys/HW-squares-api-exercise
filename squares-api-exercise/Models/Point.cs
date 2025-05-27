namespace squares_api_excercise.Models
{
    public class Point
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Point()
        {
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            return obj is Point p && X == p.X && Y == p.Y;
        }

        public override int GetHashCode() => HashCode.Combine(X, Y);
    }
}

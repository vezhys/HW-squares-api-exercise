namespace squares_api_excercise.Models
{
    public class Square
    {
        public int Id { get; set; }

        public int P1Id { get; set; }
        public int P2Id { get; set; }
        public int P3Id { get; set; }
        public int P4Id { get; set; }

        public Point P1 { get; set; }
        public Point P2 { get; set; }
        public Point P3 { get; set; }
        public Point P4 { get; set; }

        public Square(Point[] points)
        {
            P1Id = points[0].Id;
            P2Id = points[1].Id;
            P3Id = points[2].Id;
            P4Id = points[3].Id;

            P1 = points[0];
            P2 = points[1];
            P3 = points[2];
            P4 = points[3];
        }

        public Square()
        {
        }
    }
}

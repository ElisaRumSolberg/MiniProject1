namespace MiniProject1.Models
{
    public class Laureate
    {
        public string? Id { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Motivation { get; set; }
        public string? Share { get; set; }
    }

    public class Prize
    {
        public string? Year { get; set; }
        public string? Category { get; set; }
        public List<Laureate>? Laureates { get; set; }
    }

    public class NobelPrizeData
    {
        public List<Prize>? Prizes { get; set; }
    }
}

using System.Collections.Generic;

namespace NobelPrizeProject.Models
{
    // Represents an individual laureate (Nobel Prize winner)
    public class Laureate
    {
        public string? Id { get; set; }         // The ID of the laureate
        public string? Firstname { get; set; } // The first name of the laureate
        public string? Surname { get; set; }   // The last name of the laureate
        public string? Motivation { get; set; } // The reason for winning the prize
        public string? Share { get; set; }     // The share of the prize the laureate received
    }

    // Represents a Nobel Prize category for a specific year
    public class Prize
    {
        public string? Year { get; set; }       // The year the prize was awarded
        public string? Category { get; set; }  // The category of the prize (e.g., chemistry, economics)
        public List<Laureate>? Laureates { get; set; } // A list of laureates who won in this category
    }

    // Represents the root object containing all prizes from the JSON data
    public class PrizeData
    {
        public List<Prize>? Prizes { get; set; } // A list of prizes across all years and categories
    }
}

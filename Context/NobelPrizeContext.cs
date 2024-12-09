using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NobelPrizeProject.Models;

namespace NobelPrizeProject.Context
{
    public class NobelPrizeContext
    {
        public List<Prize>? Prizes { get; set; } // Nullable to handle potential null values

        public NobelPrizeContext()
        {
            // Path to the JSON file containing Nobel Prize data
            string filePath = "wwwroot/json/nobelPrize.json";

            // Check if the file exists to avoid null reference errors
            if (File.Exists(filePath))
            {
                // Read the JSON file as a string
                string jsonData = File.ReadAllText(filePath);

                // Deserialize the JSON string into PrizeData
                PrizeData? prizeData = JsonConvert.DeserializeObject<PrizeData>(jsonData);

                // Assign deserialized data to the Prizes property
                Prizes = prizeData?.Prizes ?? new List<Prize>();
            }
            else
            {
                // If the file doesn't exist, initialize an empty list
                Prizes = new List<Prize>();
            }
        }
    }
}

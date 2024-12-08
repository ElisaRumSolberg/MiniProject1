using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using MiniProject1.Models;

namespace MiniProject1.Context
{
    public class NobelPrizeContext
    {
        public NobelPrizeData? NobelPrizes { get; set; } // Nullable yapıldı

        public NobelPrizeContext()
        {
            var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "nobelPrize.json");

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine("JSON dosyası bulunamadı.");
                return;
            }

            var jsonData = File.ReadAllText(jsonFilePath);
            NobelPrizes = JsonSerializer.Deserialize<NobelPrizeData>(jsonData);
        }
    }
}

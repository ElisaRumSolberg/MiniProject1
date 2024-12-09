using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using NobelPrizeProject.Context;
using NobelPrizeProject.Models;

class Program
{
    static void Main(string[] args)
    {
        // 1️⃣ NobelPrizeContext sınıfı kullanılarak JSON verilerini yükle
        var context = new NobelPrizeContext();

        // 2️⃣ JSON verisinin yüklendiğinden emin ol
        if (context.Prizes == null || !context.Prizes.Any())
        {
            Console.WriteLine("Failed to load JSON data or no prizes found.");
            return;
        }

        // 3️⃣ Select() - Yıl ve Kategorileri Listele
        Console.WriteLine("\n1️⃣ Yıl ve Kategoriler:");
        var yearAndCategory = context.Prizes
            .Select(prize => new { Year = prize.Year ?? "Unknown", Category = prize.Category ?? "Unknown" })
            .ToList();

        foreach (var item in yearAndCategory)
        {
            Console.WriteLine($"Year: {item.Year}, Category: {item.Category}");
        }

        // 4️⃣ SelectMany() - Tüm Kazananları Listele
        Console.WriteLine("\n2️⃣ Tüm Kazananlar:");
        var laureates = context.Prizes
            .Where(prize => prize.Laureates != null)
            .SelectMany(prize => prize.Laureates!)
            .Select(laureate => new 
            { 
                Firstname = laureate.Firstname ?? "Unknown", 
                Surname = laureate.Surname ?? "Unknown", 
                Motivation = laureate.Motivation ?? "No Motivation Provided" 
            })
            .ToList();

        foreach (var laureate in laureates)
        {
            Console.WriteLine($"{laureate.Firstname} {laureate.Surname}: {laureate.Motivation}");
        }

        // 5️⃣ GroupBy() - Ödülleri Kategorilere Göre Grupla ve Say
        Console.WriteLine("\n3️⃣ Kategorilere Göre Ödül Sayısı:");
        var prizesByCategory = context.Prizes
            .GroupBy(prize => prize.Category ?? "Unknown")
            .Select(group => new 
            { 
                Category = group.Key, 
                PrizeCount = group.Count() 
            })
            .ToList();

        foreach (var group in prizesByCategory)
        {
            Console.WriteLine($"Category: {group.Category}, Prize Count: {group.PrizeCount}");
        }

        // 6️⃣ BONUS: Kullanıcıdan Yıl veya Kategori Girdisi Al
        Console.WriteLine("\n4️⃣ BONUS: Lütfen bir yıl veya kategori girin (örnek: '2024' veya 'chemistry'):");
        string? userInput = Console.ReadLine()?.Trim();

        if (!string.IsNullOrEmpty(userInput))
        {
            // Kullanıcıdan alınan girdiyle ödülleri bul
            var searchResults = context.Prizes
                .Where(prize => 
                    (prize.Year != null && prize.Year.Equals(userInput, StringComparison.OrdinalIgnoreCase)) ||
                    (prize.Category != null && prize.Category.Equals(userInput, StringComparison.OrdinalIgnoreCase))
                )
                .Select(prize => new 
                { 
                    Year = prize.Year, 
                    Category = prize.Category, 
                    Laureates = prize.Laureates?.Select(l => $"{l.Firstname} {l.Surname}").ToList() 
                })
                .ToList();

            if (!searchResults.Any())
            {
                Console.WriteLine($"\n'{userInput}' için herhangi bir ödül bulunamadı.");
            }
            else
            {
                Console.WriteLine($"\n'{userInput}' için bulunan ödüller:");
                foreach (var prize in searchResults)
                {
                    Console.WriteLine($"Year: {prize.Year}, Category: {prize.Category}");
                    if (prize.Laureates != null)
                    {
                        Console.WriteLine("Laureates:");
                        foreach (var laureate in prize.Laureates)
                        {
                            Console.WriteLine($"- {laureate}");
                        }
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("No input provided. Exiting...");
        }
    }
}

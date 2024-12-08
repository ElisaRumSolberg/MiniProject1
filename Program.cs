using System;
using System.Linq;
using MiniProject1.Context;
using MiniProject1.Models; // Bu satırı ekledik

class Program
{
    static void Main(string[] args)
    {
        // JSON bağlamını oluştur ve verileri yükle
        var context = new NobelPrizeContext();

        if (context.NobelPrizes == null || context.NobelPrizes.Prizes == null)
        {
            Console.WriteLine("JSON verisi yüklenemedi.");
            return;
        }

        // 1️⃣ Select() - Ödül Kategorilerini Listele
        var categories = context.NobelPrizes.Prizes
            .Select(p => new { p.Year, p.Category })
            .ToList();

        Console.WriteLine("\nKategoriler:");
        foreach (var category in categories)
        {
            Console.WriteLine($"{category.Year} - {category.Category}");
        }

        // 2️⃣ SelectMany() - Tüm Kazananları Listele
        var laureates = context.NobelPrizes.Prizes
            .SelectMany(p => p.Laureates ?? new List<Laureate>()) // Bu satırda değişiklik yaptık
            .Select(l => new { l.Firstname, l.Surname })
            .ToList();

        Console.WriteLine("\nKazananlar:");
        foreach (var laureate in laureates)
        {
            if (laureate != null) // Null kontrolü
            {
                Console.WriteLine($"{laureate.Firstname} {laureate.Surname}");
            }
        }

        // 3️⃣ GroupBy() - Ödülleri Yıllara Göre Grupla
        var prizesByYear = context.NobelPrizes.Prizes
            .GroupBy(p => p.Year ?? "Bilinmeyen Yıl") // Null kontrolü
            .Select(g => new { Year = g.Key, Count = g.Count() })
            .ToList();

        Console.WriteLine("\nYıllara Göre Ödül Sayısı:");
        foreach (var prize in prizesByYear)
        {
            Console.WriteLine($"{prize.Year}: {prize.Count} ödül");
        }

        // 🟢 BONUS: Kullanıcıdan Kategori veya Yıl Al ve Arama Yap 🟢
        Console.WriteLine("\nBONUS: Lütfen bir kategori (örneğin: 'chemistry') veya yıl (örneğin: '2024') girin:");
        string? userInput = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(userInput))
        {
            Console.WriteLine("Geçersiz giriş. Program sonlandırılıyor.");
            return;
        }

        // Kullanıcıdan alınan girdiye göre ödülleri bul
        var searchResults = context.NobelPrizes.Prizes
            .Where(p => p.Category != null && p.Category.Equals(userInput, StringComparison.OrdinalIgnoreCase) 
                     || p.Year != null && p.Year.Equals(userInput))
            .Select(p => new 
            { 
                Year = p.Year, 
                Category = p.Category, 
                Laureates = p.Laureates?.Select(l => $"{l.Firstname} {l.Surname}").ToList() 
            })
            .ToList();

        if (searchResults.Count == 0)
        {
            Console.WriteLine($"\n'{userInput}' için herhangi bir ödül bulunamadı.");
        }
        else
        {
            Console.WriteLine($"\n'{userInput}' için bulunan ödüller:");
            foreach (var prize in searchResults)
            {
                if (prize != null) // Null kontrolü
                {
                    Console.WriteLine($"\nYıl: {prize.Year}, Kategori: {prize.Category}");
                    Console.WriteLine("Kazananlar:");
                    if (prize.Laureates != null)
                    {
                        foreach (var laureate in prize.Laureates)
                        {
                            Console.WriteLine($"- {laureate}");
                        }
                    }
                }
            }
        }
    }
}

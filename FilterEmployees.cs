using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

class EmployeeAnalysis
{
    public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        //Filtreliyoruz yaş 25-40, departman IT veya finans, maaş 5k-9k, işe giriş 2017den sonra
        var secilenKisiler = employees
            .Where(calisan => calisan.Age >= 25 && calisan.Age <= 40)
            .Where(calisan => calisan.Department == "IT" || calisan.Department == "Finance")
            .Where(calisan => calisan.Salary >= 5000 && calisan.Salary <= 9000)
            .Where(calisan => calisan.HireDate > new DateTime(2017, 1, 1))
            .ToList();

        //isimleri uzunluklarına göre azalan olarak sonra alfabetik sırala
        var isimler = secilenKisiler
            .Select(calisan => calisan.Name)
            .OrderByDescending(ad => ad.Length)
            .ThenBy(ad => ad)
            .ToList();

        // Maaş hesaplamaları
        decimal toplamMaas = secilenKisiler.Sum(calisan => calisan.Salary);
        decimal ortalamaMaas = secilenKisiler.Any() ? Math.Round(secilenKisiler.Average(calisan => calisan.Salary), 2) : 0;
        decimal minMaas = secilenKisiler.Any() ? secilenKisiler.Min(calisan => calisan.Salary) : 0;
        decimal maxMaas = secilenKisiler.Any() ? secilenKisiler.Max(calisan => calisan.Salary) : 0;
        int toplamKisi = secilenKisiler.Count;

        // Sonucu dictionary içine koy
        var sonuc = new Dictionary<string, object>
        {
            {"Names", isimler},
            {"TotalSalary", toplamMaas},
            {"AverageSalary", ortalamaMaas},
            {"MinSalary", minMaas},
            {"MaxSalary", maxMaas},
            {"Count", toplamKisi}
        };

        // JSON döndür
        return JsonSerializer.Serialize(sonuc);
    }

    //konsolda test etmek için main fonk içinde bir girdi değerini yerleştirdim
    static void Main()
    {
        var calisanlar = new List<(string, int, string, decimal, DateTime)>
        {
            ("Ali", 30, "IT", 6000m, new DateTime(2018, 5, 1)),
            ("Ayşe", 35, "Finance", 8500m, new DateTime(2019, 3, 15)),
            ("Veli", 28, "IT", 7000m, new DateTime(2020, 1, 1))
        };

        Console.WriteLine(FilterEmployees(calisanlar));
    }
}

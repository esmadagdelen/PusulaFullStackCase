using System;
using System.Collections.Generic;
using System.Text.Json;

class SesliHarfBul
{
    // Bu metod, kelime listesinde her kelimenin en uzun ardışık sesli harf alt dizisini bulur ve JSON olarak döndürür
    public static string LongestVowelSubsequenceAsJson(List<string> words)
    {
        // Sesli harfleri tanımlıyoruz
        HashSet<char> sesliler = new HashSet<char> { 'a','e','i','o','u','A','E','I','O','U' };

        // Sonuçları tutacak liste
        var sonucListesi = new List<Dictionary<string, object>>();

        foreach (var kelime in words)
        {
            string enUzunAltDizi = "";      // Her kelime için en uzun alt dizi
            string anlikAltDizi = "";       // Geçici alt dizi

            foreach (var harf in kelime)
            {
                if (sesliler.Contains(harf))
                {
                    anlikAltDizi += harf; // Sesli harfse geçici diziyi büyüt
                    if (anlikAltDizi.Length > enUzunAltDizi.Length)
                        enUzunAltDizi = anlikAltDizi; // Daha uzun alt dizi bulunduysa güncelle
                }
                else
                {
                    anlikAltDizi = ""; // Sessiz harf gelirse geçici alt diziyi sıfırla
                }
            }

            // Her kelime için dictionary ekle
            sonucListesi.Add(new Dictionary<string, object>
            {
                {"kelime", kelime},
                {"alt_dizi", enUzunAltDizi},
                {"uzunluk", enUzunAltDizi.Length}
            });
        }

        // Sonuç listesini JSON string olarak döndür
        return JsonSerializer.Serialize(sonucListesi);
    }

    // Test için Main
    static void Main()
    {
var test1 = new List<string> { "aeiou", "bcd", "aaa" };
var test2 = new List<string> { "miscellaneous", "queue", "sky", "cooperative" };
var test3 = new List<string> { "sequential", "beautifully", "rhythms", "encyclopaedia" };
var test4 = new List<string> { "algorithm", "education", "idea", "strength" };
var test5 = new List<string>(); // boş liste

Console.WriteLine(LongestVowelSubsequenceAsJson(test1));
Console.WriteLine(LongestVowelSubsequenceAsJson(test2));
Console.WriteLine(LongestVowelSubsequenceAsJson(test3));
Console.WriteLine(LongestVowelSubsequenceAsJson(test4));
Console.WriteLine(LongestVowelSubsequenceAsJson(test5));
    }
}

using System;
using System.Collections.Generic;
using System.Text.Json;

class MaxIncreasingSubArray
{
    // Bu metod, en büyük artan alt diziyi bulur ve JSON olarak döndürür
    public static string MaxIncreasingSubArrayAsJson(List<int> sayilar)
    {
        if (sayilar == null || sayilar.Count == 0)
            return "[]";

        List<int> eniyi = new List<int>();  //başta diziyi boş olarak kuruyoruz.
        List<int> anlik_inceleme = new List<int> { sayilar[0] }; 

        for (int i = 1; i < sayilar.Count; i++) //döngü 1den başlar çünkü sayilar[0] anlik_inceleme listesinde zatn
        {
            if (sayilar[i] > sayilar[i - 1])
            {
                anlik_inceleme.Add(sayilar[i]); //arada artış varsa alt diziyi genişlet
            }
            else
            {
                if (Sum(anlik_inceleme) > Sum(eniyi))
                    eniyi = new List<int>(anlik_inceleme); // En iyi toplamı güncelle

                anlik_inceleme = new List<int> { sayilar[i] }; // Yeni alt dizi başlat
            }
        }

        // Son alt diziyi kontrol et
        if (Sum(anlik_inceleme) > Sum(eniyi))
            eniyi = new List<int>(anlik_inceleme);

        return JsonSerializer.Serialize(eniyi);
    }

    // Yardımcı metod listedeki sayıların toplamını hesaplar
    private static int Sum(List<int> list)
    {
        int total = 0;
        foreach (var n in list)
            total += n;
        return total;
    }


}

using System;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;
using System.Collections.Generic;

class PersonXmlFilter{
    public static string FilterPeopleFromXml(string xmlData){

        //önce Xml i parçalayacağız
        var doc= XDocument.Parse(xmlData);

        //bütün person ları al
        var person=doc.Descendants("Person");

        //Filtreleyeceğiz. age>30 , department=IT , saalry>5000, HireDate<2019
        var secilenKisiler = person
            .Where(p => int.Parse(p.Element("Age").Value) > 30)
            .Where(p => p.Element("Department").Value == "IT")
            .Where(p => int.Parse(p.Element("Salary").Value) > 5000)
            .Where(p => DateTime.Parse(p.Element("HireDate").Value).Year < 2019)
            .Select(p => new
            {
                Name = p.Element("Name").Value,
                Salary = int.Parse(p.Element("Salary").Value)
            })
            .ToList();


        //isimleri alfabetik sırala
        var isimler = secilenKisiler.Select(f => f.Name).OrderBy(n => n).ToList();

        //maaş istatistikleri
        int toplamMaas = secilenKisiler.Sum(f => f.Salary);
        int maxMaas = secilenKisiler.Any() ? secilenKisiler.Max(f => f.Salary) : 0;
        int ortalamaMaas = secilenKisiler.Any() ? toplamMaas / secilenKisiler.Count : 0;
        int toplamKisi = secilenKisiler.Count;

        //sonuç dictionary olarak hazırlanır
        var sonuc = new Dictionary<string, object>
        {
            { "Names", isimler },
            { "TotalSalary", toplamMaas },
            { "AverageSalary", ortalamaMaas },
            { "MaxSalary", maxMaas },
            { "Count", toplamKisi }
        };

        // JSON string olarak döndür
        return JsonSerializer.Serialize(sonuc);
    }
    //girdilerden 2 tanesini konsolda çıktısını görebilmek için test ettim
    static void Main()
    {
        string xml1 = "<People><Person><Name>Ali</Name><Age>35</Age><Department>IT</Department><Salary>6000</Salary><HireDate>2018-06-01</HireDate></Person><Person><Name>Ayşe</Name><Age>28</Age><Department>HR</Department><Salary>4500</Salary><HireDate>2020-04-15</HireDate></Person></People>";
        Console.WriteLine(FilterPeopleFromXml(xml1));

        string xml2 = "<People><Person><Name>Mehmet</Name><Age>40</Age><Department>IT</Department><Salary>7500</Salary><HireDate>2017-02-01</HireDate></Person></People>";
        Console.WriteLine(FilterPeopleFromXml(xml2));
    }
}

using Linq;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    // Zufallsgenerator
    static Random random = new Random();
    // Listen
    static List<string> firstNames = new List<string> { "John", "Jane", "Alex", "Emily", "Chris", "Katie", "Michael", "Sarah", "David", "Laura" };
    static List<string> lastNames = new List<string> { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Martinez", "Hernandez" };
    static List<string> cities = new List<string> { "New York", "Los Angeles", "Chicago", "München", "Phoenix", "Philadelphia", "San Antonio", "San Diego", "Dallas", "San Jose", "Wien", "Berlin" };
  
    // zufällige Generierung von Namen, Alter, Geschelcht
    static string GenerateRandomName(List<string> names)
    {
        int index = random.Next(names.Count);
        return names[index];
    }

    static int GenerateRandomAge()
    {
        return random.Next(18, 81);
    }

    static string GenerateRandomGender()
    {
        return random.Next(2) == 0 ? "Male" : "Female";
    }
    /// <summary>
    /// Erstellt eine Liste von 100 Personene
    /// </summary>
    static void Main()
    {
        List<Person> people = new List<Person>();

        for (int i = 0; i < 100; i++)
        {
            Person person = new Person
            {
                Name = GenerateRandomName(firstNames),
                Surname = GenerateRandomName(lastNames),
                Age = GenerateRandomAge(),
                City = GenerateRandomName(cities),
                Gender = GenerateRandomGender()
            };

            people.Add(person);
        }

        
        PrintHeader("Personen über 30");
        var Over30 = people.Where(p => p.Age > 30).ToList();
        PrintPeople(Over30);

        PrintHeader("Personen in Berlin");
        var EqualBerlin = people.Where(p => p.City == "Berlin").ToList();
        PrintPeople(EqualBerlin);
        
        PrintHeader("Weibliche Personen alphabetisch nach Nachname");
        var FemaleCharList = people.Where(p => p.Gender == "Female")
            .OrderBy(p => p.Surname)
            .ToList();
        PrintPeople(FemaleCharList);

        PrintHeader("Älteste Person");
        var OldestPerson = people.OrderByDescending(p => p.Age).First();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(OldestPerson);

        PrintHeader("Personen Nachnamen die in München oder Wien wohnen");
        var berlinOrWien = people.Where(p => p.City == "München" || p.City == "Wien").ToList();
        foreach (var person in berlinOrWien)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(person.Surname);
        }

        PrintHeader("Durchschnittliche Buchstabenanzahl der Namen in München oder Wien");
        if (berlinOrWien.Any())
        {
            double averageNameLength = berlinOrWien.Average(p => p.Name.Length);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Durchschnittliche Buchstabenanzahl der Namen: {averageNameLength}");
        }
        else
        {
            Console.WriteLine("Keine Personen in München oder Wien gefunden.");
        }

        PrintHeader("Anzahl der Personen pro Geschlecht");
        var maleCounter = people.Count(p => p.Gender == "Male");
        var femaleCounter = people.Count(p => p.Gender == "Female");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Männlich: "+ maleCounter + "Weiblich: "+ femaleCounter);




        PrintHeader("Personen, die zwischen 20-40 Jahren liegen, sortiert nach Stadt");
        var wantedCitys = (from person in people
                           where person.Age > 20 && person.Age < 40
                           orderby person.City
                           select person).ToList();
        PrintPeople(wantedCitys);

        PrintHeader("Nachnamen, die sowohl von mindestens einer Frau als auch einem Mann geteilt werden");
        var surnamesSharedByBothGenders = people
            .GroupBy(p => p.Surname)
            .Where(g => g.Any(p => p.Gender == "Male") && g.Any(p => p.Gender == "Female"))
            .Select(g => g.Key)
            .ToList();
        foreach (var surname in surnamesSharedByBothGenders)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(surname);
        }

        PrintHeader("Älteste Person pro Stadt");
        var oldestPersonPerCity = people
            .GroupBy(p => p.City)
            .Select(g => g.OrderByDescending(p => p.Age).First())
            .ToList();
        PrintPeople(oldestPersonPerCity);
    }

    /// <summary>
    ///  Konsolen Styling Header
    /// </summary>
    /// <param name="header"></param>
    static void PrintHeader(string header)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine();
        Console.WriteLine(header);
        Console.WriteLine(new string('-', 60)); // Gibt eine Linie mit 60 Bindestrichen aus, um den Header optisch abzugrenzen

        // Spaltenüberschriften für die tabellarische Darstellung der Personen
        // Jede Spalte wird auf eine feste Breite ausgerichtet
        Console.WriteLine($"{"City".PadRight(15)} {"Name".PadRight(15)} {"Surname".PadRight(15)} {"Age".PadRight(5)} {"Gender".PadRight(10)}");
        Console.WriteLine(new string('-', 60));
        Console.ResetColor();
    }

    /// <summary>
    /// Print People Styling
    /// </summary>
    /// <param name="people"></param>
    static void PrintPeople(List<Person> people)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        foreach (var person in people)
        {
            Console.WriteLine($"{person.City.PadRight(15)} {person.Name.PadRight(15)} {person.Surname.PadRight(15)} {person.Age.ToString().PadRight(5)} {person.Gender.PadRight(10)}");
        }
        Console.ResetColor();
    }
}

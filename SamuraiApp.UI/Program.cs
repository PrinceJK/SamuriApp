using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Linq;

namespace SamuraiApp.UI
{
    internal class Program
    {
        private static SamuraiContext _context = new SamuraiContext();
       
        static void Main(string[] args)
        {
            _context.Database.EnsureCreated();
            GetSamurais("Before Add: ");
            AddSamurai();
            GetSamurais("After Add: ");
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private static void AddSamurai()
        {
            var samuari = new Samurai
            {
                Name = "John"
            };
            _context.Samurais.Add(samuari);
            _context.SaveChanges();            
        }
        private static void GetSamurais(string text)
        {
            var samurais = _context.Samurais.ToList();
            Console.WriteLine($"{text}: Samurai count is {samurais.Count}");
            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }
    }
}

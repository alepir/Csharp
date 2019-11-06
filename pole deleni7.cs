using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Zadejte počet čísel: ");
            int n = int.Parse(Console.ReadLine());
            int[] pole = new int[n];
            Int16 poc = 0;
            for (int i = 0; i < n; i++)
            {
                Console.Write("Zadejte číslo ");
                int cislo = int.Parse(Console.ReadLine());
                if (cislo % 7 == 0) { pole[i] = cislo; poc++; }
            }
            Console.WriteLine("Počet čísel dělitelných 7 je: " + poc.ToString());
            for (int i = 0; i < n; i++) { if (pole[i] != 0) Console.WriteLine(pole[i]); }
            Console.ReadKey();
        }
    }
}

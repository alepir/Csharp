using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication11
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] pole = new int[25];
            for (int i = 1; i <= 20; i++)
            {
                Console.Write("Zadejte číslo : ");
                int cislo = int.Parse(Console.ReadLine());
                pole[i] = cislo;
            }
            for (int i = 0; i <= 20; i++) if (i % 2 != 0) { Console.Write(" " + pole[i]); }
            Console.WriteLine("");
            for (int i = 0; i <= 20; i++) if (i % 2 == 0) { Console.Write(" " + pole[i]); }
            Console.ReadKey();
        }
    }
}

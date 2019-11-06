using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVOJABS
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] polekl = new int[11];
            int[] polezp = new int[11];
            for (int i = 1; i <= 10; i++)
            {
                Console.Write("Zadejte " + i + ". číslo: ");
                int cislo = int.Parse(Console.ReadLine());
                if (cislo > 0) { cislo *= 2; polekl[i] = cislo; }
                else if (cislo < 0)
                {
                    cislo *= -1;
                    polezp[i] = cislo;
                }
            }
            for (int poc = 1; poc <= 10; poc++)
            {
                if (polekl[poc] != 0)
                {
                    Console.WriteLine("Dvojnasobky kladných číel: " + polekl[poc]);
                }
            }
            for (int poc = 1; poc <= 10; poc++)
            {
                if (polezp[poc] != 0)
                {
                    Console.WriteLine("Absolutni hodnoty zapornych cisel jsou: " + polezp[poc]);
                }
            }
            Console.ReadKey();
        }
    }
}

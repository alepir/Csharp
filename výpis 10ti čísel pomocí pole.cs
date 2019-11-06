using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pole
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] pole = new int[10];

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Zadejte číslo: ");
                int cislo = int.Parse(Console.ReadLine());
                pole[i] = cislo;
            }
            foreach (int i in pole)
                Console.Write("{0} ", i);
            Console.ReadKey();
        }
    }
}

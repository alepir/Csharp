using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    class Program
    {

        static void Main(string[] args)
        {
            int[,] matice = new int[3, 3];
            int[] vysledek = new int[3];
            int n = 0;
            int poc = 0;
            for (int i = 0; i <= 8; i++)
            {
                if (poc > 2)
                {
                    n += 1;
                    poc = 0;
                }
                Console.Write("Zadejte {0}. číslo, {1}řádku: ", i+1, n+1);
                int cislo = int.Parse(Console.ReadLine());
                matice[n, poc] = cislo;
                poc += 1;
            }
            Console.WriteLine("Stiskněte libovolnou klávesu pro spuštění součtu řádků...");
            Console.ReadLine();
            for (int i = 0; i <= 2; i++)
            {
                vysledek[i] = matice[i, 0] + matice[i, 1] + matice[i, 2];
                Console.WriteLine("Součet {0}. řádku je: {1}", i, vysledek[i]);
            }
            Console.ReadLine();
        }
    }
}
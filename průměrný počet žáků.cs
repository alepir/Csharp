using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.Write("Zadejte pocet zaku na skole: ");
            int pupils = int.Parse(Console.ReadLine());
            Console.Write("Zadejte pocet trid: ");
            int group = int.Parse(Console.ReadLine());
            float ave = pupils / group;
            Console.Write("Prumerny pocet zaku ve tride je: " + ave);
            Console.ReadLine();
        }
    }
}
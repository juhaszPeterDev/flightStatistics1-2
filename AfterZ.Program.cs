using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace AfterZ
{
    class Program
    {
        static void Main(string[] args)
        {
            // szöveg beolvasása
            string szoveg = File.ReadAllText("After-Z.txt");
            // Lista amiben összegyüjtöm a találatokat
            List<int> talalatok = new List<int>();
            // Reguláris kifejezés
            string pattern = @"[Z]\d";
            Regex rg = new Regex(pattern);
            MatchCollection matchedZafter = rg.Matches(szoveg);

            int darab = 0;
            int osszeg = 0;
            foreach (var item in matchedZafter)
            {
                darab++;
                osszeg +=(int)Char.GetNumericValue(item.ToString()[1]);
            }
            Console.WriteLine("Átlag:{0}/{1}={2}",osszeg,darab,osszeg/darab);
            Console.ReadLine();
        }
    }
}


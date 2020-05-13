using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace repulo
{

    class Program
    {

        public struct repulo
        {
            public int year;
            public int month; 
            public string carrier; //A légitársaság kódja
            public string carrier_name;  // A légitársaság neve
            public string airport;//A reptér kódja
            public string airport_name;// A reptér neve
            public double arr_flights; // Az érkező járatok száma
            public double arr_del15;// A késett járatok száma
            public double carrier_ct;
            public double weather_ct;
            public double nas_ct;
            public double security_ct;
            public double late_aircraft_ct;
            public double arr_cancelled;
            public double arr_diverted;
            public double arr_delay;// A késések összesítve percben
            public double carrier_delay;
            public double weather_delay;
            public double nas_delay;
            public double security_delay;
            public double late_aircraft_delay;
        }
        static int sor = 0;
        static List<repulo> repulok = new List<repulo>();
        static string vesszo(string s)
        {
            if (s.Contains("."))
                return s.Replace(".", ",");
            else return s;
        }
        static double ertek(string s)
        {
            if (s == "")
                return -1; //így jelzem, hogy nincs adat, később kiderül, mit kell vele kezdeni, ha egyáltalán kell
            else
                return Convert.ToDouble(vesszo(s));
        }
        static string levag(string s)
        {
            return s.Substring(1, s.Length - 2);
        }

        static void elso()
        {
            StreamReader sr = new StreamReader("flights.csv");
            string[] atmeneti;
            repulo r;
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                atmeneti = sr.ReadLine().Split(',');
                r = new repulo();
                r.year = Convert.ToInt32(atmeneti[0]);
                r.month = Convert.ToInt32(atmeneti[1]);
                r.carrier = levag(atmeneti[2]);
                r.carrier_name = levag(atmeneti[3]);
                r.airport = levag(atmeneti[4]);
                r.airport_name = levag(atmeneti[5] + ", " + atmeneti[6]);
                r.arr_flights = ertek(atmeneti[7]);
                r.arr_del15 = ertek(atmeneti[8]);
                r.carrier_ct = ertek(atmeneti[9]);
                r.weather_ct = ertek(atmeneti[10]);
                r.nas_ct = ertek(atmeneti[11]);
                r.security_ct = ertek(atmeneti[12]);
                r.late_aircraft_ct = ertek(atmeneti[13]);
                r.arr_cancelled = ertek(atmeneti[14]);
                r.arr_diverted = ertek(atmeneti[15]);
                r.arr_delay = ertek(atmeneti[16]);
                r.carrier_delay = ertek(atmeneti[17]);
                r.weather_delay = ertek(atmeneti[18]);
                r.nas_delay = ertek(atmeneti[19]);
                r.security_delay = ertek(atmeneti[20]);
                r.late_aircraft_delay = ertek(atmeneti[21]);
                repulok.Add(r);
                sor++;
            }
            sr.Close();
            //Ez csak teszt nagyjából:
            //for (int i = 0; i < sor; i++)
            //{
            //    Console.WriteLine("{0} {1,-6} {2,-30} {3,-10:0.00 }", i + 1, repulok[i].year, repulok[i].airport_name, repulok[i].late_aircraft_delay);
            //}
        }
        //A légitársaságok nevének listája
        static void legitarsasagokNeve()
        {
            List<string> carrier_name_list = new List<string>();
            int j;
            bool van;
            carrier_name_list.Add(repulok[0].carrier_name);
            for (int i = 1; i < sor; i++)
            {
                j = 0;
                van = false;
                while (j < carrier_name_list.Count && !van)
                {
                    if (repulok[i].carrier_name == carrier_name_list[j])
                    {
                        van = true;
                    }
                    j++;
                }
                if (!van)
                {
                    carrier_name_list.Add(repulok[i].carrier_name);
                }
            }

            carrier_name_list.Sort();

            StreamWriter sw = new StreamWriter("carrier_name.txt");
            for (int i = 0; i < carrier_name_list.Count; i++)
            {
                sw.WriteLine(carrier_name_list[i]);
            }
            sw.Close();
        }
        //Az egyes légitársaságok összes járatának összeszámolása
        //Az egyes légitársaságok által meglátogatott repterek összeszámolása
        //Az egyes légitársaságok késéseinek átlagos idejének kiszámítása
        //A 3 legforgalmasabb reptér kikeresése
        //A 3 legkisebb átlagos késésű társaság kiszámítása


        static void Main(string[] args)
        {
            elso();
            legitarsasagokNeve();
            Console.ReadLine();

        }
    }
}

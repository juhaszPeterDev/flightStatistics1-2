using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using Microsoft.VisualBasic.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;

namespace repulo
{
    class ValueComparerDesc : IComparer<KeyValuePair<string, double>>
    {
        public int Compare(KeyValuePair<string, double> x, KeyValuePair<string, double> y)
        {
            if (x.Value == y.Value) return 0;
            if (x.Value > y.Value) return -1;
            else return 1;
        }
    }

    class ValueComparerAsc : IComparer<KeyValuePair<string, double>>
    {
        public int Compare(KeyValuePair<string, double> x, KeyValuePair<string, double> y)
        {
            if (x.Value == y.Value) return 0;
            if (x.Value < y.Value) return -1;
            else return 1;
        }
    }
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
        static List<repulo> repulok = new List<repulo>(); //a fájl tartalma
        static List<string> carrier_name_list = new List<string>(); //a légitársaságok listája
        static List<string> airport_name_list = new List<string>(); //a repterek listája
        static Dictionary<string, double> carrierDelay = new Dictionary<string, double>(); //társaságonként a késések összege

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

        static void beolvasas()
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
        static void carrierName()
        {
            Console.WriteLine("A légitársaságok nevei...");
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
            sw.WriteLine("A légitársaságok nevei:");
            for (int i = 0; i < carrier_name_list.Count; i++)
            {
                sw.WriteLine(carrier_name_list[i]);
            }
            sw.Close();
        }

        //Az egyes légitársaságok összes járatának összeszámolása
        static void arrFlights()
        {
            Console.WriteLine("Az egyes légitársaságok összes járata...");
            StreamWriter sw = new StreamWriter("arr_flights_sum.txt");
            sw.WriteLine("Az egyes légitársaságok összes járata:");
            double sum;
            for (int i = 0; i < carrier_name_list.Count; i++)
            {
                sum = 0;
                for (int j = 0; j < repulok.Count; j++)
                {
                    if (carrier_name_list[i] == repulok[j].carrier_name)
                    {
                        sum += repulok[j].arr_flights;
                    }
                }
                //Console.WriteLine(carrier_name_list[i] + "; " + sum);
                sw.WriteLine(carrier_name_list[i] + "; " + sum);
            }
            sw.Close();
        }
        
        //A repülőterek listája; nem saját feladat, de szükséges a következőhöz
        static void airportsName() 
        {
            Console.WriteLine("A repülőterek listája...");
            int j;
            bool van;
            airport_name_list.Add(repulok[0].airport_name);
            for (int i = 1; i < sor; i++)
            {
                j = 0;
                van = false;
                while (j < airport_name_list.Count && !van)
                {
                    if (repulok[i].airport_name == airport_name_list[j])
                    {
                        van = true;
                    }
                    j++;
                }
                if (!van)
                {
                    airport_name_list.Add(repulok[i].airport_name);
                }
            }

            airport_name_list.Sort();

            StreamWriter sw = new StreamWriter("airport_name.txt");
            sw.WriteLine("A repülőterek listája:");
            for (int i = 0; i < airport_name_list.Count; i++)
            {
                sw.WriteLine(airport_name_list[i]);
                //Console.WriteLine(airport_name_list[i]);
            }
            sw.Close();
        }

        //Az egyes légitársaságok által meglátogatott repterek összeszámolása
        static void airports()
        {
            Console.WriteLine("Az egyes légitársaságok által meglátogatott repterek száma...");
            StreamWriter sw = new StreamWriter("airports_count.txt");
            sw.WriteLine("Az egyes légitársaságok által meglátogatott repterek száma:");
            int db;
            for (int i = 0; i < carrier_name_list.Count; i++)
            {
                db = 0;
                //Console.Write(carrier_name_list[i]);
                sw.Write(carrier_name_list[i]);
                for (int j = 0; j < airport_name_list.Count; j++)
                {
                    for (int k = 0; k < repulok.Count; k++)
                    {
                        if (carrier_name_list[i] == repulok[k].carrier_name && airport_name_list[j] == repulok[k].airport_name)
                        {
                            db++;
                        }
                    }
                }
                //if (db != 0) Console.WriteLine(";{0}", db); else Console.WriteLine();
                if (db != 0) sw.WriteLine(";{0}", db); else sw.WriteLine();
            }
            sw.Close();
        }
        
        //Az egyes légitársaságok késéseinek átlagos idejének kiszámítása
        static void carrierDelayAverage()
        {
            Console.WriteLine("Az egyes légitársaságok késéseinek átlagos ideje percben...");
            StreamWriter sw = new StreamWriter("carrier_delay.txt");
            sw.WriteLine("Az egyes légitársaságok késéseinek átlagos ideje percben:");
            double count, sum;
            for (int i = 0; i < carrier_name_list.Count; i++)
            {
                count = 0;
                sum = 0;
                //Console.Write(carrier_name_list[i]);
                sw.Write(carrier_name_list[i]);
                for (int k = 0; k < repulok.Count; k++)
                {
                    if (carrier_name_list[i] == repulok[k].carrier_name)
                    {
                        count += repulok[k].arr_del15;
                        sum += repulok[k].arr_delay;
                    }
                }
                //Console.WriteLine(" {0:0.00}", sum / count);
                sw.WriteLine("; {0:0.00}", sum / count);
                carrierDelay.Add(carrier_name_list[i], sum / count);
            }
            sw.Close();
        }

        //A 3 legkisebb átlagos késésű társaság kiszámítása
        static void carrierDelayAverageMin()
        {
            Console.WriteLine("A 3 legkisebb átlagos késésű társaság...");
            StreamWriter sw = new StreamWriter("carrier_delay_min.txt");
            sw.WriteLine("A 3 legkisebb átlagos késésű társaság:");

            ValueComparerAsc wordComp = new ValueComparerAsc();
            List<KeyValuePair<string, double>> sortedList = new List<KeyValuePair<string, double>>();
            sortedList.AddRange(carrierDelay);
            sortedList.Sort(wordComp);

            //Console.WriteLine(sortedList[0].Key);
            //Console.WriteLine(sortedList[1].Key);
            //Console.WriteLine(sortedList[2].Key);
            sw.WriteLine(sortedList[0].Key);
            sw.WriteLine(sortedList[1].Key);
            sw.WriteLine(sortedList[2].Key);
            sw.Close();
        }

        //A 3 legforgalmasabb reptér kikeresése
        static void airportMax()
        {
            Console.WriteLine("A 3 legforgalmasabb reptér...");
            Dictionary<string, double> airportsDict = new Dictionary<string, double>();
            double sum;
            for (int i = 0; i < airport_name_list.Count; i++)
            {
                sum = 0;
                for (int j = 0; j < repulok.Count; j++)
                {
                    if (airport_name_list[i] == repulok[j].airport_name)
                    {
                        sum += repulok[j].arr_flights;
                    }
                }
                airportsDict.Add(airport_name_list[i], sum);
            }

            //foreach (KeyValuePair<string, double> pair in airportsDict) //itt még nincs sorrendben
            //{
            //    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            //}
            //Console.ReadLine();

            ValueComparerDesc wordComp = new ValueComparerDesc();
            List<KeyValuePair<string, double>> sortedList = new List<KeyValuePair<string, double>>();
            sortedList.AddRange(airportsDict);
            sortedList.Sort(wordComp);

            StreamWriter sw = new StreamWriter("airport_max.txt");
            sw.WriteLine("A 3 legforgalmasabb reptér:");
            //ez is sorbarendezi, de utána nem tudtam kinyerni belőle az első három kulcsot
            //var sortedDict = from entry in airportsDict orderby entry.Value descending select entry;
            //foreach (KeyValuePair<string, double> pair in sortedList)
            //{
            //    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            //}
            //Console.WriteLine(sortedList[0].Key);
            //Console.WriteLine(sortedList[1].Key);
            //Console.WriteLine(sortedList[2].Key);

            sw.WriteLine(sortedList[0].Key);
            sw.WriteLine(sortedList[1].Key);
            sw.WriteLine(sortedList[2].Key);

            sw.Close();
        }

        static void Main(string[] args)
        {
            beolvasas();
            carrierName();
            airportsName();
            arrFlights();
            airports();
            carrierDelayAverage();
            carrierDelayAverageMin();
            airportMax();
            Console.WriteLine("Nyomjon egy billentyűt a kilépéshez...");
            Console.ReadLine();
        }
    }
}

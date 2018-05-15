using System;
using System.Collections.Generic;

namespace zad1
{
    public struct sasiad{
        public string name;
        public double x;
        public double y;
        public double odleglosc;
        public sasiad(string _name, double _x,double _y,double _mx,double _my)
        {
            name = _name;
            x = _x;
            y = _y;
            odleglosc =Math.Round(Math.Sqrt(Math.Pow(Math.Abs(_x-_mx),2)+Math.Pow(Math.Abs(_y-_my),2)),2);
        }   
    }
    class Obserwator
    {
        private List<sasiad> sasiedzi = new List<sasiad>();
        private double x;
        private double y;
        private string name;
        private int count;
        static Random rnd = new Random();
        public Obserwator(int _count,List<Obserwator> _listaO)
        {
            x = rnd.NextDouble()*(1.0);
            y = rnd.NextDouble()*(1.0);
            name = "Obs"+_count;
            count = _count;
            AktualizujSasiadow(_listaO);
        }
        public void AktualizujSasiadow(List<Obserwator> _listaO)
        {
            foreach (var i in _listaO)
            {
                if(count < i.count)
                {
                    var z = new sasiad(i.name,i.x,i.y,this.x,this.y);
                    if(!sasiedzi.Contains(z))
                        sasiedzi.Add(z);
                }
            }
            sasiedzi.Sort((s1,s2) => s1.odleglosc.CompareTo(s2.odleglosc));
            sasiedzi.Reverse();
            while(sasiedzi.Count > 2)
            {
                sasiedzi.RemoveAt(sasiedzi.Count-1);
            }
        }
        public void WypiszSasiadow()
        {
            Console.Write("Jestem "+name+" a to moi sasiedzi: ");
            foreach (var i in sasiedzi)
            {
                Console.Write(i.name);
                Console.Write(" ");
                Console.Write(i.odleglosc);
                Console.Write(" ");
            }
            Console.WriteLine(" "); //dodac endl!!!!
        }
    }
    class Tworca 
    {
        private List<Obserwator> listaObs;
        private int count;
        
        public Tworca()
        {
            listaObs = new List<Obserwator>();
            count = 0;
            Console.WriteLine("Jestem twórcą!");
        }
        public void Nowy()
        {
            listaObs.Add(new Obserwator(count,listaObs));
            Aktualizuj();
            count++;
        }
        public void Nowe(int _n)
        {
            for (int i = 0; i < _n; i++)
            {
                Nowy();
            }
        }
        public void Aktualizuj()
        {
            foreach (var i in listaObs)
            {
                i.AktualizujSasiadow(listaObs);
            }
        }
        public void Wypisz()
        {
            foreach (var i in listaObs)
            {
                i.WypiszSasiadow();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Tworca mojTworca = new Tworca();
            mojTworca.Nowe(10);
            mojTworca.Wypisz();
        }
    }
}
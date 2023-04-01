using System.IO;
using System.Reflection.Metadata;
using System.Security.Cryptography;

namespace Graph
{
    internal class Program
    {
        static Graph bas = null;
        static Graph son = null;
        static Graph gecici;
        static Graph onemli;
        static void sehirekle(string sehir, int plaka)
        {
            Graph current = new Graph();
            current.label = sehir;
            current.counter = 0;
            current.plaka = plaka;
            current.next = null;
            if (bas == null)
            {
                bas = current;
                son = current;
            }
            else
            {
                son.next = current;
                son = current;
            }
        }
        static void yazdır()
        {
            gecici = bas;
            while (gecici.next != null)
            {
                Console.WriteLine(gecici.label + "/" + gecici.plaka);
                gecici = gecici.next;
            }
            Console.WriteLine(gecici.label + "/" + gecici.plaka);
        }

        static void komsuekle(int src, int dest)
        {
            Graph source;
            dugumbul(src);
            source = onemli;
            Graph destination;
            dugumbul(dest);
            destination = onemli;
            source.Komsular[source.counter] = destination;
            source.counter++;
        }
        static bool gelendugumkontrol(int p)
        {
            bool kontrol = false;
            if (bas == null)
            {
                Console.WriteLine("Graf Boş !");
                return false;
            }
            gecici = bas;
            while (gecici != null)
            {
                if (gecici.plaka == p)
                {
                    kontrol = true;
                    break;
                }
                else kontrol = false;
                gecici=gecici.next; 
            }
            if (kontrol == false)
            {
                Console.WriteLine("Hatalı Dugum!");
                return false;
            }
            else return true;
        }
        static void dugumbul(int plaka)
        {

            if (bas == null) return;
            gecici = bas;
            while (gecici.plaka != plaka)
            {
                gecici = gecici.next;

            }
            onemli = gecici;
        }


        static void komsuyaz(int hedef)
        {
            dugumbul(hedef);
            for (int i = 0; i < onemli.counter; i++)
            {
                if (i != onemli.counter - 1)
                    Console.Write(onemli.Komsular[i].label + "/" + onemli.Komsular[i].plaka + "->");
                else
                    Console.Write(onemli.Komsular[i].label + "/" + onemli.Komsular[i].plaka);
            }
            Console.WriteLine(" ");
        }

        static bool dugumVarmi(string S, int P)
        {
            if (bas == null) return false;
            gecici = bas;
            while (gecici.next != null)
            {
                if (gecici.plaka == P)
                    return true;
                gecici = gecici.next;
            }
            if (gecici.plaka == P)
                return true;
            return false;
        }

        static void dugumAktar()
        {
            bool kontrol;
            FileStream fs = new FileStream("sehirler.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string line = sr.ReadLine();
            while (line != null)
            {
                string[] parcala = line.Split("->");
                foreach (string parcala2 in parcala)
                {
                    string[] plakaayir = parcala2.Split('/');
                    kontrol = dugumVarmi(plakaayir[0], Convert.ToInt32(plakaayir[1]));
                    if (kontrol == false)
                        sehirekle(plakaayir[0], Convert.ToInt32(plakaayir[1]));
                }
                line = sr.ReadLine();
            }
        }
        static void KomsuAktar()
        {
            bool kontrol;
            string ilkdugum;
            FileStream fs = new FileStream("sehirler.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string line = sr.ReadLine();
            while (line != null)
            {
                string[] parcala = line.Split("->");
                string[] ilk = parcala[0].Split('/');
                ilkdugum = ilk[1];
                for (int i = 1; i < parcala.Length; i++)
                {
                    string[] plakaayir = parcala[i].Split('/');
                    kontrol = dugumVarmi(plakaayir[0], Convert.ToInt32(plakaayir[1]));
                    if (kontrol == false)
                    {
                        Console.WriteLine("Hatalı Komşu");
                        break;
                    }
                    else
                    {
                        ilkdugum.Trim();
                        plakaayir[0].Trim();
                        komsuekle(Convert.ToInt32(ilkdugum), Convert.ToInt32(plakaayir[1]));
                    }

                }
                line = sr.ReadLine();
            }
        }

        static void girishesapla(int plaka)
        {
            bool kontrol = false;
            gecici = bas;
            while (gecici != null)
            {
                for (int i = 0; i < gecici.counter; i++)
                {
                    if (gecici.Komsular[i].plaka == plaka)
                    {
                        kontrol = true;
                        break;
                    }
                    else kontrol = false;
                }
                if (kontrol == true)
                {
                    Console.Write(gecici.label + "/" + gecici.plaka + " ");
                }

                gecici = gecici.next;
            }
            Console.WriteLine(" ");
        }
        static int girissay = 0;
        static int cıkıssay = 0;
        static void giriscıkıshesap(int plaka)
        {
            bool kontrol = false;
            girissay = 0;
            cıkıssay = 0;
            gecici = bas;
            while (gecici != null)
            {
                for (int i = 0; i < gecici.counter; i++)
                {
                    if (gecici.Komsular[i].plaka == plaka)
                    {
                        kontrol = true;
                        girissay++;
                        break;
                    }
                    else kontrol = false;
                }
                gecici = gecici.next;
            }
            dugumbul(plaka);
            cıkıssay = onemli.counter;
        }
        static void Main(string[] args)
        {
            int sec;
            int kenarsay = 0;
            dugumAktar();
            KomsuAktar();
            while (true)
            {
                Console.WriteLine("0-Çıkış\n1-Komsuluk Listesi\n2-Giriş Çıkış Derece Hesapla\n3-Kenar Sayısı Hesaplama\n4-Gidilecek Şehirleri Listele\n5-Gelinen Yerleri Listele");
                sec = Convert.ToInt32(Console.ReadLine());
                switch (sec)
                {
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Red;
                        gecici = bas;
                        while (gecici.next != null)
                        {
                            Console.Write(gecici.label + "/" + gecici.plaka + "->");
                            komsuyaz(gecici.plaka);
                            gecici = gecici.next;
                        }
                        Console.Write(gecici.label + "/" + gecici.plaka + "->");
                        komsuyaz(gecici.plaka);
                        Console.ResetColor();
                        break;
                    case 2:
                        Console.Write("Giriş Çıkış Derecesi Hesaplanacak Şehir Plakası: ");
                        int GCPlaka = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (gelendugumkontrol(GCPlaka) == true)
                        { 
                            giriscıkıshesap(GCPlaka);
                            Console.WriteLine(GCPlaka + " Plakalı Dugumun Giriş Derecesi: " + girissay + " Çıkış Derecesi: " + cıkıssay);
                        }
                        Console.ResetColor();
                        break;
                    case 3:
                        gecici = bas;
                        while (gecici != null)
                        {
                            dugumbul(gecici.plaka);
                            for (int i = 0; i < onemli.counter; i++)
                            {
                                kenarsay++;
                            }
                            gecici = gecici.next;
                        }
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Graf Kenar Sayısı: " + kenarsay);
                        Console.ResetColor();
                        kenarsay = 0;
                        break;
                    case 4:
                        Console.Write("Sehir Plakası Giriniz: ");
                        int klistele = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (gelendugumkontrol(klistele) == true)
                            komsuyaz(klistele);
                        Console.ResetColor();
                        break;
                    case 5:
                        Console.Write("İstenilen plaka: ");
                        int girplaka = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (gelendugumkontrol(girplaka) == true)
                            girishesapla(girplaka);
                        Console.ResetColor();
                        break;
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Kapatılıyor.. ");
                        Console.ResetColor();
                        Environment.Exit(0); 
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Hata");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}
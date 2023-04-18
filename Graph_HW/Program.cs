using System;
using System.Collections;
using System.IO;
namespace Graph_HW2
{
    class Program
    {
        static Liste list;
        static int Adet()
        {
            FileStream fs = new FileStream("Sehirler.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string line = sr.ReadLine();
            int say = 0;
            while (line != null)
            {
                say++;
                line = sr.ReadLine();
            }
            fs.Close();
            return say;
        }
        static void ListOlustur()
        {
            int say = Adet();
            list = new Liste();
            list.satır = new Dugum[say];
            list.sehirIsmi = new string[say];
            list.plaka = new int[say];
            FileStream fs = new FileStream("Sehirler.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string line = sr.ReadLine();
            int a = 0;
            while (line != null)
            {
                string[] parca = line.Split("->");
                for (int i = 0; i < parca.Length; i++)
                {
                    string[] sehirplaka = parca[i].Split('/');

                    if (i == 0)
                    {
                        list.sehirIsmi[a] = sehirplaka[0];
                        list.plaka[a] = Convert.ToInt32(sehirplaka[1]);

                    }
                    else
                    {
                        Dugum dgm = new Dugum();
                        if (list.satır[a] == null)
                        {
                            list.satır[a] = new Dugum();
                            list.satır[a].SehirAdi = sehirplaka[0];
                            list.satır[a].plaka = Convert.ToInt32(sehirplaka[1]);
                            list.satır[a].Next = null;
                        }
                        else
                        {
                            Dugum yeni = new Dugum();
                            dgm = list.satır[a];
                            while (dgm.Next != null)
                            {
                                dgm = dgm.Next;
                            }
                            yeni.SehirAdi = sehirplaka[0];
                            yeni.plaka = Convert.ToInt32(sehirplaka[1]);
                            dgm.Next = yeni;

                        }

                    }
                }
                a++;
                line = sr.ReadLine();
            }
        }
        static int KenarSay()
        {
            int say = 0;
            for (int i = 0; i < list.satır.Length; i++)
            {
                Dugum d = new Dugum();
                d = list.satır[i];
                while (d != null)
                {
                    say++;
                    d = d.Next;
                }
            }
            return say;
        }
        static void EkranaYaz()
        {
            for (int i = 0; i < list.satır.Length; i++)
            {
                Console.Write(list.sehirIsmi[i] + "/" + list.plaka[i]);
                Dugum dgm = new Dugum();
                dgm = list.satır[i];
                while (dgm != null)
                {
                    Console.Write("->" + dgm.SehirAdi + "/" + dgm.plaka);
                    dgm = dgm.Next;
                }
                Console.WriteLine();
            }
        }
        static void GirisCikisHesapla(int plaka)
        {
            int giris = 0;
            int cikis = 0;
            for (int i = 0; i < list.satır.Length; i++)
            {
                if (list.plaka[i] == plaka)
                {
                    Dugum d1 = new Dugum();
                    d1 = list.satır[i];
                    while (d1 != null)
                    {
                        cikis++;
                        d1 = d1.Next;
                    }
                }
                Dugum d = new Dugum();
                d = list.satır[i];
                while (d != null)
                {
                    if (plaka == d.plaka)
                        giris++;
                    d = d.Next;
                }
            }
            Console.WriteLine(plaka + " plakalı şehrin giriş derecesi: " + giris + " çıkış derecesi: " + cikis);
        }
        static void GelinenYerler(int plaka)
        {
            for (int i = 0; i < list.satır.Length; i++)
            {
                Dugum d = new Dugum();
                d = list.satır[i];
                if (d.plaka == plaka)
                {
                    Console.WriteLine(list.sehirIsmi[i] + "/" + list.plaka[i]);
                }
                while (d != null)
                {
                    if (d.Next == null)
                    {
                        break;
                    }
                    else if (d.Next.plaka == plaka)
                    {
                        Console.WriteLine(list.sehirIsmi[i] + "/" + list.plaka[i]);
                        break;
                    }
                    d = d.Next;
                }
            }

        }
        static void GidilenYerler(int plaka)
        {
            int i = Array.IndexOf(list.plaka, plaka);
            Dugum d = list.satır[i];
            while (d != null)
            {
                Console.WriteLine(d.SehirAdi + "/" + d.plaka);
                d = d.Next;
            }


        }
        static void Main(string[] args)
        {
            int secim;
            ListOlustur();
            while (true)
            {
                Console.Write("0-)Cıkış\n1-)Komşuluk Listesi\n2-)Düğüm Giriş Çıkış Derecesi Hesaplama\n3-)Graftaki Toplam Kenar Sayısı\n4-)Hangi Şehirlere Gidildiğini Öğrenin\n5-)Hangi Şehirlerden Şehrinize Gelindiğini Öğrenin\nSecim: ");
                secim = Convert.ToInt32(Console.ReadLine());
                switch (secim)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Green;
                        EkranaYaz();
                        Console.ResetColor();
                        break;
                    case 2:
                        Console.Write("Plaka girin: ");
                        int plk = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Green;
                        GirisCikisHesapla(plk);
                        Console.ResetColor();
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Toplam Kenar Sayisi: " + KenarSay());
                        Console.ResetColor();
                        break;
                    case 4:
                        Console.Write("Plaka girin: ");
                        int plk1 = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Green;
                        GidilenYerler(plk1);
                        Console.ResetColor();
                        break;
                    case 5:
                        Console.Write("Plaka girin: ");
                        int plk2 = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Green;
                        GelinenYerler(plk2);
                        Console.ResetColor();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Hatalı giriş");
                        Console.ResetColor();
                        break;
                }
                Console.WriteLine();
            }
        }
    }
    class Dugum
    {
        public string SehirAdi;
        public int plaka;
        public Dugum Next;
    }
    class Liste
    {
        public Dugum[] satır;
        public string[] sehirIsmi;
        public int[] plaka;
    }
}
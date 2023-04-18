using System;
using System.IO;

namespace _211213108_Mehmet_KAZAK_Odev_2
{
    class Program
    {
        static int[,] matris;
        static int Adet()
        {
            FileStream fs = new FileStream("graf.txt", FileMode.Open, FileAccess.Read);
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
        static void VeriOku()
        {
            FileStream fs = new FileStream("graf.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string line = sr.ReadLine();
            int a = Adet();
            matris = new int[a, a];
            while (line != null)
            {
                for (int i = 0; i < a; i++)
                {
                    string[] satir = line.Split(' ');
                    for (int j = 0; j < a; j++)
                    {
                        matris[i, j] = int.Parse(satir[j]);
                    }
                    line = sr.ReadLine();
                }
            }
        }
        static void EkranaYaz()
        {
            int a = Adet();
            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < a; j++)
                {
                    Console.Write(matris[i, j].ToString() + " ");
                }
                Console.WriteLine();
            }
        }
        static void Komsular()
        {
            string olusturulan = "";
            for (int i = 0; i < matris.GetLength(0); i++)
            {
                olusturulan += i + " : ";
                for (int j = 0; j < matris.GetLength(1); j++)
                {
                    if (matris[i, j] == 1)
                    {
                        olusturulan += j + ",";
                    }
                }

                Console.WriteLine(olusturulan.Substring(0, olusturulan.Length - 1));
                olusturulan = "";
            }
        }
        static void DepthFirstSearch(int[,] matris, bool[] ziyaret, int a)
        {
            ziyaret[a] = true;
            Console.Write(a + " ");

            for (int i = 0; i < matris.GetLength(0); i++)
            {
                if (matris[a, i] == 1 && !ziyaret[i])
                {
                    DepthFirstSearch(matris, ziyaret, i);
                }
            }
        }
        static void Main(string[] args)
        {
            VeriOku();
            EkranaYaz();
            Console.WriteLine();
            Komsular();
            Console.WriteLine();

            Console.Write("Baslangic duğum numarasını girin :");
            int giris = Convert.ToInt32(Console.ReadLine());

            bool[] ziyaret = new bool[Adet()];
            Console.Write("Depth_First Search sonucu: ");
            
            DepthFirstSearch(matris, ziyaret, giris);




            Console.ReadKey();
        }
    }
}

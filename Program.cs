using System;
using System.Diagnostics;
namespace projekt_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Czas(wstawianie, "sortowanie przez wstawianie");
            Czas(stogowe, "sortowanie stogowe");
            Czas(koktailowe, "sortowanie koktajlowe");
            Czas(QucickSort, "sortowanie QuickSort");
        }
        static int[] Tablica()
        {
            int[] tablica = new int[50000];
            Random r = new Random();
            for (int i = 0; i > tablica.Length; i++) r.Next(0, 50000);
            return tablica;
        }
        static void QucickSort(int[] arr)
        {
            int pivot;
            int x = 0;
            int p, l, i, j;
            int[] lewa = new int[arr.Length];
            int[] prawa = new int[arr.Length];
            lewa[x] = 0;
            prawa[x] = arr.Length - 1;
            do
            {
                l = lewa[x];
                p = prawa[x];
                x--;
                do
                {
                    i = l;
                    j = p;
                    pivot = arr[(l + p) / 2];
                    do
                    {
                        while (arr[i] < pivot) i++;
                        while (arr[j] > pivot) j--;
                        if (i <= j)
                        {
                            var y = arr[i];
                            arr[i] = arr[j];
                            arr[j] = y;
                            i++;
                            j--;
                        }
                    }
                    while (i <= j);
                    if (i < p)
                    {
                        x++;
                        lewa[x] = i;
                        prawa[x] = p;
                    }
                    p = j;
                } while (l < p);
            }while (x >= 0);
        }

        static void wstawianie(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (arr[j - 1] > arr[j])
                    {
                        int x = arr[j - 1]; 
                        arr[j - 1] = arr[j];
                        arr[j] = x;
                    }
                }
            }
        }
        static void kopiec(int[] arr, int a, int b)
        {
            int najwieksza = b;
            int lewa = 2 * b + 1;
            int prawa = 2 * b + 2;
            if (lewa < a && arr[lewa] > arr[najwieksza]) najwieksza = lewa;
            if (prawa < a && arr[prawa] > arr[najwieksza]) najwieksza = prawa;
            if (najwieksza != b)
            {
                int x = arr[b];
                arr[b] = arr[najwieksza];
                arr[najwieksza] = x;
                kopiec(arr, a, najwieksza);
            }
        }
        static void stogowe(int[] arr)
        {
            for (int i = (arr.Length / 2) - 1; i >= 0; i--) kopiec(arr, arr.Length, i);
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                int x = arr[0];
                arr[0] = arr[i];
                arr[i] = x;
                kopiec(arr, i, 0);
            }
        }
        static void koktailowe(int[] arr)
        {
            int lewa = 0;
            int prawa = arr.Length - 1;
            bool t = true;
            while (t == true)
            {
                t = false;
                for (int i = lewa; i < prawa; i++)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        int x = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = x;
                        t = true;
                    }
                }
                prawa = prawa - 1;
                for (int i = prawa; i > lewa; i--)
                {
                    if (arr[i] < arr[i - 1])
                    {
                        int x = arr[i];
                        arr[i] = arr[i - 1];
                        arr[i - 1] = x;
                        t = true;
                    }
                }
                lewa = lewa + 1;
            }
        }
        static void Czas(Action<int[]> AlgorytmSortowania, string nazwa)
        {
            Console.WriteLine("\n"+ nazwa +"\n");
            for (int i = 0; i < 5; i++)
            {
                double srednia = 0.00;
                double min = double.MinValue;
                double max = double.MaxValue;
                double czas=0;
                int operacje = 10;
                for(int j = 0; j <= operacje; j++)
                {
                    int[] arr = Tablica();
                    if (i == 1) Array.Sort(arr, 0, arr.Length);
                    else if (i == 2) Array.Reverse(arr, 0, arr.Length);
                    else if (i == 3)
                    {
                        Random r = new Random();
                        int losowa = r.Next();
                        for (int k=0; k < arr.Length; k++) arr[k] = losowa;
                    }
                    else if (i == 4)
                    {
                        Array.Sort(arr, 0, (arr.Length / 2) + 1);
                        Array.Reverse(arr, 0, (arr.Length / 2) + 1);
                        Array.Sort(arr, arr.Length / 2, arr.Length / 2 + arr.Length % 2);
                    }                   
                    long start = Stopwatch.GetTimestamp();
                    AlgorytmSortowania(arr);
                    long stop = Stopwatch.GetTimestamp();
                    long t = stop - start;
                    czas += t;
                    if (t < min) min = t;
                    if (t > max) max = t;                   
                }
                czas -= (min + max);
                srednia = (czas / operacje/Stopwatch.Frequency);
                if (i == 0) Console.WriteLine("wartości w tablicy generowanej losowo: " + srednia.ToString("F8") + "s");
                else if (i == 1) Console.WriteLine("wartości w tablicy generowanej rosnąco: " + srednia.ToString("F8") + "s");
                else if (i == 2) Console.WriteLine("wartości w tablicy generowanej malejąco: " + srednia.ToString("F8") + "s");
                else if (i == 3) Console.WriteLine("stałe wartości w tablicy: " + srednia.ToString("F8") + "s");
                else Console.WriteLine("tablica V kształtna: " + srednia.ToString("F8") + "s");                
            }
        }
    }
}


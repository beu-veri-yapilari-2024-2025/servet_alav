using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SortingAlgorithms
{
    class SortingDemo
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- SIRALAMA ALGORİTMALARI DEMO (C#) ---");
            Console.Write("Sıralanacak dizi elemanlarını aralarında boşluk bırakarak girin (Örn: 50 12 7 90): ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Boş giriş yapıldı, varsayılan dizi kullanılıyor: 50 12 7 90 18");
                input = "50 12 7 90 18";
            }

            // Girdiyi diziye çevirme
            int[] originalArray;
            try
            {
                originalArray = input.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                     .Select(int.Parse).ToArray();
            }
            catch
            {
                Console.WriteLine("Hatalı giriş! Varsayılan dizi kullanılıyor.");
                originalArray = new int[] { 50, 12, 7, 90, 18 };
            }

            while (true)
            {
                Console.WriteLine("\n--------------------------------");
                Console.WriteLine("Lütfen bir algoritma seçiniz:");
                Console.WriteLine("1. Bubble Sort");
                Console.WriteLine("2. Selection Sort");
                Console.WriteLine("3. Insertion Sort");
                Console.WriteLine("4. Shell Sort");
                Console.WriteLine("5. Heap Sort");
                Console.WriteLine("6. Merge Sort");
                Console.WriteLine("7. Quick Sort");
                Console.WriteLine("8. Radix Sort");
                Console.WriteLine("9. Bucket Sort");
                Console.WriteLine("10. TÜMÜNÜ KARŞILAŞTIR (Performans Testi)");
                Console.WriteLine("0. Çıkış");
                Console.Write("Seçiminiz: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    continue;
                }

                if (choice == 0) break;

                // Her seferinde orijinal dizinin bir kopyası üzerinde çalış
                int[] arr = (int[])originalArray.Clone();

                if (choice == 10)
                {
                    CompareAll(originalArray);
                }
                else
                {
                    PrintComplexity(choice);
                    Console.WriteLine("Başlangıç Dizisi: " + string.Join(", ", arr));
                    
                    Stopwatch sw = Stopwatch.StartNew();

                    // 'true' parametresi adımları yazdırmasını sağlar
                    switch (choice)
                    {
                        case 1: BubbleSort(arr, true); break;
                        case 2: SelectionSort(arr, true); break;
                        case 3: InsertionSort(arr, true); break;
                        case 4: ShellSort(arr, true); break;
                        case 5: HeapSort(arr, true); break;
                        case 6: MergeSort(arr, 0, arr.Length - 1, true); break;
                        case 7: QuickSort(arr, 0, arr.Length - 1, true); break;
                        case 8: RadixSort(arr, true); break;
                        case 9: BucketSort(arr, true); break;
                        default: Console.WriteLine("Geçersiz seçim!"); break;
                    }

                    sw.Stop();
                    Console.WriteLine("Sıralanmış Dizi: " + string.Join(", ", arr));
                    Console.WriteLine($"Geçen Süre: {sw.Elapsed.TotalMilliseconds} ms ({sw.ElapsedTicks} ticks)");
                }
            }
        }

        // --- YARDIMCI METOTLAR ---

        static void PrintStep(int[] arr, string message)
        {
            Console.WriteLine($"{message,-25} -> [{string.Join(", ", arr)}]");
        }

        static void PrintComplexity(int choice)
        {
            Console.WriteLine("\n--- Karmaşıklık Analizi ---");
            switch (choice)
            {
                case 1: Console.WriteLine("Bubble Sort:    O(n^2) Average | O(1) Space"); break;
                case 2: Console.WriteLine("Selection Sort: O(n^2) Average | O(1) Space"); break;
                case 3: Console.WriteLine("Insertion Sort: O(n^2) Average | O(1) Space"); break;
                case 4: Console.WriteLine("Shell Sort:     O(n log n) ~ O(n^2) | O(1) Space"); break;
                case 5: Console.WriteLine("Heap Sort:      O(n log n) All Cases | O(1) Space"); break;
                case 6: Console.WriteLine("Merge Sort:     O(n log n) All Cases | O(n) Space"); break;
                case 7: Console.WriteLine("Quick Sort:     O(n log n) Avg, O(n^2) Worst | O(log n) Space"); break;
                case 8: Console.WriteLine("Radix Sort:     O(nk) | O(n+k) Space"); break;
                case 9: Console.WriteLine("Bucket Sort:    O(n+k) Avg, O(n^2) Worst | O(n) Space"); break;
            }
            Console.WriteLine("---------------------------");
        }

        // --- ALGORİTMALAR ---

        // 1. Bubble Sort
        static void BubbleSort(int[] arr, bool verbose)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                bool swapped = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        swapped = true;
                    }
                }
                if (verbose) PrintStep(arr, $"{i + 1}. Tur (Bubble)");
                if (!swapped) break;
            }
        }

        // 2. Selection Sort
        static void SelectionSort(int[] arr, bool verbose)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int min_idx = i;
                for (int j = i + 1; j < n; j++)
                    if (arr[j] < arr[min_idx])
                        min_idx = j;

                int temp = arr[min_idx];
                arr[min_idx] = arr[i];
                arr[i] = temp;

                if (verbose) PrintStep(arr, $"{i + 1}. Tur (Selection)");
            }
        }

        // 3. Insertion Sort
        static void InsertionSort(int[] arr, bool verbose)
        {
            int n = arr.Length;
            for (int i = 1; i < n; ++i)
            {
                int key = arr[i];
                int j = i - 1;
                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = key;
                if (verbose) PrintStep(arr, $"Eleman {key} eklendi");
            }
        }

        // 4. Shell Sort
        static void ShellSort(int[] arr, bool verbose)
        {
            int n = arr.Length;
            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < n; i += 1)
                {
                    int temp = arr[i];
                    int j;
                    for (j = i; j >= gap && arr[j - gap] > temp; j -= gap)
                        arr[j] = arr[j - gap];
                    arr[j] = temp;
                }
                if (verbose) PrintStep(arr, $"Gap: {gap}");
            }
        }

        // 5. Heap Sort
        static void HeapSort(int[] arr, bool verbose)
        {
            int n = arr.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(arr, n, i);

            for (int i = n - 1; i > 0; i--)
            {
                int temp = arr[0];
                arr[0] = arr[i];
                arr[i] = temp;
                Heapify(arr, i, 0);
                if (verbose) PrintStep(arr, $"Max çekildi: {temp}");
            }
        }

        static void Heapify(int[] arr, int n, int i)
        {
            int largest = i;
            int l = 2 * i + 1;
            int r = 2 * i + 2;
            if (l < n && arr[l] > arr[largest]) largest = l;
            if (r < n && arr[r] > arr[largest]) largest = r;
            if (largest != i)
            {
                int swap = arr[i];
                arr[i] = arr[largest];
                arr[largest] = swap;
                Heapify(arr, n, largest);
            }
        }

        // 6. Merge Sort
        static void MergeSort(int[] arr, int l, int r, bool verbose)
        {
            if (l < r)
            {
                int m = l + (r - l) / 2;
                MergeSort(arr, l, m, verbose);
                MergeSort(arr, m + 1, r, verbose);
                Merge(arr, l, m, r);
                if (verbose) PrintStep(arr, $"Birleştir: {l}-{r}");
            }
        }

        static void Merge(int[] arr, int l, int m, int r)
        {
            int n1 = m - l + 1;
            int n2 = r - m;
            int[] L = new int[n1];
            int[] R = new int[n2];

            Array.Copy(arr, l, L, 0, n1);
            Array.Copy(arr, m + 1, R, 0, n2);

            int i = 0, j = 0, k = l;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j]) { arr[k] = L[i]; i++; }
                else { arr[k] = R[j]; j++; }
                k++;
            }
            while (i < n1) { arr[k] = L[i]; i++; k++; }
            while (j < n2) { arr[k] = R[j]; j++; k++; }
        }

        // 7. Quick Sort
        static void QuickSort(int[] arr, int low, int high, bool verbose)
        {
            if (low < high)
            {
                int pi = Partition(arr, low, high);
                if (verbose) PrintStep(arr, $"Pivot ({arr[pi]}) yerleşti");
                QuickSort(arr, low, pi - 1, verbose);
                QuickSort(arr, pi + 1, high, verbose);
            }
        }

        static int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }
            int temp2 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp2;
            return i + 1;
        }

        // 8. Radix Sort
        static void RadixSort(int[] arr, bool verbose)
        {
            if (arr.Length == 0) return;
            int m = arr.Max();
            for (int exp = 1; m / exp > 0; exp *= 10)
            {
                CountSortRadix(arr, exp);
                if (verbose) PrintStep(arr, $"Basamak: {exp}");
            }
        }

        static void CountSortRadix(int[] arr, int exp)
        {
            int n = arr.Length;
            int[] output = new int[n];
            int[] count = new int[10];

            for (int i = 0; i < n; i++) count[(arr[i] / exp) % 10]++;
            for (int i = 1; i < 10; i++) count[i] += count[i - 1];
            for (int i = n - 1; i >= 0; i--)
            {
                output[count[(arr[i] / exp) % 10] - 1] = arr[i];
                count[(arr[i] / exp) % 10]--;
            }
            for (int i = 0; i < n; i++) arr[i] = output[i];
        }

        // 9. Bucket Sort
        static void BucketSort(int[] arr, bool verbose)
        {
            if (arr.Length == 0) return;
            int maxVal = arr.Max();
            int n = arr.Length;
            int bucketCount = (int)Math.Sqrt(n) + 1;

            List<List<int>> buckets = new List<List<int>>(bucketCount);
            for (int i = 0; i < bucketCount; i++) buckets.Add(new List<int>());

            for (int i = 0; i < n; i++)
            {
                int bucketIndex = (arr[i] * (bucketCount - 1)) / (maxVal + 1);
                buckets[bucketIndex].Add(arr[i]);
            }

            int index = 0;
            for (int i = 0; i < bucketCount; i++)
            {
                buckets[i].Sort();
                foreach (int val in buckets[i])
                {
                    arr[index++] = val;
                }
                if (verbose && buckets[i].Count > 0)
                {
                    Console.WriteLine($"Bucket {i} sıralandı ve eklendi: [{string.Join(", ", buckets[i])}]");
                }
            }
            if (verbose) PrintStep(arr, "Bucket Birleştirme");
        }

        // 10. Karşılaştırma Modu
        static void CompareAll(int[] original)
        {
            Console.WriteLine("\n--- PERFORMANS KARŞILAŞTIRMASI ---");
            Console.WriteLine($"{"Algoritma",-20} | {"Ticks",-15} | {"Milisaniye",-15}");
            Console.WriteLine(new string('-', 55));

            RunAndMeasure("Bubble Sort", 1, original);
            RunAndMeasure("Selection Sort", 2, original);
            RunAndMeasure("Insertion Sort", 3, original);
            RunAndMeasure("Shell Sort", 4, original);
            RunAndMeasure("Heap Sort", 5, original);
            RunAndMeasure("Merge Sort", 6, original);
            RunAndMeasure("Quick Sort", 7, original);
            RunAndMeasure("Radix Sort", 8, original);
            RunAndMeasure("Bucket Sort", 9, original);
        }

        static void RunAndMeasure(string name, int type, int[] original)
        {
            int[] arr = (int[])original.Clone();
            Stopwatch sw = Stopwatch.StartNew();

            // verbose: false gönderiyoruz ki adımları yazmasın, sadece hızı ölçsün
            switch (type)
            {
                case 1: BubbleSort(arr, false); break;
                case 2: SelectionSort(arr, false); break;
                case 3: InsertionSort(arr, false); break;
                case 4: ShellSort(arr, false); break;
                case 5: HeapSort(arr, false); break;
                case 6: MergeSort(arr, 0, arr.Length - 1, false); break;
                case 7: QuickSort(arr, 0, arr.Length - 1, false); break;
                case 8: RadixSort(arr, false); break;
                case 9: BucketSort(arr, false); break;
            }

            sw.Stop();
            Console.WriteLine($"{name,-20} | {sw.ElapsedTicks,-15} | {sw.Elapsed.TotalMilliseconds,-15:F4}");
        }
    }
}

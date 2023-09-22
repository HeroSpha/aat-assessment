using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Assessment2.Data;

class Program
{
    private static List<int> globalList = new List<int>();
    private static readonly object lockObject = new object();
    private static int oddCount = 0;
    private static int evenCount = 0;

    static void Main()
    {
        Thread oddThread = new Thread(AddRandomOddNumbers);
        Thread primeThread = new Thread(AddNegativePrimes);
        Thread evenThread = new Thread(AddRandomEvenNumbers);

        oddThread.Start();
        primeThread.Start();

        while (globalList.Count < 250000)
        {
            // Wait for the first two threads to reach 250,000 items
            Thread.Sleep(100);
        }

        evenThread.Start();

        while (globalList.Count < 1000000)
        {
            // Wait for all threads to finish
            Thread.Sleep(100);
        }

        // Sort the global list
        globalList.Sort();

        // Count odd and even numbers
        foreach (int num in globalList)
        {
            if (num % 2 == 0)
                evenCount++;
            else
                oddCount++;
        }

        IFileSaver dbServer = new Saver();
        dbServer = new XmlFileDb(dbServer);
        dbServer = new BinaryFileDb(dbServer);
        dbServer.Save(globalList);
        //
        // // Serialize the list to binary
        // using (System.IO.Stream stream = System.IO.File.Open("output.bin", System.IO.FileMode.Create))
        // {
        //     var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        //    // binaryFormatter.Serialize(stream, globalList);
        // }
        //
        // // Serialize the list to XML
        // var xmlRoot = new System.Xml.Linq.XElement("Numbers");
        // foreach (int num in globalList)
        // {
        //     xmlRoot.Add(new System.Xml.Linq.XElement("Number", num));
        // }
        // xmlRoot.Save("output.xml");

        // Display odd and even counts
        Console.WriteLine("Odd Count: " + oddCount);
        Console.WriteLine("Even Count: " + evenCount);
    }

    static void AddRandomOddNumbers()
    {
        while (globalList.Count < 250000)
        {
            var num = GetRandomOddNumber();
            lock (lockObject)
            {
                globalList.Add(num);
            }
        }
    }

    static void AddNegativePrimes()
    {
        while (globalList.Count < 250000)
        {
            var prime = GetNextPrime();
            lock (lockObject)
            {
                globalList.Add(-prime);
            }
        }
    }

    static void AddRandomEvenNumbers()
    {
        while (globalList.Count < 1000000)
        {
            int num = GetRandomEvenNumber();
            lock (lockObject)
            {
                globalList.Add(num);
            }
        }
    }

    static bool IsPrime(int number)
    {
        if (number <= 1)
            return false;
        if (number <= 3)
            return true;
        if (number % 2 == 0 || number % 3 == 0)
            return false;

        for (int i = 5; i * i <= number; i += 6)
        {
            if (number % i == 0 || number % (i + 2) == 0)
                return false;
        }
        return true;
    }

    static int GetNextPrime()
    {
        int num = 2;
        while (true)
        {
            if (IsPrime(num))
                return num;
            num++;
        }
    }

    static int GetRandomOddNumber()
    {
        Random rand = new Random();
        int num = rand.Next(1, int.MaxValue);
        return num % 2 == 1 ? num : num + 1;
    }

    static int GetRandomEvenNumber()
    {
        Random rand = new Random();
        int num = rand.Next(2, int.MaxValue);
        return num % 2 == 0 ? num : num - 1;
    }
}

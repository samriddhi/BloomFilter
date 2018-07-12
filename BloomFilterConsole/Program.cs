using System;
using System.Text;

namespace BloomFilterConsole
{
    class Program
    {
        private static byte[] _bloomFilter;
        static void Main(string[] args)
        {
            Run();
            //Make sure console doesnt close
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                Run();
            }
        }

        static void Run()
        {
            Console.WriteLine("Enter bitmap size or press enter to use default (10,000):");

            //bitmap size default value = 10,000
            string input = Console.ReadLine();
            if (Int32.TryParse(input, out int m))
            {
                _bloomFilter = new byte[m];
            }
            else
            {
                _bloomFilter = new byte[10000];
            }


            Console.WriteLine("Enter number of hashes(k) or press enter to use default (100):");
            //Number of hashes default value = 1
            string input2 = Console.ReadLine();

            if (!Int32.TryParse(input2, out int k))
            {
                k = 1;
            }

            //Read file and call Add function.
            Console.WriteLine("Inputing all the words....");
            var lines = System.IO.File.ReadAllLines("C:\\words.txt");
            foreach (var line in lines)
            {
                Add(line, k);
            }

            Console.WriteLine("Done...");

            Console.WriteLine("Enter word to check");
            string term = Console.ReadLine();


            Console.WriteLine(Exists(term, k));


            //Any other key will resetart the process
            Console.WriteLine("Press any key to continue or Esc to exit");
        }

        static void Add(string term, int k)
        {
            //Hash Item and add to bitmap 
            var hashed = Hash(term, k);

            foreach (var h in hashed)
            {
                _bloomFilter[h % _bloomFilter.Length] = 1;
               
            }
        }

        static string Exists(string term, int k)
        {
            //check if item exists;
            var hashed = Hash(term, k);

            foreach (var h in hashed)
            {
                if (_bloomFilter[h % _bloomFilter.Length] == 0)
                {
                    return term + " definetly does not exist";
                }
            }

            return term + " might exist";
        }

        private static int[] Hash(string term, int k)
        {
            int[] kHash = new int[k];
            for(var i = 0; i <= k -1; i++)
            {
                //add the k byte at the end of term to create k amount of hashes
                using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
                {
                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(term + i);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);

                    var intHash = BitConverter.ToInt32(hashBytes);

                    kHash[i] = Math.Abs(intHash);
                }

            }

            return kHash;
        } 


            

    }
}

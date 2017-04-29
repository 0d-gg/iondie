using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace iondie
{
    class Program
    {
        static void Main(string[] args)
        {
            var results = DnsInfo.GetDnsCacheEntries();

            Console.WriteLine(results.Count);

            foreach (var entry in results)
            {
                Console.WriteLine(entry.Name + "\t" + entry.TypeName);
            }
            Console.ReadKey();
        }


    }
}

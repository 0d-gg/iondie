using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iondie
{
    class Program
    {
        static DnsCacheSpy spy;
        static int count = 0;
        static void Main(string[] args)
        {
            spy = new DnsCacheSpy();
            spy.StartWatching();
            spy.Update += Spy_Update;

            Console.ReadKey();
        }

        private static void Spy_Update()
        {
            if(spy.MasterList.Count != count)
            {
                for (int i = count; i < spy.MasterList.Count; i++)
                {
                    Console.WriteLine(spy.MasterList[i].Name);
                }
            }
        }
    }
}

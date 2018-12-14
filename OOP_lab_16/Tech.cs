using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace OOP_lab_16
{
    public class Tech
    {
        static BlockingCollection<int> bc;

        public static void Producer()
        {
            for (int i = 0; i < 100; i++)
            {
                bc.Add(i * i);
                Console.WriteLine("Производится число " + i * i);
            }
            bc.CompleteAdding();
        }

        public static void Consumer()
        {
            int i;
            while (!bc.IsCompleted)
            {
                if (bc.TryTake(out i))
                    Console.WriteLine("Потребляется число: " + i);
            }
        }
    }

}
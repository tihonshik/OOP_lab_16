using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OOP_lab_16
{
    public class Shop
    {
        private BlockingCollection<int> _blockingCollection;
        private List<Task> _providers;
        private List<Task> _customers;

        public Shop()
        {
            _blockingCollection = new BlockingCollection<int>();
            _providers = new List<Task>();
            _customers = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                if (i < 5)
                    _providers.Add(new Task(() => Provider(i)));
                _customers.Add(new Task(() => Customer(i)));
            }
        }

        public void Start()
        {
            Console.WriteLine("Работа начата!");
            for (int i = 0; i < 10; i++)
            {
                if (i < 5)
                    _providers[i].Start();
                _customers[i].Start();
            }

            Task.WaitAll(_customers.ToArray());
        }

        private void Provider(int time)
        {
            _blockingCollection.Add(time);
            Thread.Sleep(time * 100);
        }

        private void Customer(int wait)
        {
            wait *= 200;
            if (_blockingCollection.TryTake(out wait))
                Console.WriteLine("Клиент не дождался.");
            else Console.WriteLine("Клиент купил вещь.");
        }
    }
}
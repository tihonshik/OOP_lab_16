using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OOP_lab_16
{   
    
    public class Program
    {
        
        static void Operators()
        {
            Console.WriteLine("----------------------+");
            int[] arr = new int[100000];
            for(int i = 0; i < arr.Length; i++)
            {
                arr[i] = i;
            }
            for(int i=0; i<10; i++)
            {
                Console.WriteLine(arr[i]);
            }
            Console.WriteLine("-----------------------");
        }
        static void Factorial(int x)
        {
            int result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }
            Console.WriteLine("Выполняется задача {0}", Task.CurrentId);
            Console.WriteLine("Факториал числа {0} равен {1}", x, result);
            Thread.Sleep(1000);
        }
        
        public static async void FactorialAsync(int Number)
        {
            await Task.Run(() =>
            {
                int result = 1;

                for (int i = 1; i <= Number; i++)
                {
                    result *= i;
                }
                Console.WriteLine("Выполняется задача {0}", Task.CurrentId);
                Console.WriteLine("Факториал числа {0} равен {1}", Number, result);
                Thread.Sleep(1000);
            });
        } 
      
        
        public static void Main(string[] args)
        {
            MultipleVector mTrue = new MultipleVector(true);
            MultipleVector mFalse = new MultipleVector(false);
            
            mFalse.Multiple();
            mTrue.Multiple();
            
            Task<int> getA = new Task<int>(() =>
            {
                int a = 0;
                for(int i=0; i<100; i++)
                {
                    a++;
                }
                return a;
            });
            Task<int> getB = new Task<int>(() =>
            {
                int b = 0;
                for (int i = 0; i < 22; i++)
                {
                    b++;
                }
                return b;
            });
            Task<int> getC = new Task<int>(() =>
            {
                int c = 0;
                for (int i = 0; i < 88; i++)
                {
                    c++;
                }
                return c;
            });
            Task<int> formula = new Task<int>(() =>
            {
                getA.Start();
                getB.Start();
                getC.Start();

                return getA.Result * getB.Result * getC.Result;
            });
            formula.Start();
            Console.WriteLine("Result formula: {0}", formula.Result);
            
            //task 4            
            Task conTask1 = new Task(() => { Console.WriteLine("Текущий ID(1) : " + Task.CurrentId); });
            Task conTask2 = conTask1.ContinueWith((Task t) => { Console.WriteLine(" : "+Task.CurrentId); });
            
            conTask1.Start();
            Task.WaitAll(conTask1, conTask2);
            
            
            Parallel.For(1, 10, Factorial);
            Parallel.ForEach<int>(new List<int>() { 1, 3 }, Factorial);
            Parallel.Invoke(Operators);
            
            FactorialAsync(7);

            Shop shop = new Shop();
            shop.Start();

        }
    }
}
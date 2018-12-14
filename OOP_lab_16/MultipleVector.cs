using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OOP_lab_16
{
    public class MultipleVector
    {
        int[] vector = new int[1000000];
        bool stopToken = false;
        const int x = 2;
        
        public MultipleVector(bool stopToken)
        {
            this.stopToken = stopToken;

            for(int i = 0; i < vector.Length; i++)
            {
                vector[i] = i;
            }
        }
        
        
        public void Multiple()
        {
            CancellationTokenSource CTokenSource = new CancellationTokenSource();
            if (stopToken == true)
            {
                Console.WriteLine("Task is close.");
            }
            
            
            Task v1 = new Task(() =>
            {
                if (stopToken == true)
                    CTokenSource.Cancel();
                
                Console.WriteLine("Task ID: {0}", Task.CurrentId);
                for (var i = 0; i < vector.Length/2; i++)
                {
                    vector[i] *= x;
                }
            });
            
            Task v2 = new Task(() =>
            {
                if (stopToken == true)
                    CTokenSource.Cancel();
                
                for (var i = vector.Length/2; i < vector.Length; i++)
                {
                    vector[i] *= x;
                }
            });


                Stopwatch time = new Stopwatch();
                time.Start();
                
                v1.Start();
                v2.Start();
                
                Console.WriteLine("Task completed: {0}", v1.IsCompleted);
                Console.WriteLine("Task status: {0}", v1.Status);
                
                // Wait completed first and second task
                Task.WaitAll(v1, v2); 
                
                time.Stop();
                Console.WriteLine("Time milliseconds: {0}", time.ElapsedMilliseconds);
   



           
        }
        
    }
}
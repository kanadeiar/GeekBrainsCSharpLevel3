using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace Task1.Models
{
    class Calculations
    {
        public Calculations()
        {
        }

        public (long, long) CalculateFactorialAndSumParallel(long value)
        {
            long fact = 0;
            long sum = 0;
            Thread[] threads = new Thread[2];
            threads[0] = new Thread(() => Fact(value, out fact));
            threads[1] = new Thread(() => Sum(value, out sum));
            foreach (var th in threads)
                th.Start();
            foreach (var th in threads)
                th.Join();
            return (fact, sum);
        }
        private void Fact(long value, out long result)
        {
            long fact = 1;
            while (value > 1)
            {
                fact *= value;
                value -= 1;
            }
#if DEBUG
            Thread.Sleep(5_000); //Для прикола
#endif
            result = fact;
        }
        private void Sum(long value, out long result)
        {
            long sum = 0;
            for (long i = 1; i <= value; i++)
            {
                sum += i;
            }
#if DEBUG
            Thread.Sleep(5_000); //Для прикола
#endif
            result = sum;
        }
    }
}

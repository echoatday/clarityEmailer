using System;
using ClarityDLL;

namespace clarityInterview
{
    class Program
    {
        // Used to run the dll initially with a hard-coded email. If you're here, run ClarityEmail instead
        static void Main(string[] args) 
        {
            Counter<int>.Value++;
            Console.Write(Counter<double>.Value);
        }
    }

    class Counter<T>
    {
        public static int Value;
    }
}

using System;
using DistanceCalculatorClient.ServiceReference;

namespace DistanceCalculatorClient
{
    static class DSClient
    {
        static void Main(string[] args)
        {
            var client = new ServiceDistanceCalculatorClient();
            Console.WriteLine(client.CalcDistance(new Point() { X = 1, Y = 2 }, new Point() { X = 5, Y = 10 }));
        }
    }
}

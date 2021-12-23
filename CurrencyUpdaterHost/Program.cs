using System;
using System.ServiceModel;

namespace CurrencyUpdaterHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host  = new ServiceHost(typeof(CurrencyUpdater.CurrencyUpdate)))
            {
                host.Open();

                Console.WriteLine("Host open");

                Console.ReadLine();
            }
        }
    }
}

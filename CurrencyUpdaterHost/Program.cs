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
                try
                {
                    host.Open();

                    Console.WriteLine("Host open");
                }
                catch (AddressAlreadyInUseException ex)
                {
                    Console.WriteLine(ex.Message);
                }
 
                Console.ReadLine();
            }
        }
    }
}

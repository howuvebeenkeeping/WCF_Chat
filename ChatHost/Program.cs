using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ChatHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(WCF_Chat.ServiceChat)))
            {
                host.Open();
                Console.WriteLine("Host is working...");
                Console.ReadLine();
            }
        }
    }
}

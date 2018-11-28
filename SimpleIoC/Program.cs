using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace SimpleIoC
{
    public class PaymentProcessor
    {

    }

    class Program
    {
        static void Main(string[] args)
        {
            SimpleIoC ioc = new SimpleIoC();
            ioc.Register<MainViewModel, MainViewModel>();
            ioc.Register<ICustomer, Customer>();
            ioc.Register<ICustomerRepository, CustomerRepository>();
            ioc.Register<IDbGateway, DbGateway>();

            var mainViewModel = ioc.Resolve<MainViewModel>();

            ReadLine();
        }
    }
}

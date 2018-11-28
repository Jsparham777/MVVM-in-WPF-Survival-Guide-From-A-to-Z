namespace SimpleIoC
{
    public class MainViewModel
    {
        public ICustomer Customer { get; set; }

        public MainViewModel()
        {
            Customer = new Customer(new CustomerRepository(new DbGateway()));
        }
    }
}
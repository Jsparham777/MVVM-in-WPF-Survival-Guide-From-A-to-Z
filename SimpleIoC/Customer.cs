namespace SimpleIoC
{
    public class Customer : ICustomer
    {
        private readonly ICustomerRepository _customerRepository;

        public Customer(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
    }
}
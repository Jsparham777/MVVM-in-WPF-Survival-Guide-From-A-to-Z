namespace SimpleIoC
{
    public class CustomerRepository :ICustomerRepository
    {
        private readonly IDbGateway _gateway;

        public CustomerRepository(IDbGateway gateway)
        {
            _gateway = gateway;
        }
    }
}
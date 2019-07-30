using System.Threading.Tasks;
using CleanMocking.Model;
using CleanMocking.Repositories;

namespace CleanMocking.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task Add(Customer customerToAdd)
        {
            await _customerRepository.Add(customerToAdd);
        }
    }
}
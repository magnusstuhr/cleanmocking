using System;
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
            var customer = await _customerRepository.Add(customerToAdd);

            ValidateCustomer(customer);
        }

        private void ValidateCustomer(Customer customer)
        {
            if (customer is null)
            {
                throw new Exception("An error occurred when trying to " +
                                    $"{ToLower(nameof(Add))} the {ToLower(nameof(Customer))}.");
            }
        }

        private static string ToLower(string s)
        {
            return s.ToLowerInvariant();
        }
    }
}
using System.Threading.Tasks;
using CleanMocking.Model;

namespace CleanMocking.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> Add(Customer customer);

        Task<Customer> Get();

        Task Update(Customer customer);

        Task Delete(string customerId);
    }
}
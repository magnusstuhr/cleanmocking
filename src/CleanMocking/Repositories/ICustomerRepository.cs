using System.Threading.Tasks;
using CleanMocking.Model;

namespace CleanMocking.Repositories
{
    public interface ICustomerRepository
    {
        Task Add(Customer customer);

        Task<Customer> Get();

        Task Update(Customer customer);

        Task Delete(string customerId);
    }
}
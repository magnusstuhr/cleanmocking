using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CleanMocking.Model;
using CleanMocking.Repositories;
using Moq;
using Moq.Language.Flow;

namespace CleanMockingTests.Mocks
{
    public class CustomerRepositoryMock : Mock<ICustomerRepository>
    {
        private static readonly Times TimesOnce = Times.Once();

        public void SetupAddReturns(Customer customerToAddAndReturn)
        {
            SetupAddReturns(customerToAddAndReturn, customerToAddAndReturn);
        }

        public void SetupAddReturns(Customer customerToAdd, Customer customerToReturn)
        {
            SetupAdd(customerToAdd).ReturnsAsync(customerToReturn);
        }

        public void VerifyOnlyAddWasCalledOnce(Customer customerAdded)
        {
            VerifyOnlyAddWasCalled(TimesOnce, customerAdded);
        }

        public void VerifyOnlyAddWasCalled(Times times, Customer customerAdded)
        {
            VerifyAddWasCalled(times, customerAdded);
            VerifyNoOtherCalls();
        }

        public void VerifyAddWasCalled(Times times, Customer customerAdded)
        {
            var addExpression = CreateAddExpression(customerAdded);
            Verify(addExpression, times);
        }

        private ISetup<ICustomerRepository, Task<Customer>> SetupAdd(Customer customerToAdd)
        {
            var addExpression = CreateAddExpression(customerToAdd);
            return SetupCustomerReturned(addExpression);
        }

        private ISetup<ICustomerRepository, Task<Customer>> SetupCustomerReturned(
            Expression<Func<ICustomerRepository, Task<Customer>>> expression)
        {
            return Setup(expression);
        }

        private static Expression<Func<ICustomerRepository, Task<Customer>>> CreateAddExpression(Customer customerAdded)
        {
            return repository => repository.Add(customerAdded);
        }
    }
}
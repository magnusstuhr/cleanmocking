using System;
using CleanMocking.Model;
using CleanMocking.Repositories;
using CleanMocking.Services;
using Moq;
using Xunit;

namespace CleanMockingTests.Services
{
    public class CustomerServiceTests
    {
        [Fact]
        public async void Add_CustomerRepositorySucceeds_ExpectedCallsWereInvoked()
        {
            // Arrange
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var customerRepository = customerRepositoryMock.Object;
            var customerService = new CustomerService(customerRepository);
            var customerToAdd = CreateRandomCustomer();

            // Act
            await customerService.Add(customerToAdd);

            // Assert
            customerRepositoryMock.Verify(repository => repository.Add(customerToAdd), Times.Once());
            customerRepositoryMock.VerifyNoOtherCalls();
        }

        private static Customer CreateRandomCustomer()
        {
            return new Customer
            {
                Id = CreateRandomString(),
                Name = CreateRandomString()
            };
        }

        private static string CreateRandomString()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
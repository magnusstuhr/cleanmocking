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
        public async void Add_CustomerRepositorySucceeds_ExpectedCallsWereMade()
        {
            // Arrange
            var customerToAdd = CreateRandomCustomer();

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(repository => repository.Add(customerToAdd)).ReturnsAsync(customerToAdd);

            var customerRepository = customerRepositoryMock.Object;
            var customerService = new CustomerService(customerRepository);

            // Act
            await customerService.Add(customerToAdd);

            // Assert
            customerRepositoryMock.Verify(repository => repository.Add(customerToAdd), Times.Once());
            customerRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Add_CustomerRepositoryReturnsNull_ThrowsExceptionAndExpectedCallsWereMade()
        {
            // Arrange
            Customer customerToAdd = null;

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(repository => repository.Add(customerToAdd)).ReturnsAsync(customerToAdd);

            var customerRepository = customerRepositoryMock.Object;
            var customerService = new CustomerService(customerRepository);

            // Act
            var exception = await Record.ExceptionAsync(() => customerService.Add(customerToAdd));

            // Assert
            Assert.IsType<Exception>(exception);
            var exceptionMessage = exception.Message;
            Assert.Equal("An error occurred when trying to add the customer.", exceptionMessage);
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
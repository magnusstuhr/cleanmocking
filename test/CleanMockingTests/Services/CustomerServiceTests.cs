using System;
using CleanMocking.Model;
using CleanMocking.Repositories;
using CleanMocking.Services;
using CleanMockingTests.Mocks;
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

            var customerRepositoryMock = new CustomerRepositoryMock();
            customerRepositoryMock.SetupAddReturns(customerToAdd);

            var customerService = CreateCustomerService(customerRepositoryMock);

            // Act
            await customerService.Add(customerToAdd);

            // Assert
            customerRepositoryMock.VerifyOnlyAddWasCalledOnce(customerToAdd);
        }

        [Fact]
        public async void Add_CustomerRepositoryReturnsNull_ThrowsExceptionAndExpectedCallsWereMade()
        {
            // Arrange
            Customer customerToAdd = null;
            const string expectedExceptionMessage = "An error occurred when trying to add the customer.";

            var customerRepositoryMock = new CustomerRepositoryMock();
            customerRepositoryMock.SetupAddReturns(customerToAdd);

            var customerService = CreateCustomerService(customerRepositoryMock);

            // Act
            var exception = await Record.ExceptionAsync(() => customerService.Add(customerToAdd));

            // Assert
            VerifyException<Exception>(exception, expectedExceptionMessage);
            customerRepositoryMock.VerifyOnlyAddWasCalledOnce(customerToAdd);
        }

        private static void VerifyException<T>(Exception exceptionToVerify, string expectedExceptionMessage)
            where T : Exception
        {
            Assert.IsType<T>(exceptionToVerify);
            var exceptionMessage = exceptionToVerify.Message;
            Assert.Equal(expectedExceptionMessage, exceptionMessage);
        }

        private static CustomerService CreateCustomerService(IMock<ICustomerRepository> customerRepositoryMock)
        {
            return new CustomerService(customerRepositoryMock.Object);
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
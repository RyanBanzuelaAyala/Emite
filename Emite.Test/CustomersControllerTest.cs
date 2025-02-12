using Emite.Api.Controllers;
using Emite.Domain.Model.V1.CustomerVM;
using Emite.Infrastructure.V1.CustomerService.Command.Create;
using Emite.Infrastructure.V1.CustomerService.Command.Delete;
using Emite.Infrastructure.V1.CustomerService.Command.Update;
using Emite.Infrastructure.V1.CustomerService.Query.ListAll;
using Emite.Infrastructure.V1.CustomerService.Query.ListBy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace Emite.Api.Customers.Test;

public class CustomersControllerTests
{
    private readonly Mock<IMemoryCache> _memoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly CustomersController _controller;

    public CustomersControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _memoryMock = SetupMemoryCache();
        _controller = new CustomersController(_mediatorMock.Object, _memoryMock.Object);
    }

    private Mock<IMemoryCache> SetupMemoryCache()
    {
        var cacheMock = new Mock<IMemoryCache>();
        var cacheEntryMock = new Mock<ICacheEntry>();
        cacheMock.Setup(m => m.CreateEntry(It.IsAny<object>()))
                 .Returns(cacheEntryMock.Object);
        return cacheMock;
    }

    [Fact]
    public async Task Create_ShouldReturnCustomer()
    {
        // Arrange
        var customer = new Customer
        {
            Name = "John Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "123-456-7890",
            LastContactDate = DateTime.Now.AddDays(-1)
        };

        var expectedCustomer = new Customer
        {
            Id = 1,
            Name = "John Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "123-456-7890",
            LastContactDate = DateTime.Now.AddDays(-1)
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateCmd>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCustomer);

        // Act
        var result = await _controller.Create(customer);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Customer>>(result);
        var returnedCustomer = Assert.IsType<Customer>(actionResult.Value);
        Assert.Equal(expectedCustomer, returnedCustomer);
    }

    [Fact]
    public async Task Update_ShouldReturnCustomer()
    {
        // Arrange
        var customer = new Customer
        {
            Id = 1,
            Name = "John Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "123-456-7890",
            LastContactDate = DateTime.Now.AddDays(-1)
        };

        var expectedCustomer = new Customer
        {
            Id = 1,
            Name = "John Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "123-456-7890",
            LastContactDate = DateTime.Now.AddDays(-1)
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateCmd>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCustomer);

        // Act
        var result = await _controller.Update(customer);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Customer>>(result);
        var returnedCustomer = Assert.IsType<Customer>(actionResult.Value);
        Assert.Equal(expectedCustomer, returnedCustomer);
    }

    [Fact]
    public async Task Delete_ShouldReturnCustomer()
    {
        // Arrange
        var customer = new Customer
        {
            Id = 1,
            Name = "John Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "123-456-7890",
            LastContactDate = DateTime.Now.AddDays(-1)
        };

        var expectedCustomer = new Customer
        {
            Id = 1,
            Name = "John Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "123-456-7890",
            LastContactDate = DateTime.Now.AddDays(-1)
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateCmd>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCustomer);

        // Act
        var resultAdd = await _controller.Create(customer);

        // Arrange
        int customerIdRemove = 1;
        var expectedCustomerRemove = new Customer
        {
            Id = 1,
            Name = "John Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "123-456-7890",
            LastContactDate = DateTime.Now.AddDays(-1)
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteCmd>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCustomerRemove);

        // Act
        var result = await _controller.Delete(customerIdRemove);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Customer>>(result);
        var returnedCustomer = Assert.IsType<Customer>(actionResult.Value);
        Assert.Equal(expectedCustomerRemove, returnedCustomer);
    }

    [Fact]
    public async Task ListBy_ShouldReturnCustomers()
    {
        // Arrange
        int customerId = 1;
        var customersExpected = new List<Customer>
    {
        new Customer
        {
            Id = 1,
            Name = "John Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "123-456-7890",
            LastContactDate = DateTime.Now.AddDays(-1)
        },
        new Customer
        {
            Id = 2,
            Name = "Jane Smith",
            Email = "jane.smith@example.com",
            PhoneNumber = "098-765-4321",
            LastContactDate = DateTime.Now.AddDays(-2)
        }
    };

        _mediatorMock.Setup(m => m.Send(It.IsAny<ListByQry>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(customersExpected);

        // Act
        var result = await _controller.ListBy(customerId);

        // Assert
        var actionResult = Assert.IsType<List<Customer>>(result);
        Assert.Equal(customersExpected.Count, actionResult.Count());
    }

    [Fact]
    public async Task ListAll_ShouldReturnCustomers()
    {
        var customersExpected = new List<Customer>
    {
        new Customer
        {
            Id = 1,
            Name = "John Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "123-456-7890",
            LastContactDate = DateTime.Now.AddDays(-1)
        },
        new Customer
        {
            Id = 2,
            Name = "Jane Smith",
            Email = "jane.smith@example.com",
            PhoneNumber = "098-765-4321",
            LastContactDate = DateTime.Now.AddDays(-2)
        }
    };

        _mediatorMock.Setup(m => m.Send(It.IsAny<ListAllQry>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(customersExpected);

        // Act
        var result = await _controller.ListAll();

        // Assert
        var actionResult = Assert.IsType<List<Customer>>(result);
        Assert.Equal(customersExpected.Count, actionResult.Count());
    }

}

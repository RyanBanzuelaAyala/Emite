using Emite.Api.Controllers;
using Emite.Domain.Model.V1.CallVM;
using Emite.Infrastructure.V1.CallService.Command.Create;
using Emite.Infrastructure.V1.CallService.Command.Delete;
using Emite.Infrastructure.V1.CallService.Command.Update;
using Emite.Infrastructure.V1.CallService.Query.ListAll;
using Emite.Infrastructure.V1.CallService.Query.ListBy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace Emite.Api.Calls.Test;

public class CallsControllerTests
{
    private readonly Mock<IMemoryCache> _memoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly CallsController _controller;

    public CallsControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _memoryMock = SetupMemoryCache();
        _controller = new CallsController(_mediatorMock.Object, _memoryMock.Object);
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
    public async Task Create_ShouldReturnCall()
    {
        // Arrange
        var call = new Call
        {
            CustomerId = "CUST123",
            AgentId = 1001,
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddMinutes(5),
            Status = CallStatus.Completed,
            Notes = "Test call"
        };

        var expectedCall = new Call
        {
            Id = 1,
            CustomerId = "CUST123",
            AgentId = 1001,
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddMinutes(5),
            Status = CallStatus.Completed,
            Notes = "Test call"
        };


        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateCmd>(), default))
            .ReturnsAsync(expectedCall);

        // Act
        var result = await _controller.Create(call);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Call>>(result);
        var returnedcall = Assert.IsType<Call>(actionResult.Value);

        Assert.Equal(expectedCall, returnedcall);
    }

    [Fact]
    public async Task Update_ShouldReturnCall()
    {
        // Arrange
        var call = new Call
        {
            Id = 1,
            CustomerId = "CUST123",
            AgentId = 1001,
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddMinutes(5),
            Status = CallStatus.Completed,
            Notes = "Test call"
        };

        var expectedCall = new Call
        {
            Id = 1,
            CustomerId = "CUST123",
            AgentId = 1001,
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddMinutes(5),
            Status = CallStatus.Completed,
            Notes = "Test call"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateCmd>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCall);

        // Act
        var result = await _controller.Update(call);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Call>>(result);
        var returnedcall = Assert.IsType<Call>(actionResult.Value);

        Assert.Equal(expectedCall, returnedcall);
    }

    [Fact]
    public async Task Delete_ShouldReturnCall()
    {
        // Arrange
        var call = new Call
        {
            Id = 1,
            CustomerId = "CUST123",
            AgentId = 1001,
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddMinutes(5),
            Status = CallStatus.Completed,
            Notes = "Test call"
        };

        var expectedCall = new Call
        {
            Id = 1,
            CustomerId = "CUST123",
            AgentId = 1001,
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddMinutes(5),
            Status = CallStatus.Completed,
            Notes = "Test call"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateCmd>(), default))
            .ReturnsAsync(expectedCall);

        // Act
        var resultAdd = await _controller.Create(call);

        // Arrange
        int CallIdRemove = 1;
        var expectedCallRemove = new Call
        {
            Id = 1,
            CustomerId = "CUST123",
            AgentId = 1001,
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddMinutes(5),
            Status = CallStatus.Completed,
            Notes = "Test call"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteCmd>(), default))
           .ReturnsAsync(expectedCallRemove);


        // Act
        var result = await _controller.Delete(CallIdRemove);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Call>>(result);
        var returnedCall = Assert.IsType<Call>(actionResult.Value);

        Assert.Equal(expectedCallRemove, returnedCall);
    }

    [Fact]
    public async Task ListBy_ShouldReturnCalls()
    {
        // Arrange
        int CallId = 1;
        var callsExpected = new List<Call>
        {
            new Call
            {
                Id = 1,
                CustomerId = "CUST123",
                AgentId = 1001,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(5),
                Status = CallStatus.Completed,
                Notes = "Test call"
            },

            new Call
            {
                Id = 2,
                CustomerId = "CUST1234",
                AgentId = 1001,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(5),
                Status = CallStatus.Completed,
                Notes = "Test call 4"
            }
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<ListByQry>(), default))
            .ReturnsAsync(callsExpected);

        // Act
        var result = await _controller.ListBy(CallId);

        // Assert
        var actionResult = Assert.IsType<List<Call>>(result);

        Assert.Equal(callsExpected.Count, actionResult.Count());
    }

    [Fact]
    public async Task ListAll_ShouldReturnCalls()
    {
        var callsExpected = new List<Call>
        {
            new Call
            {
                Id = 1,
                CustomerId = "CUST123",
                AgentId = 1001,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(5),
                Status = CallStatus.Completed,
                Notes = "Test call"
            },

            new Call
            {
                Id = 2,
                CustomerId = "CUST1234",
                AgentId = 1001,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(5),
                Status = CallStatus.Completed,
                Notes = "Test call 4"
            }
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<ListAllQry>(), default))
            .ReturnsAsync(callsExpected);

        // Act
        var result = await _controller.ListAll();

        // Assert
        var actionResult = Assert.IsType<List<Call>>(result);

        Assert.Equal(callsExpected.Count, actionResult.Count());

    }
}

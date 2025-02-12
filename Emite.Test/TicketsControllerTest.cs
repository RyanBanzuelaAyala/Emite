using Emite.Api.Controllers;
using Emite.Domain.Model.V1.TicketVM;
using Emite.Infrastructure.V1.TicketService.Command.Create;
using Emite.Infrastructure.V1.TicketService.Command.Delete;
using Emite.Infrastructure.V1.TicketService.Command.Update;
using Emite.Infrastructure.V1.TicketService.Query.ListAll;
using Emite.Infrastructure.V1.TicketService.Query.ListBy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace Emite.Api.Tickets.Test;

public class TicketsControllerTests
{

    private readonly Mock<IMemoryCache> _memoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly TicketsController _controller;

    public TicketsControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _memoryMock = SetupMemoryCache();
        _controller = new TicketsController(_mediatorMock.Object, _memoryMock.Object);
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
    public async Task Create_ShouldReturnTicket()
    {
        // Arrange
        var ticket = new Ticket
        {
            CustomerId = "C001",
            AgentId = 1001,
            Status = TicketStatus.Open,
            Priority = Priority.High,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Description = "Issue with product",
            Resolution = ""
        };

        var expectedTicket = new Ticket
        {
            Id = 1,
            CustomerId = "C001",
            AgentId = 1001,
            Status = TicketStatus.Open,
            Priority = Priority.High,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Description = "Issue with product",
            Resolution = ""
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateCmd>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedTicket);

        // Act
        var result = await _controller.Create(ticket);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Ticket>>(result);
        var returnedTicket = Assert.IsType<Ticket>(actionResult.Value);
        Assert.Equal(expectedTicket, returnedTicket);
    }

    [Fact]
    public async Task Update_ShouldReturnTicket()
    {
        // Arrange
        var ticket = new Ticket
        {
            Id = 1,
            CustomerId = "C001",
            AgentId = 1001,
            Status = TicketStatus.InProgress,
            Priority = Priority.High,
            CreatedAt = DateTime.Now.AddDays(-1),
            UpdatedAt = DateTime.Now,
            Description = "Issue with product - ongoing",
            Resolution = "Partially resolved"
        };

        var expectedTicket = new Ticket
        {
            Id = 1,
            CustomerId = "C001",
            AgentId = 1001,
            Status = TicketStatus.InProgress,
            Priority = Priority.High,
            CreatedAt = DateTime.Now.AddDays(-1),
            UpdatedAt = DateTime.Now,
            Description = "Issue with product - ongoing",
            Resolution = "Partially resolved"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateCmd>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedTicket);

        // Act
        var result = await _controller.Update(ticket);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Ticket>>(result);
        var returnedTicket = Assert.IsType<Ticket>(actionResult.Value);
        Assert.Equal(expectedTicket, returnedTicket);
    }

    [Fact]
    public async Task Delete_ShouldReturnTicket()
    {
        // Arrange
        var ticket = new Ticket
        {
            Id = 1,
            CustomerId = "C001",
            AgentId = 1001,
            Status = TicketStatus.Closed,
            Priority = Priority.High,
            CreatedAt = DateTime.Now.AddDays(-1),
            UpdatedAt = DateTime.Now,
            Description = "Issue with product",
            Resolution = "Resolved"
        };

        var expectedTicket = new Ticket
        {
            Id = 1,
            CustomerId = "C001",
            AgentId = 1001,
            Status = TicketStatus.Closed,
            Priority = Priority.High,
            CreatedAt = DateTime.Now.AddDays(-1),
            UpdatedAt = DateTime.Now,
            Description = "Issue with product",
            Resolution = "Resolved"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateCmd>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedTicket);

        // Act
        var resultAdd = await _controller.Create(ticket);

        // Arrange
        int ticketIdRemove = 1;
        var expectedTicketRemove = new Ticket
        {
            Id = 1,
            CustomerId = "C001",
            AgentId = 1001,
            Status = TicketStatus.Closed,
            Priority = Priority.High,
            CreatedAt = DateTime.Now.AddDays(-1),
            UpdatedAt = DateTime.Now,
            Description = "Issue with product",
            Resolution = "Resolved"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteCmd>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedTicketRemove);

        // Act
        var result = await _controller.Delete(ticketIdRemove);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Ticket>>(result);
        var returnedTicket = Assert.IsType<Ticket>(actionResult.Value);
        Assert.Equal(expectedTicketRemove, returnedTicket);
    }

    [Fact]
    public async Task ListBy_ShouldReturnTickets()
    {
        // Arrange
        int ticketId = 1;
        var ticketsExpected = new List<Ticket>
    {
        new Ticket
        {
            Id = 1,
            CustomerId = "C001",
            AgentId = 1001,
            Status = TicketStatus.Open,
            Priority = Priority.High,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Description = "Issue with product",
            Resolution = ""
        },
        new Ticket
        {
            Id = 2,
            CustomerId = "C002",
            AgentId = 1002,
            Status = TicketStatus.Resolved,
            Priority = Priority.Medium,
            CreatedAt = DateTime.Now.AddDays(-1),
            UpdatedAt = DateTime.Now,
            Description = "Issue resolved",
            Resolution = "Fixed the issue"
        }
    };

        _mediatorMock.Setup(m => m.Send(It.IsAny<ListByQry>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ticketsExpected);

        // Act
        var result = await _controller.ListBy(ticketId);

        // Assert
        var actionResult = Assert.IsType<List<Ticket>>(result);
        Assert.Equal(ticketsExpected.Count, actionResult.Count());
    }

    [Fact]
    public async Task ListAll_ShouldReturnTickets()
    {
        var ticketsExpected = new List<Ticket>
    {
        new Ticket
        {
            Id = 1,
            CustomerId = "C001",
            AgentId = 1001,
            Status = TicketStatus.Open,
            Priority = Priority.High,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Description = "Issue with product",
            Resolution = ""
        },
        new Ticket
        {
            Id = 2,
            CustomerId = "C002",
            AgentId = 1002,
            Status = TicketStatus.Resolved,
            Priority = Priority.Medium,
            CreatedAt = DateTime.Now.AddDays(-1),
            UpdatedAt = DateTime.Now,
            Description = "Issue resolved",
            Resolution = "Fixed the issue"
        }
    };

        _mediatorMock.Setup(m => m.Send(It.IsAny<ListAllQry>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ticketsExpected);

        // Act
        var result = await _controller.ListAll();

        // Assert
        var actionResult = Assert.IsType<List<Ticket>>(result);
        Assert.Equal(ticketsExpected.Count, actionResult.Count());
    }

}

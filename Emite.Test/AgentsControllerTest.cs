using Emite.Api.Controllers;
using Emite.Domain.Model.V1.AgentVM;
using Emite.Infrastructure.V1.AgentService.Command.Create;
using Emite.Infrastructure.V1.AgentService.Command.Delete;
using Emite.Infrastructure.V1.AgentService.Command.Update;
using Emite.Infrastructure.V1.AgentService.Command.UpdateStatus;
using Emite.Infrastructure.V1.AgentService.Query.ListAll;
using Emite.Infrastructure.V1.AgentService.Query.ListBy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace Emite.Api.Agents.Test;

public class AgentControllerTest
{

    private readonly Mock<IMemoryCache> _memoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly AgentsController _controller;

    public AgentControllerTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _memoryMock = SetupMemoryCache();
        _controller = new AgentsController(_mediatorMock.Object, _memoryMock.Object);
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
    public async Task Create_ReturnsAgent_WhenAgentIsCreatedSuccessfully()
    {
        // Arrange
        var Agent = new Agent { Name = "Ryan Ayala" };
        var expectedAgent = new Agent { Id = 1, Name = "Ryan Ayala" };

        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateCmd>(), default))
            .ReturnsAsync(expectedAgent);

        // Act
        var result = await _controller.Create(Agent);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Agent>>(result);
        var returnedAgent = Assert.IsType<Agent>(actionResult.Value);

        Assert.Equal(expectedAgent, returnedAgent);
    }

    [Fact]
    public async Task UpdateAgent_ReturnsAgent_WhenAgentIsUpdatedSuccessfully()
    {
        // Arrange
        var Agent = new Agent { Id = 1, Name = "Ryan Ayala" };

        var expectedAgent = new Agent { Id = 1, Name = "Ryan Ayala" };

        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateCmd>(), default))
                .ReturnsAsync(expectedAgent);

        // Act
        var result = await _controller.Update(Agent);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Agent>>(result);
        var returnedAgent = Assert.IsType<Agent>(actionResult.Value);

        Assert.Equal(expectedAgent, returnedAgent);
    }

    [Fact]
    public async Task UpdateStatusAgent_ReturnsAgent_WhenAgentStatusIsUpdatedSuccessfully()
    {
        // Arrange
        var Agent = new Agent { Id = 1, Name = "Ryan Ayala", Status = AgentStatus.Available };

        var expectedAgent = new Agent { Id = 1, Name = "Ryan Ayala", Status = AgentStatus.Available };

        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateStatusCmd>(), default))
                .ReturnsAsync(expectedAgent);

        // Act
        var result = await _controller.Update(Agent.Id, AgentStatus.Available);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Agent>>(result);
        var returnedAgent = Assert.IsType<Agent>(actionResult.Value);

        Assert.Equal(expectedAgent, returnedAgent);
    }

    [Fact]
    public async Task DeleteAgent_ReturnsAgentWithNoAgentId_WhenAgentIsDeletedSuccessfully()
    {
        // Arrange
        var Agent = new Agent { Id = 1, Name = "Ryan Ayala" };
        var expectedAgent = new Agent { Id = 1, Name = "Ryan Ayala" };

        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateCmd>(), default))
            .ReturnsAsync(expectedAgent);

        // Act
        var resultAdd = await _controller.Create(Agent);

        // Arrange
        int AgentIdRemove = 1;
        var expectedAgentRemove = new Agent { Id = 1, Name = "Ryan Ayala" };

        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteCmd>(), default))
            .ReturnsAsync(expectedAgentRemove);

        // Act
        var resultIfRemove = await _controller.Delete(AgentIdRemove);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Agent>>(resultIfRemove);
        var returnedAgent = Assert.IsType<Agent>(actionResult.Value);

        Assert.Equal(expectedAgentRemove, returnedAgent);
    }

    [Fact]
    public async Task ListAgentByAgent_ReturnsListOfAgentsAgented_WhenAgentsAreFound()
    {
        // Arrange
        int AgentId = 1; // Assuming this Agent ID is valid
        var expectedAgents = new List<Agent>
        {
            new Agent {  Id = 1, Name = "Ryan Ayala" },
            new Agent {  Id = 2, Name = "Ayala Ryan"}
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<ListByQry>(), default))
            .ReturnsAsync(expectedAgents);

        // Act
        var result = await _controller.ListBy(AgentId);

        // Assert
        var actionResult = Assert.IsType<List<Agent>>(result);

        Assert.Equal(expectedAgents.Count, actionResult.Count());
    }

    [Fact]
    public async Task ListAgentByGenre_ReturnsListOfAgentsGenre_WhenAgentsAreFound()
    {
        // Arrange
        var expectedAgents = new List<Agent>
        {
             new Agent {  Id = 1, Name = "Ryan Ayala" },
            new Agent {  Id = 2, Name = "Ayala Ryan"}
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<ListAllQry>(), default))
            .ReturnsAsync(expectedAgents);

        // Act
        var result = await _controller.ListAll();

        // Assert
        var actionResult = Assert.IsType<List<Agent>>(result);

        Assert.Equal(expectedAgents.Count, actionResult.Count());
    }
}
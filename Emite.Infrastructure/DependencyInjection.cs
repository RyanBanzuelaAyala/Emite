using Emite.Domain.Model.V1.AgentVM;
using Emite.Domain.Model.V1.CallVM;
using Emite.Domain.Model.V1.CustomerVM;
using Emite.Domain.Model.V1.TicketVM;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Emite.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection EmiteInfrastructure(this IServiceCollection services)
        {
            // Agent
            services.AddTransient<IRequestHandler<V1.AgentService.Command.Create.CreateCmd, Agent>, V1.AgentService.Command.Create.CreateCmdHandler>();
            services.AddTransient<IRequestHandler<V1.AgentService.Command.Delete.DeleteCmd, Agent>, V1.AgentService.Command.Delete.DeleteCmdHandler>();
            services.AddTransient<IRequestHandler<V1.AgentService.Command.Update.UpdateCmd, Agent>, V1.AgentService.Command.Update.UpdateCmdHandler>();
            services.AddTransient<IRequestHandler<V1.AgentService.Command.UpdateStatus.UpdateStatusCmd, Agent>, V1.AgentService.Command.UpdateStatus.UpdateStatusCmdHandler>();

            services.AddTransient<IRequestHandler<V1.AgentService.Query.ListBy.ListByQry, IEnumerable<Agent>>, V1.AgentService.Query.ListBy.ListByQryHandler>();
            services.AddTransient<IRequestHandler<V1.AgentService.Query.ListAll.ListAllQry, IEnumerable<Agent>>, V1.AgentService.Query.ListAll.ListAllQryHandler>();

            // Call
            services.AddTransient<IRequestHandler<V1.CallService.Command.Create.CreateCmd, Call>, V1.CallService.Command.Create.CreateCmdHandler>();
            services.AddTransient<IRequestHandler<V1.CallService.Command.Delete.DeleteCmd, Call>, V1.CallService.Command.Delete.DeleteCmdHandler>();
            services.AddTransient<IRequestHandler<V1.CallService.Command.Update.UpdateCmd, Call>, V1.CallService.Command.Update.UpdateCmdHandler>();
            services.AddTransient<IRequestHandler<V1.CallService.Command.Assign.AssignCmd, Call>, V1.CallService.Command.Assign.AssignCmdHandler>();

            services.AddTransient<IRequestHandler<V1.CallService.Query.ListBy.ListByQry, IEnumerable<Call>>, V1.CallService.Query.ListBy.ListByQryHandler>();
            services.AddTransient<IRequestHandler<V1.CallService.Query.ListAll.ListAllQry, IEnumerable<Call>>, V1.CallService.Query.ListAll.ListAllQryHandler>();

            // Customer
            services.AddTransient<IRequestHandler<V1.CustomerService.Command.Create.CreateCmd, Customer>, V1.CustomerService.Command.Create.CreateCmdHandler>();
            services.AddTransient<IRequestHandler<V1.CustomerService.Command.Delete.DeleteCmd, Customer>, V1.CustomerService.Command.Delete.DeleteCmdHandler>();
            services.AddTransient<IRequestHandler<V1.CustomerService.Command.Update.UpdateCmd, Customer>, V1.CustomerService.Command.Update.UpdateCmdHandler>();

            services.AddTransient<IRequestHandler<V1.CustomerService.Query.ListBy.ListByQry, IEnumerable<Customer>>, V1.CustomerService.Query.ListBy.ListByQryHandler>();
            services.AddTransient<IRequestHandler<V1.CustomerService.Query.ListAll.ListAllQry, IEnumerable<Customer>>, V1.CustomerService.Query.ListAll.ListAllQryHandler>();

            // Ticket
            services.AddTransient<IRequestHandler<V1.TicketService.Command.Create.CreateCmd, Ticket>, V1.TicketService.Command.Create.CreateCmdHandler>();
            services.AddTransient<IRequestHandler<V1.TicketService.Command.Delete.DeleteCmd, Ticket>, V1.TicketService.Command.Delete.DeleteCmdHandler>();
            services.AddTransient<IRequestHandler<V1.TicketService.Command.Update.UpdateCmd, Ticket>, V1.TicketService.Command.Update.UpdateCmdHandler>();
            services.AddTransient<IRequestHandler<V1.TicketService.Command.Assign.AssignCmd, Ticket>, V1.TicketService.Command.Assign.AssignCmdHandler>();

            services.AddTransient<IRequestHandler<V1.TicketService.Query.ListBy.ListByQry, IEnumerable<Ticket>>, V1.TicketService.Query.ListBy.ListByQryHandler>();
            services.AddTransient<IRequestHandler<V1.TicketService.Query.ListAll.ListAllQry, IEnumerable<Ticket>>, V1.TicketService.Query.ListAll.ListAllQryHandler>();


            return services;
        }
    }
}

using Emite.Domain.Model.V1.CallVM;
using MediatR;

namespace Emite.Infrastructure.V1.CallService.Command.Delete
{
    public class DeleteCmd : IRequest<Call>
    {
        public int Id { get; set; }
    }
}
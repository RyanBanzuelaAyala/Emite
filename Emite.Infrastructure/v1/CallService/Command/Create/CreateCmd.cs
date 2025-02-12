using Emite.Domain.Model.V1.CallVM;
using MediatR;

namespace Emite.Infrastructure.V1.CallService.Command.Create
{
    public class CreateCmd : IRequest<Call>
    {
        public Call entity { get; set; }
    }
}
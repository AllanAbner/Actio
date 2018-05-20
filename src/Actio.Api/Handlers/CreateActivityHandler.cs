using System;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;

namespace Actio.Api.Handlers
{
    public class CreateActivityHandler : IEventHandler<ActivityCreated>
    {
        public async Task HandleAsync(ActivityCreated @event)
        {
            await Task.CompletedTask;
            Console.WriteLine($"Activity created: {@event.Name}");
        }
    }
}
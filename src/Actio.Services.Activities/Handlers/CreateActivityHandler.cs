using Actio.Common.Commands;
using Actio.Common.Events;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient busClient;

        public CreateActivityHandler(IBusClient busClient)
        {
            this.busClient = busClient;
        }

        public async Task HandleAsync(CreateActivity Command)
        {
            Console.Write($"Creating activity: {Command.Name} at {Command.CreatedAt}");

            await busClient.PublishAsync(new ActivityCreated(Command.Id, Command.UserId, Command.Category, Command.Name, Command.Description,
            Command.CreatedAt));
        }
    }
}
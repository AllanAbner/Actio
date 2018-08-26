using Actio.Common.Commands;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IBusClient busClient;

        public CreateUserHandler(IBusClient busClient)
        {
            this.busClient = busClient;
        }

        public async Task HandleAsync(CreateUser command)
        {
            Console.Write($"Creating activity: {command.Name}");

            await busClient.PublishAsync(new CreateUser
            {
                Email = command.Name,
                Name = command.Name,
                Password = command.Password
            });
        }
    }
}
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exceptions;
using Actio.Services.Identity.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IBusClient busClient;
        private readonly IUserService userService;
      //  private readonly ILogger logger;

        public CreateUserHandler(IBusClient busClient, IUserService userService /*ILogger<CreateUserHandler> logger*/)
        {
            this.busClient = busClient;
            this.userService = userService;
            //logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task HandleAsync(CreateUser command)
        {
           // logger.LogInformation($"creating User User: {command.Name} at {DateTime.Now.ToUniversalTime()}");
            try
            {
                await userService.RegisterAsync(command.Email, command.Password, command.Name);
                await busClient.PublishAsync(new UserCreated(command.Email, command.Name));
               

                await Task.CompletedTask;
            }
            catch (ActioException ex)
            {
                await busClient
                    .PublishAsync(new CreateUserRejected(command.Email,
                        ex.Code, ex.Message));
            }
            catch (Exception ex)
            {
                await busClient.PublishAsync(new CreateUserRejected(command.Email,
                    "Error", ex.Message));
                //logger.LogError(ex.Message);
            }

           
        }
    }
}
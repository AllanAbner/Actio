using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exceptions;
using Actio.Services.Activities.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient busClient;
        private readonly IActivityService activityService;
        private ILogger logger;

        public CreateActivityHandler(IBusClient busClient,
            IActivityService activityService, ILogger<CreateActivityHandler> logger)
        {
            this.busClient = busClient;
            this.activityService = activityService;
            this.logger = logger;
        }

        public async Task HandleAsync(CreateActivity Command)
        {
            logger.LogInformation($"Creating activity: {Command.Name} at {Command.CreatedAt}");
            try
            {
                await activityService.AddAsync(Command.Id, Command.UserId,
                    Command.Category, Command.Name, Command.Description, Command.CreatedAt);

                await busClient.PublishAsync(new ActivityCreated(Command.Id, Command.UserId,
                    Command.Category, Command.Name, Command.Description,
           Command.CreatedAt));
                return;
            }
            catch (ActioException ex)
            {
                await busClient
                    .PublishAsync(new CreateActivityRejected(Command.Id,
                     ex.Code, ex.Message));
            }
            catch (Exception ex)
            {
                await busClient.PublishAsync(new CreateActivityRejected(Command.Id,
                   "Error", ex.Message));
                logger.LogError(ex.Message);
            }
        }
    }
}
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

        public async Task HandleAsync(CreateActivity command)
        {
            logger.LogInformation($"Creating activity: {command.Name} at {command.CreatedAt}");
            try
            {
                await activityService.AddAsync(command.Id, command.UserId,
                    command.Category, command.Name, command.Description, command.CreatedAt);

                await busClient.PublishAsync(new ActivityCreated(command.Id, command.UserId,
                    command.Category, command.Name, command.Description,
                    command.CreatedAt));

                await Task.CompletedTask;
            }
            catch (ActioException ex)
            {
                await busClient
                    .PublishAsync(new CreateActivityRejected(command.Id,
                        ex.Code, ex.Message));
            }
            catch (Exception ex)
            {
                await busClient.PublishAsync(new CreateActivityRejected(command.Id,
                    "Error", ex.Message));
                logger.LogError(ex.Message);
            }
        }
    }
}
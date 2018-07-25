using System;
using System.Threading.Tasks;
using Actio.Common.Exceptions;
using Actio.Services.Activities.Domain.Repositories;

namespace Actio.Services.Activities.Services
{
    public class ActivityService : IActivityService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IActivityRepository activityRepository;

        public ActivityService(IActivityRepository activityRepository,
            ICategoryRepository categoryRepository)
        {
            this.activityRepository = activityRepository ??
                throw new ArgumentNullException(nameof(activityRepository));
            this.categoryRepository = categoryRepository ??
                throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task AddAsync(Guid id, Guid userId, string category, string name, string description, DateTime createdAt)
        {
            var activityCategory = await categoryRepository.GetAsync(category);
            if (activityCategory == null)
            {
                throw new ActioException("category_not_found",
                    $"Category: '{category}' was not found.");
            }
            var activity = new Domain.Models.Activity(id, activityCategory, userId,
                name, description, createdAt);
            await activityRepository.AddAsync(activity);
        }
    }
}
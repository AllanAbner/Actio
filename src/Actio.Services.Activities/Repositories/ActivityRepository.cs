using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.Services.Activities.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase database;
        private IMongoCollection<Activity> Collection => database.GetCollection<Activity>(nameof(Activities));

        public async Task<Activity> GetAsync(Guid id) => await Collection.AsQueryable()
            .FirstOrDefaultAsync(x => x.Id == id);

        public ActivityRepository(IMongoDatabase database)
        {
            this.database = database;
        }
        public async Task AddAsync(Activity activity) => await Collection.InsertOneAsync(activity);

        public async Task<IEnumerable<Activity>> BrowseAsync() => await Collection
            .AsQueryable()
            .ToListAsync();
    }
}
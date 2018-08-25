using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Common.Mongo
{
    public class MongoSeeder : IDatabaseSeeder
    {
        protected readonly IMongoDatabase database;

        public MongoSeeder(IMongoDatabase database)
        {
            this.database = database;
        }

        public async Task SeedAsync()
        {
            var collectionCursor = await database.ListCollectionsAsync();
            var collections = await collectionCursor.ToListAsync();

            if (collections.Any())
            {
                return;
            }
            await CustomSeedAsync();
        }

        protected virtual async Task CustomSeedAsync()
        {
            await Task.CompletedTask;
        }
    }
}
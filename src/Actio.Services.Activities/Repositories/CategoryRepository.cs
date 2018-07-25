using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.Services.Activities.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoDatabase database;

        public CategoryRepository(IMongoDatabase database)
        {
            this.database = database;
        }

        public async Task<Category> GetAsync(string name) => await Collection
            .AsQueryable()
            .FirstOrDefaultAsync(x => x.Name.Equals(name.ToLowerInvariant()));
        public async Task AddAsync(Category category) => await Collection.InsertOneAsync(category);

        public async Task<IEnumerable<Category>> BrowseAsync() => await Collection
            .AsQueryable()
            .ToListAsync();

        private IMongoCollection<Category> Collection => database.GetCollection<Category>("Categories");
    }
}
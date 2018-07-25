using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Mongo;
using Actio.Services.Activities.Domain.Repositories;
using MongoDB.Driver;

namespace Actio.Services.Activities.Services
{
    public class CustomMongoSeeder : MongoSeeder
    {
        private readonly ICategoryRepository categoryRepository;

        public CustomMongoSeeder(IMongoDatabase database,
            ICategoryRepository categoryRepository) : base(database)
        {
            this.categoryRepository = categoryRepository;
        }
        protected override async Task CustomSeedAsync()
        {
            var categories = new List<string>
            {
                "work",
                "sport",
                "hobby"
            };
            await Task.WhenAll(categories.Select(x =>
                categoryRepository.AddAsync(new Domain.Models.Category(x))));
        }
    }
}
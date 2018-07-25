using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Actio.Common.Mongo
{
    public class MongoInitializer : IDatabaseInitializer
    {

        private bool _initialized;
        private readonly bool _seed;

        private readonly IMongoDatabase database;

        private IDatabaseSeeder Seeder { get; set; }

        public MongoInitializer(IMongoDatabase database,
            IDatabaseSeeder seeder,
            IOptions<MongoOptions> options)
        {
            this.database = database;
            Seeder = seeder;
            _seed = options.Value.Seed;
            this.Seeder = seeder ??
                throw new ArgumentNullException(nameof(seeder));
        }

        public async Task InitializeAsync()
        {
            if (_initialized)
            {
                return;
            }
            RegisterConventions();
            _initialized = true;
            if (!_seed)
            {
                return;
            }
            await Seeder.SeedAsync();
        }

        private void RegisterConventions()
        {
            ConventionRegistry.Register("ActioConventions", new MongoConvention(),
                x => true);
        }

        private class MongoConvention : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>()
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(MongoDB.Bson.BsonType.String),
                new CamelCaseElementNameConvention()

            };
        }
    }
}
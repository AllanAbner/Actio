using Actio.Common.Exceptions;
using System;

namespace Actio.Services.Activities.Domain.Models
{
    public class Activity
    {
        public Guid Id { get; protected set; }
        public string Name { get; private set; }
        public string Category { get; protected set; }
        public string Description { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected Activity()
        { }

        public Activity(Guid id, Category category, Guid userId,
            string name, string description, DateTime createdAt)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ActioException("empty_activity_name",
                    "Activity name can not be empty.");
            }
            Id = id;
            Category = category.Name;
            UserId = userId;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
        }
    }
}
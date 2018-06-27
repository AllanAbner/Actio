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
        {
        }

        public Activity(Guid id, string name, string category, string description, Guid userId, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Category = category;
            Description = description;
            UserId = userId;
            CreatedAt = createdAt;
        }
    }
}
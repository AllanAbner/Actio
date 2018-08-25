using Actio.Common.Exceptions;
using System;

namespace Actio.Services.Identity.Domain.Models
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected User()
        {
        }

        public User(string email, string name)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ActioException("empty_User_email",
                    "User email can not be empty.");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ActioException("empty_User_name",
                    "User name can not be empty.");
            }
            Email = email;
            Name = name;
        }
    }
}
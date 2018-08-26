using Actio.Common.Exceptions;
using Actio.Services.Identity.Domain.Services;
using System;

namespace Actio.Services.Identity.Domain.Models
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Name { get; private set; }
        public string Salt { get; protected set; }
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
        public void SetPassword(string password, IEncrypter encrypter)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ActioException("empty_password",
                    "Password can not be empty.");
            }
            Salt = encrypter.GetSalt();
            Password = encrypter.GetHash(password, Salt);
        }

        public bool ValidatePassword(string password, IEncrypter encrypter)
            => Password.Equals(encrypter.GetHash(password, Salt));
    }
}
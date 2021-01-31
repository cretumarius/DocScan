﻿using System;
using Domain.Base.EntityBase;

namespace Domain.Users
{
    public class User : EntityBase<Guid>
    {
        private User() { }

        private User(string Email, string FirstName, string LastName, string PasswordHash)
        {
            Id = Guid.NewGuid();
            this.Email = Email;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.PasswordHash = PasswordHash;
        }

        public string Email { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string PasswordHash { get; private set; }

        public static User CreateUser(string email, string firstName, string lastName, string passwordHash)
        {
            return new User(email, firstName, lastName, passwordHash);
        }
    }
}

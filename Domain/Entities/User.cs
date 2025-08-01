﻿#nullable disable

using System.Data;
using ClassRegistrationApplication2025.Domain.Enums;

namespace ClassRegistrationApplication2025.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserID { get; set; } //get from AD
        public string Name { get; set; }
        public string EmailSMTP { get; set; }
        public Role Role { get; set; } = Role.User;
        public List<Registration> Registrations { get; set; } = new();
        public List<Class> ClassesCreated { get; set; } = new();

    }
}

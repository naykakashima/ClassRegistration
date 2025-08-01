﻿using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly AppDbContext _db;

        public RegistrationRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<int> GetRegistrationCountByClassAsync(Guid classId)
        {
            return await _db.Registrations.CountAsync(r => r.ClassId == classId);
        }

        public async Task<bool> ExistsAsync(Guid classId, Guid userId)
        {
            return await _db.Registrations.AnyAsync(r => r.ClassId == classId && r.UserId == userId);
        }

        public async Task RegisterUserAsync(Guid userId, Guid classId, string Name, string EmailSMTP, CancellationToken ct)
        {
            var registration = new Registration
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ClassId = classId,
                UserName = Name,
                EmailSMTP = EmailSMTP,
                RegisteredAt = DateTime.UtcNow
            };

            _db.Registrations.Add(registration);
            await _db.SaveChangesAsync(ct);
        }

        public async Task UnregisterUserAsync(Guid userId, Guid classId, CancellationToken ct)
        {
            var registration = await _db.Registrations
                .FirstOrDefaultAsync(r => r.UserId == userId && r.ClassId == classId, ct);

            if (registration == null)
                return; // Or throw an exception depending on your business logic

            _db.Registrations.Remove(registration);
            await _db.SaveChangesAsync(ct);
        }

        public async Task<List<Guid>> GetClassIdsByUserAsync(Guid userId)
        {
            return await _db.Registrations
                .Where(r => r.UserId == userId)
                .Select(r => r.ClassId)
                .ToListAsync();
        }

        public async Task<List<Registration>> GetRegistrationsByClassAsync(Guid classId)
        {
            return await _db.Registrations
                            .Include(r => r.User)
                            .Where(r => r.ClassId == classId)
                            .ToListAsync();
        }

        public async Task UpdateAttendanceAsync(List<AttendanceUpdateDto> updates, CancellationToken ct)
        {
            foreach (var update in updates)
            {
                await _db.Registrations
                    .Where(r => r.Id == update.RegistrationId)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(r => r.AttendedAt, update.AttendedAt),
                    ct);
            }
        }


    }
}

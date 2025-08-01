﻿using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using System.Net.Mail;
using System.Text;

namespace ClassRegistrationApplication2025.Infrastructure.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<SmtpEmailService> _logger;

        public SmtpEmailService(IConfiguration config, ILogger<SmtpEmailService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task SendRegistrationConfirmationAsync(UserDto user, ClassDetailDto classInfo)
        {
            var smtpSection = _config.GetSection("Smtp");
            var smtpHost = smtpSection["Host"];
            var smtpPort = int.Parse(smtpSection["Port"]);
            var fromEmail = smtpSection["From"];

            var mail = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = $"Class Registration Confirmed: {classInfo.ClassName}",
                IsBodyHtml = true,
                Body = GenerateHtmlBody(user, classInfo)
            };
            mail.To.Add(user.EmailSMTP);

            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                UseDefaultCredentials = true,
                EnableSsl = false
            };

            try
            {
                await client.SendMailAsync(mail);
                _logger.LogInformation("Registration email sent to {Email}", user.EmailSMTP);
            }
            catch (SmtpException ex)
            {
                _logger.LogError(ex, "Failed to send email to {Email}", user.EmailSMTP);
            }
        }

        private string GenerateHtmlBody(UserDto user, ClassDetailDto classInfo)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"<h2>Hello {user.Name},</h2>");
            sb.AppendLine($"<p>You have successfully registered for the class <strong>{classInfo.ClassName}</strong>.</p>");
            sb.AppendLine($"<p><strong>Session:</strong> {classInfo.SessionName}<br>");
            sb.AppendLine($"<strong>Date:</strong> {classInfo.Date.ToShortDateString()}<br>");
            sb.AppendLine($"<strong>Time:</strong> {classInfo.StartTime:hh\\:mm} - {classInfo.EndTime:hh\\:mm}<br>");
            sb.AppendLine($"<strong>Presenter:</strong> {classInfo.Presenter}</p>");
            sb.AppendLine("<p>Thank you.</p>");
            return sb.ToString();
        }
    }
}

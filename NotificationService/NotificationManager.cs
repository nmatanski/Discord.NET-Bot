﻿using NotificationService.Domain;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using UserService.Domain.Models;

namespace NotificationService
{
    public class NotificationManager : INotificationActor
    {
        //private ILog _logger = Logger.GetInstance;
        private string confirmationEmailUrl = "http://localhost:55766/account/ValidateEmail";
        private string passwordResetUrl = "http://localhost:44396/account/PasswordReset";


        public async Task SendChangePasswordEmailAsync(User user)
        {
            string callbackUrl = $"{passwordResetUrl}?userId={user.Id}&validationCode={user.ValidationCode}";
            string link = $"<a href='{ callbackUrl}'>here</a>!";
            await SendEmailAsync(user.Email, "ITGigs change password request", $"Please reset your password by clicking {link}");
        }

        public async Task SendConfirmationEmailAsync(User user)
        {
            string callbackUrl = $"{confirmationEmailUrl}?userId={user.Id}&validationCode={user.ValidationCode}";
            string link = $"<a href='{ callbackUrl}'>here</a>!";
            await SendEmailAsync(user.Email, "ITGigs registration request", $"To confirm your account click  -> {link}");
        }

        private async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("", "") ///TODO: Outlook credentials here
                }; ///TODO

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("whoever@me.com")
                };
                mailMessage.To.Add(email);
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = subject;
                client.EnableSsl = true;
                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                //await _logger.LogCustomExceptionAsync(ex, null); //
                throw new ApplicationException($"Unable to load : '{ex.Message}'.");
            }
        }
    }
}

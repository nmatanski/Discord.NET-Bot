using System;
using System.Threading.Tasks;
using UserService.Domain.Models;

namespace NotificationService.Domain
{
    public interface INotificationActor
    {
        Task SendConfirmationEmailAsync(User user);
        Task SendChangePasswordEmailAsync(User user);
    }
}

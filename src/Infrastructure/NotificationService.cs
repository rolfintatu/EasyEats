using AppCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(string message)
        {
            return Task.CompletedTask;
        }
    }
}

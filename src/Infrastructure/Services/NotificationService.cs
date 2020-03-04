using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger logger;

        public NotificationService(ILogger<NotificationService> logger)
        {
            this.logger = logger;
        }

        public Task SendAsync(string message)
        {
            logger.LogInformation(message);
            return Task.CompletedTask;
        }
    }
}

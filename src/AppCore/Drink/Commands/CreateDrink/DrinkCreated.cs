using AppCore.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppCore.Drink.Commands.CreateDrink
{
    public class DrinkCreated : INotification
    {
        public string DrinkName { get; set; }

        public class DrinkCreatedHandler : INotificationHandler<DrinkCreated>
        {
            private readonly INotificationService notificationService;

            public DrinkCreatedHandler(INotificationService notificationService)
            {
                this.notificationService = notificationService;
            }

            public async Task Handle(DrinkCreated notification, CancellationToken cancellationToken)
            {
                await notificationService.SendAsync($"A drink with id:{notification.DrinkName} was added to batabase.");
            }
        }
    }
}

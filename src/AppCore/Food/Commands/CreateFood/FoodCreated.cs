using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AppCore.Interfaces;

namespace AppCore.Food.Commands.CreateFood
{
    public class FoodCreated : INotification
    {
        public int FoodId { get; set; }

        public class FoodCreatedHandler : INotificationHandler<FoodCreated>
        {
            private readonly INotificationService notificationService;

            public FoodCreatedHandler(INotificationService notificationService)
            {
                this.notificationService = notificationService;
            }

            public async Task Handle(FoodCreated notification, CancellationToken cancellationToken)
            {
                await notificationService.SendAsync("This is a message!");
            }
        }
    }
}

using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Table.Commands.CreateTable
{
    public class TableCreated : INotification
    {
    }

    public class TableCreatedHandler : INotificationHandler<TableCreated>
    {
        private readonly INotificationService notificationService;

        public TableCreatedHandler(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public async Task Handle(TableCreated notification, CancellationToken cancellationToken)
        {

            await notificationService.SendAsync("A table was added to the database.");

        }
    }
}

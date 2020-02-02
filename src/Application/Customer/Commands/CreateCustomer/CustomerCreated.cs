using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Customer.Commands.CreateCustomer
{
    public class CustomerCreated : INotification
    {
        public  string Id { get; set; }
        public  string Name { get; set; }

        public int DbResponse { get; set; }
    }

    public class CustomerCreatedHandler : INotificationHandler<CustomerCreated>
    {
        private readonly INotificationService notificationService;

        public CustomerCreatedHandler(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public async Task Handle(CustomerCreated notification, CancellationToken cancellationToken)
        {
            await notificationService.SendAsync($"A new Customer with id: {notification.Id} " +
                $"and name: {notification.Name} was added to the database with code: {notification.DbResponse}");
        }
    }
}

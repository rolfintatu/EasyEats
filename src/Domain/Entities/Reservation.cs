using Domain.ValueObjects;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Reservation
    {

        public Reservation()
        {

        }

        public Reservation(Date date
            , int hour
            , string customerId
            , int tableId
            , int duration)
        {
            this.Duration = duration;
            this.CustomerId = customerId;
            this.TableId = tableId;
            this.Date = date;
            this.Hour = hour;
        }

        public const int DefaultDuration = 60;

        public int Id { get; private set; }
        public Date Date { get; private set; }
        public int Hour { get; private set; }
        public int Duration { get; private set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Waiting;

        public string CustomerId { get; private set; }
        public Customer Customer { get; set; }

        public int? TableId { get; private set; }
        public Table Table { get; set; }

        //public int OrderId { get; set; }
        public string OrderId { get; private set; }
        public Order Order { get; set; }

        //public string WaiterId { get; set; }

        public void CancelReservation()
        {
            this.Status = ReservationStatus.Canceled;
            this.TableId = null;
        }

        public void SetOrderId(string id)
        {
            this.OrderId = id;
        }

        //public Reservation CreateOrder(Order order
        //    ,List<Product> products)
        //{
        //    this.Order = order;
        //    this.Order.OrderDetails = new OrderDetails();
        //    this.Order.OrderDetails.Products = products;
        //    return this;
        //}

    }
}

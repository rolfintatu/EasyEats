using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Application.Reservation.Commands.CreateReservation
{
    public class CreateReservationValid : AbstractValidator<CreateReservationCom>
    {
        public CreateReservationValid()
        {
            RuleFor(x => x.Date.Day)
                .Must(ChackDay);

            RuleFor(x => x.Date.Month)
                .Must(CheckMouth);

            RuleFor(x => x.Hour)
                .Must((x) =>
                {
                    if (x > 24 || x < 8)
                    {
                        return false;
                    }
                    return true;
                });

            RuleFor(x => x.Duration)
                .GreaterThan(60);
        }

        private bool ChackDay(int day)
        {

            var thisMouthDays = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            if (day <= 0 || day > thisMouthDays)
            {
                return false;
            }

            return true;

        }

        private bool CheckMouth(int mouth)
        {
            var thisMouth = DateTime.Now.Month;

            if (mouth < thisMouth)
            {
                return false;
            }

            return true;
        }
    }
}

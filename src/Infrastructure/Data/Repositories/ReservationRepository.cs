using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Aggregates.ScheduleAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly EasyEatsDbContext _context;

        public ReservationRepository(EasyEatsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CancelReservation(Guid reservationId)
        {
            try
            {
                var reservation = await _context.Reservations.FindAsync(reservationId);

                if(reservation != null)
                {
                    reservation.Stage = Domain.Enums.ReservationStatus.Canceled;
                    _context.Reservations.Update(reservation);
                    await _context.SaveChangesAsync(new CancellationToken());
                }

                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CreateAsync(Reservation obj)
        {
            try
            {
                if (obj is null)
                    return false;

                await _context.Reservations.AddAsync(obj);
                await _context.SaveChangesAsync(new CancellationToken());
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<Reservation> GetById(Guid Id)
        {
            return await _context.Reservations.FindAsync(Id);
        }

        public Task<Reservation> GetReservationById(Guid reservationId)
        {
            var reservationDetails = _context.Reservations
                .Where(x => x.Id == reservationId)
                .FirstOrDefault();

            return Task.FromResult(reservationDetails);
        }
    }
}

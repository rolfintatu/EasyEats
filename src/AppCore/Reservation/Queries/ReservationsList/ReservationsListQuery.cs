using AppCore.Dtos;
using AppCore.Interfaces;
using AppCore.Reservation.Queries.ReservationDetails;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppCore.Reservation.Queries.ReservationsList
{
    public class ReservationsListQuery : IRequest<ReservationsList>
    {

    }

    public class ReservationsListHandler : IRequestHandler<ReservationsListQuery, ReservationsList>
    {
        private readonly IEasyEatsDbContext context;
        private readonly IMapper mapper;

        public ReservationsListHandler(IEasyEatsDbContext context
            ,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ReservationsList> Handle(ReservationsListQuery request, CancellationToken cancellationToken)
        {
            var reservationList = await context.Reservations
                .AsNoTracking()
                .Include(x => x.Customer)
                .Include(x => x.Table)
                .ProjectTo<ComplexReservationDto>(mapper.ConfigurationProvider)
                .ToListAsync();


            return new ReservationsList() { Reservations = reservationList };

        }
    }
}

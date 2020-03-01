using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Reservation.Queries.ReservationsByUser
{
    public class ResByUserCom : IRequest<List<MixReservationDto>>
    {
    }

    public class ResByUserHandler : IRequestHandler<ResByUserCom, List<MixReservationDto>>
    {
        private readonly IEasyEatsDbContext context;
        private readonly IMapper mapper;
        private readonly ICurrentUserService userService;

        public ResByUserHandler(IEasyEatsDbContext context
            ,IMapper mapper
            ,ICurrentUserService userService)
        {
            this.context = context;
            this.mapper = mapper;
            this.userService = userService;
        }

        public async Task<List<MixReservationDto>> Handle(ResByUserCom request, CancellationToken cancellationToken)
        {
            var reservationsList = await context.Reservations
                .AsNoTracking()
                .Where(x => x.CustomerId == userService.UserId && x.Status != Domain.Enums.ReservationStatus.Canceled)
                .ProjectTo<MixReservationDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return reservationsList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Entities = Domain.Entities;

namespace Application.Table.Queries.TableAvailability
{
    public class AvailabilityHandler : IRequestHandler<AvailabilityRequest, AvailabilityResponse>
    {

        private readonly IEasyEatsDbContext context;
        //TODO: Add program for each day
        private List<int> program = new List<int> { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };

        public AvailabilityHandler(IEasyEatsDbContext context)
        {
            this.context = context;
        }

        public async Task<AvailabilityResponse> Handle(AvailabilityRequest request, CancellationToken cancellationToken)
        {
            var reservations = await context.Reservations
                .AsNoTracking()
                .Where(x => x.TableId == request.TableId && x.Date.Day == request.Date.Day && x.Date.Month == request.Date.Month
                && x.Date.Year == request.Date.Year).ToDictionaryAsync(x => x.Hour, x => (x.Hour + x.Duration / 60));

            var results = new AvailabilityResponse();

            var hours = (int)request.Duration / 60;
            int i = 0;
            while (i <= program.Count - (hours + 1))
            {
                if (reservations.ContainsKey(program[i]))
                {
                    reservations.TryGetValue(program[i], out int value);
                    i += value - program[i];
                }
                else if (!inRange(reservations, program[i], hours))
                {
                    results.availableIntervals.Add(program[i], program[i + hours]);
                    i++;
                }
                else { i++; }
            }

            return results;
        }

        private bool inRange(Dictionary<int, int> reservations, int startHour, int hours)
        {
            bool exist = false;
            for (int i = startHour; i < startHour + hours; i++)
            {
                if (reservations.ContainsKey(i))
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }
    }
}

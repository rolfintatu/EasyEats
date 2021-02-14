using Application.Common.Interfaces;
using Domain.Aggregates.ScheduleAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly EasyEatsDbContext _context;

        public ScheduleRepository(EasyEatsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateAsync(Schedule obj)
        {
            if (_context.Schedules.Any(x => x.Month == obj.Month))
                return false;

            await _context.Schedules.AddAsync(obj);
            await _context.SaveChangesAsync(new CancellationToken());

            return true;
        }

        public async Task<Schedule> GetById(Guid Id)
        {
            if (Id != default(Guid))
                return await _context.Schedules.FindAsync(Id);
            else
                throw new ArgumentException();
        }

        public Task<Guid> GetScheduleIdByDate(DateTime date)
        {
            var schedule = _context.Schedules.AsNoTracking().Where(x =>
                    x.Month.MonthNumber == date.Month &&
                    x.Month.Year == date.Year
                ).FirstOrDefault();

            if (schedule != null)
                return Task.FromResult(schedule.Id);
            else
                return Task.FromResult(Guid.Empty);
        }

        public Task<bool> ScheduleExist(DateTime date)
        {
            var exist = _context.Schedules.Where(x => 
                    x.Month.MonthNumber == date.Month && 
                    x.Month.Year == date.Year
                ).FirstOrDefault();

            if (exist is null)
                return Task.FromResult(false);
            else
                return Task.FromResult(true);
        }
    }
}

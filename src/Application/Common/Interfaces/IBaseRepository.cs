using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> GetById(Guid Id);
        Task<bool> CreateAsync(T obj);
    }
}

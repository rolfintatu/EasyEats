using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(string message);
    }
}

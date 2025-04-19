using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_APP.Application.Interfaces
{
    public interface INotificationService
    {
        Task NotifyStudentsAsync(string message); // already exists
        Task NotifyStudentAsync(Guid studentId, string message); // new method
    }
}

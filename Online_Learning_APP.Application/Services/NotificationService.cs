using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Online_Learning_App.Infrastructure;
using Online_Learning_APP.Application.Interfaces;

namespace Online_Learning_APP.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyStudentsAsync(string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);
        }

        public async Task NotifyStudentAsync(Guid studentId, string message)
        {
            await _hubContext.Clients.User(studentId.ToString())
                             .SendAsync("ReceiveNotification", message);
        }
    }


}

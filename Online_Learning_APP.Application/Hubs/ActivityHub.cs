using Microsoft.AspNetCore.SignalR;
using MediatR;
using Online_Learning_APP.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Online_Learning_APP.Application.Handler;

public class ActivityHub : Hub
{
    private readonly IMediator _mediator;

    public ActivityHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    // Optional: Send full list when a user connects
    public override async Task OnConnectedAsync()
    {
        var activities = await _mediator.Send(new GetAllActivitiesQuery());
        await Clients.Caller.SendAsync("ReceiveActivitiesList", activities);

        await base.OnConnectedAsync();
    }

    // Or: On client-side request
    public async Task RequestActivities()
    {
        var activities = await _mediator.Send(new GetAllActivitiesQuery());
        await Clients.Caller.SendAsync("ReceiveActivitiesList", activities);
    }

    //public override async Task OnConnectedAsync()
    //{
    //    var httpContext = Context.GetHttpContext();

    //    // Get studentId and classGroupId from query string
    //    var studentId = httpContext?.Request.Query["studentId"];
    //    var classGroupId = httpContext?.Request.Query["classGroupId"];

    //    // Join student group (if available)
    //    if (!string.IsNullOrWhiteSpace(studentId))
    //    {
    //        await Groups.AddToGroupAsync(Context.ConnectionId, $"student-{studentId}");
    //    }

    //    // Join class group (if available)
    //    if (!string.IsNullOrWhiteSpace(classGroupId))
    //    {
    //        await Groups.AddToGroupAsync(Context.ConnectionId, $"classgroup-{classGroupId}");
    //    }

    //    // Optional: send full activity list to the connecting client
    //    var activities = await _mediator.Send(new GetAllActivitiesQuery());
    //    await Clients.Caller.SendAsync("ReceiveActivitiesList", activities);

    //    await base.OnConnectedAsync();
    //}

    //public async Task RequestActivities()
    //{
    //    var activities = await _mediator.Send(new GetAllActivitiesQuery());
    //    await Clients.Caller.SendAsync("ReceiveActivitiesList", activities);
    //}
}






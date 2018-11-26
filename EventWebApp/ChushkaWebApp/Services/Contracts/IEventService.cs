namespace EventWebApp.Services.Contracts
{
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Models.Events;

    public interface IEventService
    {
        EventViewModel[] GetAllEvents(string username);

        EventViewModel[] GetMyEvents(string username);

        Event GetEventById(string id);

        IActionResult CreateEvent(EventViewModel model);

        IActionResult EditEvent(EventViewModel model);

        IActionResult DeleteEvent(string id);

    }
}

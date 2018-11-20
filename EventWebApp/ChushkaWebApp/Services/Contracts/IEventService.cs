namespace EventWebApp.Services.Contracts
{
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Models.Events;

    public interface IEventService
    {
        EventViewModel[] GetAllEvents(string username);

        Event GetEventById(int id);

        IActionResult CreateEvent(EventViewModel model);

        IActionResult EditEvent(EventViewModel model);

        IActionResult DeleteEvent(string id);

    }
}

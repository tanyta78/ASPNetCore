namespace EventWebApp.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Models.Events;
    using Services.Contracts;

    [Authorize("Admin")]
    public class EventsController : Controller
    {
        private readonly IEventService eventService;
        private readonly ILogger logger;

        public EventsController(IEventService eventService, ILogger logger)
        {
            this.eventService = eventService;
            this.logger = logger;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(EventViewModel model)
        {
            return this.eventService.CreateEvent(model);
        }

        [AllowAnonymous]
        public IActionResult Details(string id)
        {
            var eventById = this.eventService.GetEventById(int.Parse(id));
            if (eventById == null)
            {
                return this.BadRequest("Invalid event id");
            }

            return this.View(eventById);
        }

        public IActionResult Delete(string id)
        {
            Event product = this.eventService.GetEventById(int.Parse(id));
            var viewModel = new EventViewModel
            {
               //todo
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(EventViewModel model)
        {
            return this.eventService.DeleteEvent(model.Id.ToString());
        }

        public IActionResult Edit(string id)
        {
            var evento = this.eventService.GetEventById(int.Parse(id));
            var viewModel = new EventViewModel
            {
               //todo edit props for changing
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(EventViewModel model)
        {
            return this.eventService.EditEvent(model);
        }
    }
}
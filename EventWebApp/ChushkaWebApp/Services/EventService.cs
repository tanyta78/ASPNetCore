namespace EventWebApp.Services
{
    using System.Linq;
    using Contracts;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Models.Events;

    public class EventService : PageModel, IEventService
    {
        private readonly ApplicationDbContext db;
        private readonly ILogger logger;

        public EventService(ApplicationDbContext db, ILogger logger)
        {
            this.db = db;
            this.logger = logger;
        }

        public IActionResult CreateEvent(EventViewModel model)
        {
            var evento = new Event
            {
                Name = model.Name,
                Place = model.Place,
                Start = model.Start,
                End = model.End,
                PricePerTicket = model.PricePerTicket,
                TotalTickets = model.TotalTickets
            };

            this.db.Events.Add(evento);
            this.db.SaveChanges();
            this.logger.LogInformation("Event created:" + evento.Name,evento);

            return this.Redirect($"/events/details?id={evento.Id}");
        }

        public IActionResult DeleteEvent(string id)
        {
            var evento = this.db.Events.FirstOrDefault(p => p.Id == int.Parse(id));
            if (evento == null) return this.Redirect("/");
            //this.db.Products.Remove(product).State = EntityState.Deleted;

            //todo: when trying to delete ordered product an exception has been thrown. Make soft delete. product.IsDeleted = true;
            this.db.Events.Remove(evento);

            this.db.SaveChanges();

            return this.Redirect("/");
        }

        public IActionResult EditEvent(EventViewModel model)
        {
            var evento = this.db.Events.FirstOrDefault(p => p.Id == model.Id);

            if (evento == null) return this.Redirect("/");
            //todo add properties for changing values
            evento.Name = model.Name;
            evento.Place = model.Place;
            evento.PricePerTicket = model.PricePerTicket;
            evento.TotalTickets = model.TotalTickets;

            this.db.Entry(evento).State = EntityState.Modified;
            this.db.SaveChanges();

            return this.Redirect("/");
        }

        public EventsListViewModel GetAllEvents(string username)
        {
            var events = this.db.Events
                             .Select(x =>
                                 new EventViewModel
                                 {
                                     Name = x.Name,
                                     Place = x.Place,
                                     Start = x.Start,
                                     End = x.End
                                 })
                             .ToArray();

            var viewModel = new EventsListViewModel()
            {
                Events = events,
            };

            return viewModel;
        }

        public Event GetEventById(int id)
        {
            var product = this.db.Events
                              .FirstOrDefault(x => x.Id == id);

            return product;
        }
    }
}


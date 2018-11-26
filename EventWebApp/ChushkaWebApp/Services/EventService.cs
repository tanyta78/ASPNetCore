namespace EventWebApp.Services
{
    using System.Linq;
    using Contracts;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Models.Events;
    using Models.Orders;

    public class EventService : PageModel, IEventService
    {
        private readonly ApplicationDbContext db;


        public EventService(ApplicationDbContext db)
        {
            this.db = db;

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
            // this.logger.LogInformation("Event created:" + evento.Name,evento);

            return this.Redirect($"/events/details?id={evento.Id}");
        }

        public IActionResult DeleteEvent(string id)
        {
            var evento = this.db.Events.FirstOrDefault(p => p.Id.ToString() == id);
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
            //todo: add properties for changing values
            evento.Name = model.Name;
            evento.Place = model.Place;
            evento.PricePerTicket = model.PricePerTicket;
            evento.TotalTickets = model.TotalTickets;

            this.db.Entry(evento).State = EntityState.Modified;
            this.db.SaveChanges();

            return this.Redirect("/");
        }

        public EventViewModel[] GetAllEvents(string username)
        {

            var events = this.db.Events
                             .Select(x =>
                                 new EventViewModel
                                 {
                                     Name = x.Name,
                                     Place = x.Place,
                                     Start = x.Start,
                                     End = x.End,
                                     Order = new OrderViewModel()
                                     {
                                         EventId = x.Id
                                     }
                                 })
                             .ToArray();

            return events;
        }

        public EventViewModel[] GetMyEvents(string username)
        {
            var user = this.db.Users.FirstOrDefault(x => x.UserName == username);

            var usersEvents = this.db.Orders
                                  .Include(o=>o.Event)
                                  .Where(o => o.CustomerId.ToString() == user.Id).ToList().GroupBy(o=>o.EventId).ToArray();
            var events = usersEvents
                          
                             .Select(x =>
                                 new EventViewModel
                                 {
                                     Name = x.First().Event.Name,
                                     Place = x.First().Event.Place,
                                     Start = x.First().Event.Start,
                                     End = x.First().Event.End,
                                     TicketsCount = x.Sum(y=>y.TicketsCount)
                                 })
                             .ToArray();

            return events;
        }

        public Event GetEventById(string id)
        {
            var product = this.db.Events
                              .FirstOrDefault(x => x.Id.ToString() == id);

            return product;
        }
    }
}


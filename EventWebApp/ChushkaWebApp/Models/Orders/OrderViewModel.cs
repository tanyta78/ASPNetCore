namespace EventWebApp.Models.Orders
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;

    public class OrderViewModel
    {
        public Guid Id { get; set; }

        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }

        public string CustomerId { get; set; }

        public virtual ApplicationUser Customer { get; set; }

        public DateTime OrderedOn { get; set; } = DateTime.UtcNow;

        [DisplayName("Tickets")]
        [Required(ErrorMessage = "Tickets count should not be empty")]
        [Range(0, int.MaxValue, ErrorMessage = "Tickets count must be a positive number")]
        public int TicketsCount { get; set; }

        public int AvailableTickets => this.Event == null ? 0 : this.Event.TotalTickets;
    }
}

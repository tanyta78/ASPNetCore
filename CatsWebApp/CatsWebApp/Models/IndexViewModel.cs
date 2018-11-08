namespace CatsWebApp.Models
{
    using System.Collections.Generic;
    using Data;

    public class IndexViewModel
    {
        public ICollection<Cat> Cats { get; set; } = new List<Cat>();
    }
}

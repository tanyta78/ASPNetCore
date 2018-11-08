namespace CatsWebApp.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;

    public interface ICatsService
    {
        ICollection<Cat> GetAllCats();
        Cat GetCatById(int id);
        void AddCat(Cat newCat);
    }

    public class CatsService : ICatsService
    {
        private readonly ApplicationDbContext db;

        public CatsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ICollection<Cat> GetAllCats()
        {
            return this.db.Cats.ToList();
        }

        public Cat GetCatById(int id)
        {
            return this.db.Cats.Find(id);
        }

        public void AddCat(Cat newCat)
        {
            this.db.Cats.Add(newCat);
            this.db.SaveChanges();

        }
    }
}

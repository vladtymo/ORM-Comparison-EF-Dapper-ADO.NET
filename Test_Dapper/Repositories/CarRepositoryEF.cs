using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Test_Dapper
{
    public class CarDbModel : DbContext
    {
        public CarDbModel(string connectionString)
            : base(connectionString) { }

        public virtual DbSet<Car> Cars { get; set; }
    }
    // EF
    public class CarRepositoryEF : ICarRepository
    {
        CarDbModel context = null;
        public CarRepositoryEF(string conn)
        {
            context = new CarDbModel(conn);
            context.Database.Log = Console.Write;
        }

        public Car Create(Car car)
        {
            var added = context.Cars.Add(car);
            Save();
            return added;
        }

        public void Delete(int id)
        {
            var car = context.Cars.Find(id);
            if (car != null)
            {
                context.Cars.Remove(car);
                Save();
            }
        }

        public Car Get(int id)
        {
            return context.Cars.Find(id); 
        }

        public List<Car> GetCars()
        {
            return context.Cars.ToList();
        }

        public void Update(Car car)
        {
            context.Entry(car).State = EntityState.Modified;
            Save();
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
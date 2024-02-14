using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Interfaces.CarInterfaces;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Persistence.Context;

namespace UdemyCarBook.Persistence.Repositories.CarRepositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarBookContext _context;

        public CarRepository(CarBookContext context)
        {
            _context = context;
        }

        public Car GetCarByIdWithBrand(int carId)
        {
            return _context.Cars
       .Include(c => c.Brand) 
       .FirstOrDefault(c => c.CarID == carId);
        }

        public int GetCarCount()
        {
            var value = _context.Cars.Count();
            return value;
        }

        public List<Car> GetCarsListWithBrands()
        {
            var values= _context.Cars.Include(x=>x.Brand).ToList();
            return values;
        }

        public List<Car> GetLast5CarsWithBrands()
        {
            var values = _context.Cars
               .Include(x => x.Brand)
               .Include(x => x.CarPricings)
               .Where(x => x.CarPricings.Any(cp => cp.PricingID == 1))
               .OrderByDescending(x => x.CarID)
               .Take(5)
               .ToList();

            return values;
        }


    }
}

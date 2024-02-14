using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Interfaces.RentACarInterfaces;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Persistence.Context;

namespace UdemyCarBook.Persistence.Repositories.RentACarRepositories
{
    public class RentACarRepository : IRentACarRepository
    {
        private readonly CarBookContext _context;

        public RentACarRepository(CarBookContext context)
        {
            _context = context;
        }

        public async Task< List<RentACar>> GetByFilterAsync(Expression<Func<RentACar, bool>> filter)
        {
            var values= await _context.RentACars.Where(filter).Include(x=>x.Car).ThenInclude(y => y.Brand).Include(x => x.Car.CarPricings)
               .Where(x => x.Car.CarPricings.Any(cp => cp.PricingID == 1)).ToListAsync();
            return values;
        }
    }
}

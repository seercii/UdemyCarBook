using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.RentACarQueries;
using UdemyCarBook.Application.Features.Mediator.Results.RentACarResults;
using UdemyCarBook.Application.Interfaces.RentACarInterfaces;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.RentACarHandlers
{
    public class GetRentAcarQueryHandler : IRequestHandler<GetRentACarQuery, List<GetRentACarQueryResult>>
    {
        private readonly IRentACarRepository _repository;

        public GetRentAcarQueryHandler(IRentACarRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetRentACarQueryResult>> Handle(GetRentACarQuery request, CancellationToken cancellationToken)
        {
            var values= await _repository.GetByFilterAsync(x=>x.LocationID==request.LocationID && x.Avaliable==true);
         var results= values.Select(y=> new GetRentACarQueryResult
            {
                CarId=y.CarID,
                Brand=y.Car.Brand.Name,
                Model=y.Car.Model,
                CoverImageUrl = y.Car.CoverImageUrl,
                Amount=y.Car.CarPricings.FirstOrDefault(cp => cp.PricingID == 1)?.Amount ?? 0,
         }).ToList();
            return results;
        }
    }
}

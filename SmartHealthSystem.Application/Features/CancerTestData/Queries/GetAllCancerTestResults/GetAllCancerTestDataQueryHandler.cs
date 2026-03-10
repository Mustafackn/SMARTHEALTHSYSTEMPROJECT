using MediatR;
using SmartHealthSystem.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.CancerTestData.Queries.GetAllCancerTestResults
{
    public class GetAllCancerTestDataQueryHandler : IRequestHandler<GetAllCancerTestDataQueryRequest, List<GetAllCancerTestDataQueryResponse>>
    {
        // Görseldeki gerçek isimlendirme: ICancerTestDataRepository
        private readonly ICancerTestDataRepository _repository;

        public GetAllCancerTestDataQueryHandler(ICancerTestDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAllCancerTestDataQueryResponse>> Handle(GetAllCancerTestDataQueryRequest request, CancellationToken cancellationToken)
        {
            // IGenericRepository'den gelen GetAllAsync metodunu kullanıyoruz
            var allData = await _repository.GetAllAsync();

            // Verileri tarihe göre azalan sırada getiriyoruz
            return allData.OrderByDescending(x => x.CreatedDate).Select(x => new GetAllCancerTestDataQueryResponse
            {
                Id = x.Id,
                RadiusMean = x.RadiusMean,
                TextureMean = x.TextureMean,
                PerimeterMean = x.PerimeterMean,
                AreaMean = x.AreaMean,
                SmoothnessMean = x.SmoothnessMean,
                CompactnessMean = x.CompactnessMean,
                ConcavityMean = x.ConcavityMean,
                ConcavePointsMean = x.ConcavePointsMean,
                SymmetryMean = x.SymmetryMean,
                FractalDimensionMean = x.FractalDimensionMean,
                CreatedDate = x.CreatedDate
            }).ToList();
        }
    }
}

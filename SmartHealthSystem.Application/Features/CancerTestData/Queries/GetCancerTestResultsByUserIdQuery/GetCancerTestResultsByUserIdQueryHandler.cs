using MediatR;
using SmartHealthSystem.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.CancerTestData.Queries.GetCancerTestResultsByUserIdQuery
{
    public class GetCancerTestResultsByUserIdQueryHandler : IRequestHandler<GetCancerTestResultsByUserIdQueryRequest, List<GetCancerTestResultsByUserIdQueryResponse>>
    {
        private readonly ICancerTestDataRepository _repository;

        public GetCancerTestResultsByUserIdQueryHandler(ICancerTestDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetCancerTestResultsByUserIdQueryResponse>> Handle(GetCancerTestResultsByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            // Repository üzerinden sadece bu UserId'ye ait analizleri getiriyoruz
            var cancerDataList = await _repository.GetAnalysesByPatientId(request.UserId);

            return cancerDataList.Select(x => new GetCancerTestResultsByUserIdQueryResponse
            {
                Id = x.Id,
                // Navigasyon property üzerinden UserId'ye erişiyoruz
                UserId = x.TestResult.UserId.ToString(),
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

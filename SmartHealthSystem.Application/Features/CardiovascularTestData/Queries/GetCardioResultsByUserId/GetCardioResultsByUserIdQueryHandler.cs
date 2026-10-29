using MediatR;
using SmartHealthSystem.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.CardiovascularTestData.Queries.GetCardioResultsByUserId
{
    public class GetCardioResultsByUserIdQueryHandler : IRequestHandler<GetCardioResultsByUserIdQueryRequest, List<GetCardioResultsByUserIdQueryResponse>>
    {
        private readonly ICardiovascularRepository _repository;

        public GetCardioResultsByUserIdQueryHandler(ICardiovascularRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetCardioResultsByUserIdQueryResponse>> Handle(GetCardioResultsByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            // Repository üzerinden sadece bu kullanıcıya ait verileri (TestResult dahil) getiriyoruz
            var cardioDataList = await _repository.GetCardioAnalysesByUserId(request.UserId);

            return cardioDataList.Select(x => new GetCardioResultsByUserIdQueryResponse
            {
                Id = x.Id,
                UserId = x.TestResult.UserId.ToString(),
                Age = x.Age,
                Gender = x.Gender,
                Height = x.Height,
                Weight = x.Weight,
                SystolicBP = x.SystolicBP,
                DiastolicBP = x.DiastolicBP,
                Cholesterol = x.Cholesterol,
                Glucose = x.Glucose,
                IsSmoking = x.IsSmoking,
                IsAlcoholConsumer = x.IsAlcoholConsumer,
                IsActive = x.IsActive,
                CreatedDate = x.CreatedDate,

                // TestResult tablosundan gelen AI tahmin detayları
                RiskPercentage = x.TestResult?.RiskPercentage ?? 0,
                HealthScore = (int)(x.TestResult?.HealthScore ?? 0),
                Recommendations = x.TestResult?.Recommendations ?? string.Empty
            }).ToList();
        }
    }
}

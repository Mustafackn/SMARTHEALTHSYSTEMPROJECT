using MediatR;
using SmartHealthSystem.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.CardiovascularTestData.Queries.GetAllCardioTest
{
    public class GetAllCardioTestDataQueryHandler : IRequestHandler<GetAllCardioTestDataQueryRequest, List<GetAllCardioTestDataQueryResponse>>
    {
        private readonly ICardiovascularRepository _repository;

        public GetAllCardioTestDataQueryHandler(ICardiovascularRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAllCardioTestDataQueryResponse>> Handle(GetAllCardioTestDataQueryRequest request, CancellationToken cancellationToken)
        {
            // Repository üzerinden teknik verileri ve bağlı TestResult'ları (Include ile) getiriyoruz
            var allData = await _repository.GetAllCardioAnalysesWithResults();

            return allData.Select(x => new GetAllCardioTestDataQueryResponse
            {
                Id = x.Id,
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

                // TestResult verilerini de ekliyoruz
                RiskPercentage = x.TestResult?.RiskPercentage ?? 0,
                RiskStatus = (x.TestResult?.RiskPercentage > 50) ? "Yüksek Risk" : "Düşük Risk"
            }).ToList();
        }
    }
}

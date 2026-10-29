using MediatR;
using SmartHealthSystem.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.DiabetesTestData.Queries.GetAllDiabetesTest
{
    public class GetAllDiabetesTestDataQueryHandler : IRequestHandler<GetAllDiabetesTestDataQueryRequest, List<GetAllDiabetesTestDataQueryResponse>>
    {
        private readonly IDiabetesRepository _repository;

        public GetAllDiabetesTestDataQueryHandler(IDiabetesRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAllDiabetesTestDataQueryResponse>> Handle(GetAllDiabetesTestDataQueryRequest request, CancellationToken cancellationToken)
        {
            // Repository üzerinden tüm diyabet kayıtlarını getiriyoruz
            var allData = await _repository.GetAllAsync();

            // Verileri oluşturulma tarihine göre tersten sıralayıp response modeline eşliyoruz
            return allData.OrderByDescending(x => x.CreatedDate).Select(x => new GetAllDiabetesTestDataQueryResponse
            {
                Id = x.Id,
                Gender = x.Gender,
                Age = x.Age,
                Hypertension = x.Hypertension,
                HeartDisease = x.HeartDisease,
                SmokingHistory = x.SmokingHistory,
                BMI = x.BMI,
                HbA1cLevel = x.HbA1cLevel,
                BloodGlucoseLevel = x.BloodGlucoseLevel,
                CreatedDate = x.CreatedDate
            }).ToList();
        }
    }
}

using MediatR;
using SmartHealthSystem.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.DiabetesTestData.Queries.GetDiabetesResultsByUserIdQuery
{
    public class GetDiabetesResultsByUserIdQueryHandler : IRequestHandler<GetDiabetesResultsByUserIdQueryRequest, List<GetDiabetesResultsByUserIdQueryResponse>>
    {
        private readonly IDiabetesRepository _diabetesRepository;

        public GetDiabetesResultsByUserIdQueryHandler(IDiabetesRepository diabetesRepository)
        {
            _diabetesRepository = diabetesRepository;
        }

        public async Task<List<GetDiabetesResultsByUserIdQueryResponse>> Handle(GetDiabetesResultsByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            // HeartDisease tarafında yazdığın 'GetAnalysesByPatientId' metodunun 
            // IDiabetesRepository için de tanımlı olduğunu varsayıyoruz.
            var diabetesDataList = await _diabetesRepository.GetDiabetesAnalysesByPatientId(request.UserId);

            return diabetesDataList
                .OrderByDescending(x => x.CreatedDate) // En güncel test en üstte gelsin
                .Select(x => new GetDiabetesResultsByUserIdQueryResponse
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

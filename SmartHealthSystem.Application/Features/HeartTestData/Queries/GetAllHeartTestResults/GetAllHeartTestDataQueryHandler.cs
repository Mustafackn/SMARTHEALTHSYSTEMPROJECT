using MediatR;
using SmartHealthSystem.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.HeartTestData.Queries.GetAllHeartTestResults
{
    public class GetAllHeartTestDataQueryHandler : IRequestHandler<GetAllHeartTestDataQueryRequest, List<GetAllHeartTestDataQueryResponse>>
    {
        private readonly IHeartDiseaseRepository _repository;

        public GetAllHeartTestDataQueryHandler(IHeartDiseaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAllHeartTestDataQueryResponse>> Handle(GetAllHeartTestDataQueryRequest request, CancellationToken cancellationToken)
        {
            // IGenericRepository'den gelen GetAllAsync metodunu kullanıyoruz
            var allData = await _repository.GetAllAsync();

            // HeartTestDatas tablosundaki tüm teknik değerleri eşliyoruz
            return allData.OrderByDescending(x => x.CreatedDate).Select(x => new GetAllHeartTestDataQueryResponse
            {
                Id = x.Id,
                Age = x.Age,
                Sex = x.Sex,
                Cp = x.Cp,
                Trestbps = x.Trestbps,
                Chol = x.Chol,
                Fbs = x.Fbs,
                Restecg = x.Restecg,
                Thalach = x.Thalach,
                Exang = x.Exang,
                Oldpeak = x.Oldpeak,
                Slope = x.Slope,
                Ca = x.Ca,
                Thal = x.Thal,
                CreatedDate = x.CreatedDate
            }).ToList();
        }
    }
}

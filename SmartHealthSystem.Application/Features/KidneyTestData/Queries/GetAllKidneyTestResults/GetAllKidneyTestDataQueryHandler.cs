using MediatR;
using SmartHealthSystem.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.KidneyTestData.Queries.GetAllKidneyTestResults
{
    public class GetAllKidneyTestDataQueryHandler : IRequestHandler<GetAllKidneyTestDataQueryRequest, List<GetAllKidneyTestDataQueryResponse>>
    {
        private readonly IKidneyDiseaseRepository _repository;

        public GetAllKidneyTestDataQueryHandler(IKidneyDiseaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAllKidneyTestDataQueryResponse>> Handle(GetAllKidneyTestDataQueryRequest request, CancellationToken cancellationToken)
        {
            // IGenericRepository'den gelen GetAllAsync metodunu kullanıyoruz
            var allData = await _repository.GetAllAsync();

            // Verileri tarihe göre azalan sırada dizip Response modeline eşliyoruz
            return allData.OrderByDescending(x => x.CreatedDate).Select(x => new GetAllKidneyTestDataQueryResponse
            {
                Id = x.Id,
                // UserId bilgisini TestResult üzerinden çekiyoruz
                UserId = x.TestResult?.UserId.ToString(),
                Age = (int)x.Age,

                // Double -> Int/Float dönüşümleri (CS0266 hatasını önlemek için)
                Bp = (int)x.Bp,
                Sg = (float)x.Sg, // 1.020 gibi ondalıklı değerler
                Al = (int)x.Al,
                Su = (int)x.Su,
                Bgr = (int)x.Bgr,
                Bu = (int)x.Bu,
                Sc = (float)x.Sc, // 1.4 gibi serum kreatinin değerleri
                Sod = (int)x.Sod,
                Pot = (float)x.Pot,
                Hemo = (float)x.Hemo,

                CreatedDate = x.CreatedDate
            }).ToList();
        }
    }
}

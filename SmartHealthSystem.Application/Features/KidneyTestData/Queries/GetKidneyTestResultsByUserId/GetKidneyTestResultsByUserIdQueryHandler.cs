using MediatR;
using SmartHealthSystem.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.KidneyTestData.Queries.GetKidneyTestResultsByUserId
{
    public class GetKidneyTestResultsByUserIdQueryHandler : IRequestHandler<GetKidneyTestResultsByUserIdQueryRequest, List<GetKidneyTestResultsByUserIdQueryResponse>>
    {
        private readonly IKidneyDiseaseRepository _kidneyDiseaseRepository;

        public GetKidneyTestResultsByUserIdQueryHandler(IKidneyDiseaseRepository kidneyDiseaseRepository)
        {
            _kidneyDiseaseRepository = kidneyDiseaseRepository;
        }

        public async Task<List<GetKidneyTestResultsByUserIdQueryResponse>> Handle(GetKidneyTestResultsByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            // Veritabanından hastaya ait ham verileri çekiyoruz
            var kidneyDataList = await _kidneyDiseaseRepository.GetAnalysesByPatientId(request.UserId);

            // Veritabanındaki 'double' tiplerini Response modelindeki 'int' ve 'float' tiplerine dönüştürerek eşliyoruz
            return kidneyDataList.Select(x => new GetKidneyTestResultsByUserIdQueryResponse
            {
                Id = x.Id,
                UserId = x.TestResult.UserId.ToString(),
                Age = (int)x.Age,
                // CS0266 hatasını önlemek için (int) ve (float) cast işlemlerini ekledik
                Bp = (int)x.Bp,
                Sg = (float)x.Sg, // 1.020 gibi değerler için float hassasiyeti
                Al = (int)x.Al,
                Su = (int)x.Su,
                Bgr = (int)x.Bgr,
                Bu = (int)x.Bu,
                Sc = (float)x.Sc, // 1.2 gibi değerler için
                Sod = (int)x.Sod,
                Pot = (float)x.Pot,
                Hemo = (float)x.Hemo,
                CreatedDate = x.CreatedDate
            }).ToList();
        }
    }
}

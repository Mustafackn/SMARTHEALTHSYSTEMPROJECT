using MediatR;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.HeartTestData.Queries.GetHeartTestResultsByUserId
{
    public class GetHeartTestResultsByUserIdQueryHandler : IRequestHandler<GetHeartTestResultsByUserIdQueryRequest, List<GetHeartTestResultsByUserIdQueryResponse>>
    {
        private readonly IHeartDiseaseRepository _heartDiseaseRepository;

        public GetHeartTestResultsByUserIdQueryHandler(IHeartDiseaseRepository heartDiseaseRepository)
        {
            _heartDiseaseRepository = heartDiseaseRepository;
        }

        public async Task<List<GetHeartTestResultsByUserIdQueryResponse>> Handle(GetHeartTestResultsByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            // Repository içindeki yeni metodumuzu çağırıyoruz
            var heartDataList = await _heartDiseaseRepository.GetAnalysesByPatientId(request.UserId);

            // Gelen teknik verileri (HeartTestData) Response modeline eşliyoruz
            return heartDataList.Select(x => new GetHeartTestResultsByUserIdQueryResponse
            {
                Id = x.Id,
                Age = x.Age,
                Sex = x.Sex,
                Cp = x.Cp,
                Trestbps = x.Trestbps,
                Chol = x.Chol,
                Oldpeak = x.Oldpeak,
                Ca = x.Ca,
                // Eğer TestResult üzerinden risk oranını da istersen x.TestResult.RiskPercentage diyebilirsin
            }).ToList();
        }
    }
}


using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Application.Features.TestResultsData.Query;
using SmartHealthSystem.Domain.Entites; // 🌟 TestResult entity modelinin bulunduğu namespace
using SmartHealthSystem.Domain.Entites.SmartHealthSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHealthSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestResultApiController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITestResultRepository _repository;

        public TestResultApiController(IMediator mediator, ITestResultRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        /// <summary>
        /// Giriş yapan kullanıcının tüm geçmiş analizlerini getirir. (MediatR / CQRS Yapısı)
        /// </summary>
        [HttpGet("GetMyResults/{userId}")]
        public async Task<IActionResult> GetMyResults(Guid userId)
        {
            var query = new GetPatientTestResultsQueryRequest { UserId = userId };
            var response = await _mediator.Send(query);

            if (response.IsSuccess)
                return Ok(response);

            return NotFound(response);
        }

        /// <summary>
        /// 🚀 DTO OLMADAN: Doğrudan veritabanı Entity modellerini (TestResult) kullanarak listeleme yapar.
        /// </summary>
        [HttpGet("GetMyResultsDirectEntity/{userId}")]
        public async Task<IActionResult> GetMyResultsDirectEntity(Guid userId)
        {
            // Veritabanından ilişkili 3 tabloyu ve TestType verisini çekiyoruz
            var results = await _repository.GetWhere(x => x.UserId == userId)
                                           .Include(x => x.TestType)
                                           .Include(x => x.CancerTestData)
                                           .Include(x => x.DiabetesTestData)
                                           .Include(x => x.CardiovascularTestData)
                                           .OrderByDescending(x => x.CreatedDate)
                                           .ToListAsync();

            if (results == null || !results.Any())
            {
                return NotFound(new { IsSuccess = false, Message = "Kullanıcıya ait analiz kaydı bulunamadı." });
            }

            // 🔄 ÇÖZÜM: Hem TestType hem de alt nesnelerin içindeki "çift yönlü" ters ilişkileri temizliyoruz.
            var cleanEntities = results.Select(x => new TestResult
            {
                Id = x.Id,
                UserId = x.UserId,
                TestTypeId = x.TestTypeId,
                RiskPercentage = x.RiskPercentage,
                HealthScore = x.HealthScore,
                Recommendations = x.Recommendations,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate, // BaseEntity'den gelen alanlar

                // 🛑 KRİTİK HATA ÇÖZÜMÜ: TestType nesnesini yeniden örnekleyerek içindeki TestResults ters listesini null bırakıyoruz!
                TestType = x.TestType == null ? null! : new TestType
                {
                    Id = x.TestType.Id,
                    TestName = x.TestType.TestName,
                    // Buraya varsa TestType içerisindeki Description gibi diğer düz alanları da ekleyebilirsin.
                },

                // Kanser verisini bağla, içindeki çift yönlü referansı kopar
                CancerTestData = x.CancerTestData == null ? null : new CancerTestData
                {
                    Id = x.CancerTestData.Id,
                    CreatedDate = x.CancerTestData.CreatedDate,
                    TestResultId = x.CancerTestData.TestResultId,
                    RadiusMean = x.CancerTestData.RadiusMean,
                    TextureMean = x.CancerTestData.TextureMean,
                    PerimeterMean = x.CancerTestData.PerimeterMean,
                    AreaMean = x.CancerTestData.AreaMean,
                    SmoothnessMean = x.CancerTestData.SmoothnessMean,
                    CompactnessMean = x.CancerTestData.CompactnessMean,
                    ConcavityMean = x.CancerTestData.ConcavityMean,
                    ConcavePointsMean = x.CancerTestData.ConcavePointsMean,
                    SymmetryMean = x.CancerTestData.SymmetryMean,
                    FractalDimensionMean = x.CancerTestData.FractalDimensionMean,
                    TestResult = null! // ❌ Sonsuz döngüyü kırdık
                },

                // Diyabet verisini bağla, içindeki çift yönlü referansı kopar
                DiabetesTestData = x.DiabetesTestData == null ? null : new DiabetesTestData
                {
                    Id = x.DiabetesTestData.Id,
                    CreatedDate = x.DiabetesTestData.CreatedDate,
                    TestResultId = x.DiabetesTestData.TestResultId,
                    Gender = x.DiabetesTestData.Gender,
                    Age = x.DiabetesTestData.Age,
                    Hypertension = x.DiabetesTestData.Hypertension,
                    HeartDisease = x.DiabetesTestData.HeartDisease,
                    SmokingHistory = x.DiabetesTestData.SmokingHistory,
                    BMI = x.DiabetesTestData.BMI,
                    HbA1cLevel = x.DiabetesTestData.HbA1cLevel,
                    BloodGlucoseLevel = x.DiabetesTestData.BloodGlucoseLevel,
                    TestResult = null! // ❌ Sonsuz döngüyü kırdık
                },

                // Kardiyovasküler verisini bağla, içindeki çift yönlü referansı kopar
                CardiovascularTestData = x.CardiovascularTestData == null ? null : new CardiovascularTestData
                {
                    Id = x.CardiovascularTestData.Id,
                    CreatedDate = x.CardiovascularTestData.CreatedDate,
                    TestResultId = x.CardiovascularTestData.TestResultId,
                    Age = x.CardiovascularTestData.Age,
                    Gender = x.CardiovascularTestData.Gender,
                    Height = x.CardiovascularTestData.Height,
                    Weight = x.CardiovascularTestData.Weight,
                    SystolicBP = x.CardiovascularTestData.SystolicBP,
                    DiastolicBP = x.CardiovascularTestData.DiastolicBP,
                    Cholesterol = x.CardiovascularTestData.Cholesterol,
                    Glucose = x.CardiovascularTestData.Glucose,
                    IsSmoking = x.CardiovascularTestData.IsSmoking,
                    IsAlcoholConsumer = x.CardiovascularTestData.IsAlcoholConsumer,
                    IsActive = x.CardiovascularTestData.IsActive,
                    TestResult = null! // ❌ Sonsuz döngüyü kırdık
                }
            }).ToList();

            return Ok(new
            {
                IsSuccess = true,
                Message = "Doğrudan TestResult entity listesi başarıyla getirildi.",
                Results = cleanEntities
            });
        }
    }
}
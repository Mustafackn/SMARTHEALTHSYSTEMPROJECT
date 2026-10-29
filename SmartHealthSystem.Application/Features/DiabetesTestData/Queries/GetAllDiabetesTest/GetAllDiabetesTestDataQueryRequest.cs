using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.DiabetesTestData.Queries.GetAllDiabetesTest
{
    public class GetAllDiabetesTestDataQueryRequest : IRequest<List<GetAllDiabetesTestDataQueryResponse>>
    {
        // Gerekirse buraya PatientId ekleyerek sadece belirli bir hastanın verileri istenebilir.
    }
}

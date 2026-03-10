using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.KidneyTestData.Queries.GetAllKidneyTestResults
{
    public class GetAllKidneyTestDataQueryRequest : IRequest<List<GetAllKidneyTestDataQueryResponse>> { }
}

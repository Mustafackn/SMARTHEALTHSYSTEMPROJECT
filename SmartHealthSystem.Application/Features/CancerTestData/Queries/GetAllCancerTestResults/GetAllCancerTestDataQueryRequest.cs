using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.CancerTestData.Queries.GetAllCancerTestResults
{
    public class GetAllCancerTestDataQueryRequest : IRequest<List<GetAllCancerTestDataQueryResponse>> { }
}

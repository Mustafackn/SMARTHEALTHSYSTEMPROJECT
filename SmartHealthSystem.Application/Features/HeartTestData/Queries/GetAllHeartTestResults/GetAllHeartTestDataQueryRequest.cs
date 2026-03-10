using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.HeartTestData.Queries.GetAllHeartTestResults
{
    public class GetAllHeartTestDataQueryRequest : IRequest<List<GetAllHeartTestDataQueryResponse>> { }
}

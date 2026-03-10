using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.CancerTestData.Queries.GetCancerTestResultsByUserIdQuery
{
    public class GetCancerTestResultsByUserIdQueryRequest : IRequest<List<GetCancerTestResultsByUserIdQueryResponse>>
    {
        public string UserId { get; set; }
    }
}

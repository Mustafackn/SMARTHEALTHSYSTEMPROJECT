using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.DiabetesTestData.Queries.GetDiabetesResultsByUserIdQuery
{
    public class GetDiabetesResultsByUserIdQueryRequest : IRequest<List<GetDiabetesResultsByUserIdQueryResponse>>
    {
        public string UserId { get; set; } = null!;
    }
}

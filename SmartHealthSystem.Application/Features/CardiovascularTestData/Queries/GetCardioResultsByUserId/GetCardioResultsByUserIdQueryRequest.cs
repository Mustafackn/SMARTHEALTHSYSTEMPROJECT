using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.CardiovascularTestData.Queries.GetCardioResultsByUserId
{
    public class GetCardioResultsByUserIdQueryRequest : IRequest<List<GetCardioResultsByUserIdQueryResponse>>
    {
        public string UserId { get; set; } = null!;
    }
}

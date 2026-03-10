using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.KidneyTestData.Queries.GetKidneyTestResultsByUserId
{
    public class GetKidneyTestResultsByUserIdQueryRequest : IRequest<List<GetKidneyTestResultsByUserIdQueryResponse>>
    {
        public string UserId { get; set; }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.HeartTestData.Queries.GetHeartTestResultsByUserId
{
    public class GetHeartTestResultsByUserIdQueryRequest : IRequest<List<GetHeartTestResultsByUserIdQueryResponse>>
    {
        public string UserId { get; set; }
    }
}

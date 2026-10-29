using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.Banners.Queries.GetBannerById
{
    public class GetBannerByIdQueryRequest : IRequest<GetBannerByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }
}

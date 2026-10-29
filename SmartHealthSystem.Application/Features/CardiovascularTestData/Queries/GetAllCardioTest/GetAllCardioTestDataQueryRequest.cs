using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.CardiovascularTestData.Queries.GetAllCardioTest
{
    public class GetAllCardioTestDataQueryRequest : IRequest<List<GetAllCardioTestDataQueryResponse>>
    {
        // İstersen buraya "Guid UserId" ekleyerek sadece o kullanıcının geçmişini getirebilirsin.
    }
}

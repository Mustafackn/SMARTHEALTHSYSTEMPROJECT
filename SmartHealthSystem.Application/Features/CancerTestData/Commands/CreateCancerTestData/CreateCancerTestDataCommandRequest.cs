using MediatR;
using SmartHealthSystem.Application.DTOs.CancerTestData;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.CancerTestData.Commands.CreateCancerTestData
{
    public class CreateCancerTestDataCommandRequest : CreateCancerTestDataDto, IRequest<CreateCancerTestDataCommandResponse>
    {
    }
}

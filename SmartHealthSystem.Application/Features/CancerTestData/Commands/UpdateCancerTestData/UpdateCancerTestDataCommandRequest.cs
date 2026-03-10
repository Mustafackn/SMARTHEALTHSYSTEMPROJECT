using MediatR;
using SmartHealthSystem.Application.DTOs.CancerTestData;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.CancerTestData.Commands.UpdateCancerTestData
{
    public class UpdateCancerTestDataCommandRequest : IRequest<UpdateCancerTestDataCommandResponse>
    {
        public UpdateCancerTestDataDto UpdateCancerTestDataDto { get; set; }
    }
}

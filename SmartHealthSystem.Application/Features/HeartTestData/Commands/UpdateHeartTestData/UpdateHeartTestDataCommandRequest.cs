using MediatR;
using SmartHealthSystem.Application.DTOs.HeratTestData;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.HeartTestData.Commands.UpdateHeartTestData
{
    public class UpdateHeartTestDataCommandRequest : IRequest<UpdateHeartTestDataCommandResponse>
    {
        public UpdateHeartTestDataDto UpdateHeartTestDataDto { get; set; }
    }
}

using MediatR;
using SmartHealthSystem.Application.DTOs.HeratTestData;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.HeartTestData.Commands.CreateHeartTestData
{
    public class CreateHeartTestDataCommandRequest : CreateHeartTestDataDto, IRequest<CreateHeartTestDataCommandResponse>
    {
    }
}

using MediatR;
using SmartHealthSystem.Application.DTOs.CardiovascularTestData;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.CardiovascularTestData.Commands.CreateCardiovascularTestData
{
    public class CreateCardiovascularTestDataCommandRequest : CreateCardiovascularTestDataDto, IRequest<CreateCardiovascularTestDataCommandResponse>
    {
    }
}

using MediatR;
using SmartHealthSystem.Application.DTOs.CardiovascularTestData;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.CardiovascularTestData.Commands.UpdateCardiovascularTestData
{
    public class UpdateCardiovascularTestDataCommandRequest : IRequest<UpdateCardiovascularTestDataCommandResponse>
    {
        public UpdateCardiovascularTestDataDto UpdateCardioDto { get; set; } = null!;
    }
}

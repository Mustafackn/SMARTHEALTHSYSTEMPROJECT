using MediatR;
using SmartHealthSystem.Application.DTOs.KidneyTestData;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.KidneyTestData.Commands.UpdateKidneyTestData
{
    public class UpdateKidneyTestDataCommandRequest : IRequest<UpdateKidneyTestDataCommandResponse>
    {
        public UpdateKidneyTestDataDto UpdateKidneyTestDataDto { get; set; }
    }
}

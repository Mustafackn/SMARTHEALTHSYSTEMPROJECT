using MediatR;
using SmartHealthSystem.Application.DTOs.DiabetesTestData;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.DiabetesTestData.Commands.UpdateDiabetesTestData
{
    public class UpdateDiabetesTestDataCommandRequest : IRequest<UpdateDiabetesTestDataCommandResponse>
    {
        public UpdateDiabetesTestDataDto UpdateDiabetesTestDataDto { get; set; } = null!;
    }
}

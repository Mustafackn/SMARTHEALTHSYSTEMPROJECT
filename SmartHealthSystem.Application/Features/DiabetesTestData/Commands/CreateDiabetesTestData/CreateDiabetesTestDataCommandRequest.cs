using MediatR;
using SmartHealthSystem.Application.DTOs.DiabetesTestData;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.DiabetesTestData.Commands.CreateDiabetesTestData
{
    public class CreateDiabetesTestDataCommandRequest : CreateDiabetesTestDataDto, IRequest<CreateDiabetesTestDataCommandResponse>
    {
    }
}

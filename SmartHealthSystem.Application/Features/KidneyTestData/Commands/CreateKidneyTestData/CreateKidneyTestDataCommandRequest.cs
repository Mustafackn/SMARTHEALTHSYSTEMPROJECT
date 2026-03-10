using MediatR;
using SmartHealthSystem.Application.DTOs.KidneyTestData;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.KidneyTestData.Commands.CreateKidneyTestData
{
    public class CreateKidneyTestDataCommandRequest : CreateKidneyTestDataDto, IRequest<CreateKidneyTestDataCommandResponse>
    {
        // Özellikler CreateKidneyTestDataDto'dan (age, bp, sg, al, su, rbc, pc, vb.) miras alınır.
    }
}

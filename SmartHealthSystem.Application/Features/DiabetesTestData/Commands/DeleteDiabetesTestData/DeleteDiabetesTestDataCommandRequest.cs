using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.DiabetesTestData.Commands.DeleteDiabetesTestData
{
    public class DeleteDiabetesTestDataCommandRequest : IRequest<DeleteDiabetesTestDataCommandResponse>
    {
        public int Id { get; set; }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.KidneyTestData.Commands.DeleteKidneyTestData
{
    public class DeleteKidneyTestDataCommandRequest : IRequest<DeleteKidneyTestDataCommandResponse>
    {
        public int Id { get; set; }
    }
}

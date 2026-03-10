using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.HeartTestData.Commands.DeleteHeartTestData
{
    public class DeleteHeartTestDataCommandRequest : IRequest<DeleteHeartTestDataCommandResponse>
    {
        public int Id { get; set; }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.CancerTestData.Commands.DeleteCancerTestData
{
    public class DeleteCancerTestDataCommandRequest : IRequest<DeleteCancerTestDataCommandResponse>
    {
        public int Id { get; set; } // Silinecek CancerTestData kaydının ID'si
    }
}


using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.DiabetesTestData.Commands.DeleteDiabetesTestData
{
    public class DeleteDiabetesTestDataCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}

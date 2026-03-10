using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.HeratTestData
{
    public class CreateHeartTestDataDto : BaseHeartTestDataDto
    {
        public string PatientId { get; set; }
    }
}

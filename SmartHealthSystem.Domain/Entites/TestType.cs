using SmartHealthSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Domain.Entites
{
    namespace SmartHealthSystem.Domain.Entities
    {
        public class TestType : BaseEntity
        {
            public string TestName { get; set; } = string.Empty;
            public string? Description { get; set; }

            // Navigation Property: Bir test tipine ait birçok sonuç olabilir.
            public virtual ICollection<TestResult> TestResults { get; set; } = new HashSet<TestResult>();
        }
    }
}

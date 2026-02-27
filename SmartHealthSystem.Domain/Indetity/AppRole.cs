using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Domain.Indetity
{
    public class AppRole : IdentityRole<Guid>
    {
        // Role özel eklemek istediğin bir açıklama alanı varsa buraya yazabilirsin.
        public string? Description { get; set; }

        public AppRole() : base() { }

        public AppRole(string roleName) : base(roleName)
        {
        }
    }
}

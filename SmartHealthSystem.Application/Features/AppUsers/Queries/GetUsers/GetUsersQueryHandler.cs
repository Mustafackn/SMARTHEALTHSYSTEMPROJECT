using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartHealthSystem.Application.DTOs.User;
using SmartHealthSystem.Domain.Indetity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.AppUsers.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQueryRequest, List<GetAllUsersDto>>
    {
        private readonly UserManager<AppUser> _userManager;

        public GetUsersQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<GetAllUsersDto>> Handle(GetUsersQueryRequest request, CancellationToken cancellationToken)
        {
            // 1. Identity üzerinden "User" rolündeki kullanıcıları belleğe alıyoruz (Async)
            var usersInRole = await _userManager.GetUsersInRoleAsync("User");

            // 2. Listeyi IQueryable'a çevirip filtreleme yapıyoruz
            var query = usersInRole.AsQueryable();

            // 3. Arama filtresi: Id bazlı arama yapıyorsan (Görseldeki gibi)
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(u => u.Id.ToString() == request.SearchTerm.Trim());
            }

            // 4. HATA ÇÖZÜMÜ: Veriler zaten bellekte olduğu için ToListAsync() yerine ToList() kullanıyoruz.
            // Çünkü 'usersInRole' artık bir SQL sorgusu değil, bir C# Listesidir.
            return query.Select(u => new GetAllUsersDto
            {
                Id = u.Id.ToString(),
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName,
                Email = u.Email
            }).ToList(); // <-- Burayı ToList() yaptık
        }
    }
}

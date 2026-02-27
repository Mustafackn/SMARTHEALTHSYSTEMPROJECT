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
            // Veritabanı sorgusunu başlatıyoruz (Identity UserManager üzerinden)
            var query = _userManager.Users.AsNoTracking();

            // Arama filtresi: Ad, Soyad veya Kullanıcı adında geçiyorsa getir
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(u => u.Id.ToString() == request.SearchTerm.Trim());
            }
            // Manuel Mapping: Entity -> DTO dönüşümü
            return await query.Select(u => new GetAllUsersDto
            {
                Id = u.Id.ToString(),
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName,
                Email = u.Email
            }).ToListAsync(cancellationToken);
        }
    }
}

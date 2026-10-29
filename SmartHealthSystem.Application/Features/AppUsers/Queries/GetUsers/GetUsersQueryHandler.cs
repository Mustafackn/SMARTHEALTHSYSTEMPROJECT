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
            // 1. Eğer SearchTerm bir ID ise, direkt o kullanıcıyı getir (Rol kısıtlamasına takılmadan)
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var user = await _userManager.FindByIdAsync(request.SearchTerm.Trim());

                if (user != null)
                {
                    return new List<GetAllUsersDto>
            {
                new GetAllUsersDto
                {
                    Id = user.Id.ToString(),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email
                }
            };
                }
                // Kullanıcı ID ile bulunamadıysa boş liste dön
                return new List<GetAllUsersDto>();
            }

            // 2. Eğer SearchTerm boşsa, genel listeyi getir (Eski mantığın)
            var usersInRole = await _userManager.GetUsersInRoleAsync("User");
            return usersInRole.Select(u => new GetAllUsersDto
            {
                Id = u.Id.ToString(),
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName,
                Email = u.Email
            }).ToList();
        }
    }
}

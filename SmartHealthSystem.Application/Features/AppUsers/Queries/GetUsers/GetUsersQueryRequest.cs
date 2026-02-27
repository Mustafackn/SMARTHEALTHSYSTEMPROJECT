using MediatR;
using SmartHealthSystem.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.AppUsers.Queries.GetUsers
{
    public class GetUsersQueryRequest : IRequest<List<GetAllUsersDto>>
    {
        // Arama terimini buraya alıyoruz, liste dönüşü GetAllUsersDto üzerinden olacak
        public string? SearchTerm { get; set; }
    }
}

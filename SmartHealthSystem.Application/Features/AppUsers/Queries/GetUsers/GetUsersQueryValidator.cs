using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.AppUsers.Queries.GetUsers
{
    public class GetUsersQueryValidator : AbstractValidator<GetUsersQueryRequest>
    {
        public GetUsersQueryValidator()
        {
            RuleFor(x => x.SearchTerm)
                .MaximumLength(50).WithMessage("Arama kelimesi çok uzun.");
        }
    }
}

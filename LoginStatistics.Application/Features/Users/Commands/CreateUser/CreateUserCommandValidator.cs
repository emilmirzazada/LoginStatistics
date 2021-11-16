using FluentValidation;
using LoginStatistics.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoginStatistics.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .EmailAddress().WithMessage("A valid email address is required.")
                .MaximumLength(25).WithMessage("{PropertyName} must not exceed 25 characters.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.");

            RuleFor(p => p.Surname)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.");

        }

    }
}

﻿using Core.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.LastName).NotEmpty();
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.FirstName).MinimumLength(3);
            RuleFor(u => u.FirstName).MaximumLength(100);
            RuleFor(u => u.LastName).MinimumLength(2);
            RuleFor(u => u.LastName).MaximumLength(150);
            RuleFor(u => u.Email).MinimumLength(15);
            RuleFor(u => u.Email).MinimumLength(200);
        }
    }
}

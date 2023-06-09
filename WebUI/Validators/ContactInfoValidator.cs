﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.DTOs;
using Domain.Entities;
using FluentValidation;

namespace WebUI.Validators;

public class ContactInfoValidator : AbstractValidator<ContactInfoDto>
{
    public ContactInfoValidator()
    {
        RuleFor(x => x.Address)
            .NotEmpty()
            .Length(1, 50);

        RuleFor(x => x.City)
            .NotEmpty()
            .Length(1, 50);
	
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(1, 100);
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<ContactInfoDto>.CreateWithOptions((ContactInfoDto)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
    
}
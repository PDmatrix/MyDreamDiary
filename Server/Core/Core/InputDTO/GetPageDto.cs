using System;
using System.Collections.Generic;
using FluentValidation;

namespace Core.InputDTO
{
    public class PageGetDto
    {
        public int PageSize { get; set; }
        public string Tags { get; set; }

        public IEnumerable<string> GetTags()
        {
            return Tags.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
    
    public class PageGetDtoValidator: AbstractValidator<PageGetDto> {
        public PageGetDtoValidator()
        {
            RuleFor(r => r.PageSize).GreaterThan(0).WithMessage("PageSize must be greater than zero");
        }
    }
}
using System;
using System.Collections.Generic;
using FluentValidation;

namespace Core.InputDTO
{
    public class GetPageDtoIn
    {
        public int PageSize { get; set; } = 5;
        public string Tags { get; set; }

        public IEnumerable<string> GetTags()
        {
            return Tags?.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
    
    public class GetPageDtoInValidator: AbstractValidator<GetPageDtoIn> {
        public GetPageDtoInValidator()
        {
            RuleFor(r => r.PageSize).GreaterThan(0).WithMessage("PageSize must be greater than zero");
        }
    }
}
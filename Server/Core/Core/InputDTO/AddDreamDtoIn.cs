using System;
using FluentValidation;

namespace Core.InputDTO
{
    public class AddDreamDtoIn
    {
        public string Content { get; set; }
        public DateTime DreamDate { get; set; }
    }
    
    public class AddDreamDtoInValidator: AbstractValidator<AddDreamDtoIn> {
        public AddDreamDtoInValidator()
        {
            RuleFor(r => r.Content).NotEmpty();
            RuleFor(r => r.DreamDate).NotEmpty();
        }
    }
}
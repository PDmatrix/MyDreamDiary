using FluentValidation;

namespace Core.InputDTO
{
    public class AddPostDtoIn
    {
        public int DreamId { get; set; }
        public string Title { get; set; }
    }
    
    public class AddPostDtoInValidator: AbstractValidator<AddPostDtoIn> {
        public AddPostDtoInValidator()
        {
            RuleFor(r => r.DreamId).NotEmpty();
            RuleFor(r => r.Title).NotEmpty();
        }
    }
}
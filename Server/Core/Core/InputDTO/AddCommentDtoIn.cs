using FluentValidation;

namespace Core.InputDTO
{
	public class AddCommentDtoIn
	{
		public string Content { get; set; }
	}
	
	public class AddCommentDtoInValidator: AbstractValidator<AddCommentDtoIn> {
		public AddCommentDtoInValidator()
		{
			RuleFor(r => r.Content).NotEmpty();
		}
	}
}
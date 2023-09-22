using FluentValidation;

namespace Assessment3.Server.Application.UserEvents.Commands.Create;

public class CreateUserEventCommandValidator : AbstractValidator<CreateUserEventCommand>
{
    public CreateUserEventCommandValidator()
    {
        RuleFor(x => x.EventId)
            .NotEqual(Guid.Empty);
        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty);
       
    }
}
using FluentValidation;

namespace Assessment3.Server.Application.Events.Queries.GetEvent;

public class GetEventQueryValidator : AbstractValidator<GetEventQuery>
{
    public GetEventQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotNull();
    }
}
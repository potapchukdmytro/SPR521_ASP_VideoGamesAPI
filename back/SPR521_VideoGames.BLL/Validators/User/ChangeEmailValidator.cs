using FluentValidation;
using SPR521_VideoGames.BLL.Dtos.User;
using SPR521_VideoGames.DAL.Repositories;

namespace SPR521_VideoGames.BLL.Validators.User
{
    public class ChangeEmailValidator : AbstractValidator<ChangeEmailDto>
    {
        public ChangeEmailValidator(UserRepository userRepository)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Пошта є обов'язковою")
                .Matches(@"^[a-zA-Z0-9](?:[a-zA-Z0-9._%+-]*[a-zA-Z0-9])?@[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?)+$").WithMessage("Невірний формат пошти")
                .MustAsync(async (email, ct) => !await userRepository.IsExistsEmailAsync(email, ct))
                .WithMessage(x => $"Пошта '{x.Email}' вже використовуєтся");
        }
    }
}

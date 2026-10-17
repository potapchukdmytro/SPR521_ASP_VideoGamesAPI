using FluentValidation;
using SPR521_VideoGames.BLL.Dtos.Role;
using SPR521_VideoGames.DAL.Repositories;

namespace SPR521_VideoGames.BLL.Validators.Role
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleDto>
    {
        public CreateRoleValidator(RoleRepository roleRepository)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ім'я ролі не може бути порожнім")
                .MustAsync(async (name, ct) => !await roleRepository.IsExistsAsync(name, ct))
                .WithMessage(x => $"Роль з іменем {x.Name} вже існує");
        }
    }
}

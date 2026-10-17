using SPR521_VideoGames.BLL.Dtos;
using SPR521_VideoGames.DAL.Repositories;

namespace SPR521_VideoGames.BLL.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResponseDto> AddToRoleAsync(int userId , string roleName, CancellationToken ct = default)
        {
            var user = await _userRepository.GetByIdAsync(userId, ct);

            if(user == null)
            {
                return ResponseDto.Error($"Користувача з id '{userId}' не знайдено");
            }

            await _userRepository.AddToRoleAsync(user, roleName, ct);

            return ResponseDto.Success($"Користувачу додано роль '{roleName}'");
        }

        public async Task<ResponseDto> RemoveFromRoleAsync(int userId , string roleName, CancellationToken ct = default)
        {
            var user = await _userRepository.GetByIdAsync(userId, ct);

            if(user == null)
            {
                return ResponseDto.Error($"Користувача з id '{userId}' не знайдено");
            }

            await _userRepository.DeleteFromRoleAsync(user, roleName, ct);

            return ResponseDto.Success($"Користувача видалено з ролі '{roleName}'");
        }
    }
}

using BAL.DTOs;
using DAL.Repository;

namespace BAL.Services
{
    public class UsersServices
    {
        private readonly UsersRepository _usersRepository;

        public UsersServices()
        {
            _usersRepository = new UsersRepository();
        }

        public async Task<List<UsersDTO>> GetAllAsync()
        {
            var users = await _usersRepository.GetAllAsync();

            return users.Select(x => new UsersDTO
            {
                Email = x.Email,
                Password = x.Password
            }).ToList();
        }
    }
}
using DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class UsersRepository
    {
        private readonly RestaurantContext _context;
        public UsersRepository()
        {
            _context = new RestaurantContext();
        }

        public async Task<List<Users>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}

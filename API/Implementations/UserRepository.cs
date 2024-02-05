using API.Data;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
             return await _context.Users
            .FirstOrDefaultAsync(x=>x.Id ==id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users
             .FirstOrDefaultAsync(x=>x.UserName ==userName);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users
             .ToListAsync();
        }

        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
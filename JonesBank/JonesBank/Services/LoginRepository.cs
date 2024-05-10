using Jones_Bank.Models;
using JonesBank.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Jones_Bank.Services
{
    public interface ILoginRepository
    {
        Task<User> GetUser(UserDTO userDTO);
        Task<User> GetUserById(int id);
        Task<User> InsertUser(UserDTO userDTO);
    }

    public class LoginRepository : ILoginRepository
    {
        private readonly AppDbContext _dbContext;

        public LoginRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUser(UserDTO userDTO)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == userDTO.Email && x.Pass == userDTO.Pass);
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            user.Pass = "";

            return user;
        }

        public async Task<User> InsertUser(UserDTO userDTO)
        {
            User user = new User();

            user.Nombre = userDTO.Nombre;
            user.Email = userDTO.Email;
            user.Pass = userDTO.Pass;

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

    }
}

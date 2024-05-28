using CarFlex.Data;
using CarFlex.Models;
using Microsoft.EntityFrameworkCore;

namespace CarFlex.Utilities;

public interface IUserService
{
    Task<User> Authenticate(string username, string password);
    Task<User> Register(User user, string password);
    Task<bool> UserExists(string username);
}

public class UserService : IUserService
{
    private readonly CarFlexDbContext _context;

    public UserService(CarFlexDbContext context)
    {
        _context = context;
    }

    public async Task<User> Authenticate(string username, string password)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);

        if (user == null || !PasswordHasher.VerifyPassword(password, user.PasswordHash))
        {
            return null;
        }

        return user;
    }

    public async Task<User> Register(User user, string password)
    {
        if (await UserExists(user.Username))
        {
            throw new Exception("Username already exists");
        }

        user.PasswordHash = PasswordHasher.HashPassword(password);
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(u => u.Username == username);
    }
}
using CarFlex.Models;
using CarFlex.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarFlex.Controllers
{
    public class AccountController : Controller
    {
        private readonly CarFlexDbContext _context;
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(CarFlexDbContext context, IUserService userService, ILogger<AccountController> logger)
        {
            _context = context;
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userService.Authenticate(username, password);

            if (user == null)
            {
                TempData["Message"] = "Invalid username or password";
                return View();
            }

            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            TempData["Message"] = "Login successful!";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("Role");

            TempData["Message"] = "Logged out successfully!";
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Password,Role")] UserCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new User
                    {
                        Username = viewModel.Username,
                        HashedPassword = PasswordHasher.HashPassword(viewModel.Password),
                        Role = viewModel.Role
                    };

                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("User {Username} created successfully with role {Role}", user.Username,
                        user.Role);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while creating user {Username}", viewModel.Username);
                    ModelState.AddModelError("", "An error occurred while creating the user.");
                }
            }
            else
            {
                _logger.LogWarning("Invalid model state for user {Username}", viewModel.Username);
                foreach (var error in ModelState)
                {
                    foreach (var subError in error.Value.Errors)
                    {
                        _logger.LogWarning("Validation error for {Field}: {Error}", error.Key, subError.ErrorMessage);
                    }
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new UserEditViewModel
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,Role")] UserEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users.FindAsync(id);
                if (existingUser == null)
                {
                    return NotFound();
                }

                existingUser.Username = viewModel.Username;
                if (!string.IsNullOrEmpty(viewModel.Password))
                {
                    existingUser.HashedPassword =
                        PasswordHasher.HashPassword(viewModel.Password);
                }

                existingUser.Role = viewModel.Role;

                _context.Update(existingUser);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> UserExists(int id)
        {
            return await _context.Users.AnyAsync(e => e.Id == id);
        }
    }
}

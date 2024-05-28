using CarFlex.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarFlex
{
    public class AccountController : Controller
    {
        private readonly CarFlexDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(CarFlexDbContext context, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe,
                    lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // GET: Account
        // GET: Account
        public async Task<IActionResult> Index()
        {
            var identityUsers = await _userManager.Users.ToListAsync();
            var users = identityUsers.Select(user => new User
            {
                UserId = user.Id, // Assuming UserId is string in your User model or you need to convert it.
                Username = user.UserName,
                // Password should not be included for security reasons
                IsAdmin = false // Or fetch this information if you have a way to store this
            }).ToList();

            return View(users);
        }

        // GET: Account/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUser = await _userManager.FindByIdAsync(id);
            if (identityUser == null)
            {
                return NotFound();
            }

            var user = new User
            {
                UserId = identityUser.Id, // Assuming UserId is an integer
                Username = identityUser.UserName,
                // Password should not be included for security reasons
                IsAdmin = false // Or fetch this information if you have a way to store this
            };

            return View(user);
        }

        // GET: Account/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Password,IsAdmin")] User user)
        {
            Console.WriteLine("Create method hit");

            if (ModelState.IsValid)
            {
                var identityUser = new IdentityUser { UserName = user.Username };
                var result = await _userManager.CreateAsync(identityUser, user.Password);
                if (result.Succeeded)
                {
                    Console.WriteLine("User created successfully");

                    if (user.IsAdmin)
                    {
                        await _userManager.AddToRoleAsync(identityUser, "Admin");
                        Console.WriteLine("User added to Admin role");
                    }

                    await _userManager.AddToRoleAsync(identityUser, "User");
                    Console.WriteLine("User added to User role");

                    // Set the UserId from the identityUser
                    user.UserId = identityUser.Id;

                    // Add the user to your custom User table if needed
                    // _context.Users.Add(user); // Assuming you have a DbSet<User> in your context
                    // await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    Console.WriteLine($"Error: {error.Description}");
                }
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validation error: {error.ErrorMessage}");
                }
            }

            Console.WriteLine("Model state is invalid");
            return View(user);
        }



        // GET: Account/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUser = await _userManager.FindByIdAsync(id);
            if (identityUser == null)
            {
                return NotFound();
            }

            // Map IdentityUser to User
            var user = new User
            {
                UserId = (identityUser.Id), // Assuming UserId is an integer
                Username = identityUser.UserName,
                // Password should not be included for security reasons
                IsAdmin = false // Or fetch this information if you have a way to store this
            };

            return View(user);
        }

        // POST: Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Username,IsAdmin")] User user)
        {
            if (id != int.Parse(user.UserId))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var identityUser = await _userManager.FindByIdAsync(user.UserId.ToString());
                    if (identityUser == null)
                    {
                        return NotFound();
                    }

                    identityUser.UserName = user.Username;
                    // Update other fields as needed

                    var result = await _userManager.UpdateAsync(identityUser);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(user);
        }

        // GET: Account/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUser = await _userManager.FindByIdAsync(id);
            if (identityUser == null)
            {
                return NotFound();
            }

            // Map IdentityUser to User
            var user = new User
            {
                UserId = (identityUser.Id), // Assuming UserId is an integer
                Username = identityUser.UserName,
                // Password should not be included for security reasons
                IsAdmin = false // Or fetch this information if you have a way to store this
            };

            return View(user);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> UserExists(string id)
        {
            return await _userManager.FindByIdAsync(id) != null;
        }
    }
}

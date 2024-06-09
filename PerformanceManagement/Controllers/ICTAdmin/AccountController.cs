using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.Models;
using PerformanceManagement.Models.ICTAdmin;
using PerformanceManagement.Models.ICTAdmin.Services;
using PerformanceManagement.Util;
using PerformanceManagement.ViewModels;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using PerformanceManagement.Models.Employee.Services;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "ICTAdmin")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;
        private readonly IAuthorizationService authService;
        private readonly IConnProviderOBI connProviderOBI;
        private readonly Util.IEmailSender emailSender;

        //private readonly IHttpContextAccessor httpContextAccessor;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager
            , RoleManager<IdentityRole> roleManager, AppDbContext appDbContext/*, IHttpContextAccessor httpContextAccessor*/, IConnProvider connProvider, IAuthorizationService authService, IConnProviderOBI connProviderOBI, PerformanceManagement.Util.IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
            this.authService = authService;
            this.connProviderOBI = connProviderOBI;
            this.emailSender = emailSender;
            //this.httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public IActionResult Register()
        {
            //return View("/Views/ICTAdmin/account/register.cshtml");
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            using (IDbContextTransaction transaction = appDbContext.Database.BeginTransaction())
            {
                IDbConnection conn = connProviderOBI.Connection;
                IDbTransaction transactionOBI = null;
                try
                {
                    if (ModelState.IsValid)
                    {
                        var id = (await userManager.GetUserAsync(HttpContext.User))?.Id;
                        // Copy data from RegisterViewModel to IdentityUser

                        var people = appDbContext.People;
                        int maxid;
                        if (people.FirstOrDefault() == null)
                        {
                            maxid = 1;
                        }
                        else
                        {
                            maxid = appDbContext.People.Max(c => c.PeopleId) + 1;
                        }
                        var user = new ApplicationUser
                        {
                            UserName = model.Email,
                            Email = model.Email2,
                            IdNumber = model.IdNumber,
                            EffectiveStartDate = DateTime.Now,
                            CreatedBy = id,
                            People = new People
                            {
                                PeopleId = Convert.ToInt32(maxid),
                                EffectiveStartDate = DateTime.Now,
                                FirstName = model.FirstName,
                                LastName = model.LastName
                            }
                        };
                        userManager.Options.Password.RequiredLength = 5;
                        userManager.Options.Password.RequiredUniqueChars = 0;
                        userManager.Options.Password.RequireNonAlphanumeric = false;
                        userManager.Options.Password.RequireUppercase = false;
                        userManager.Options.Password.RequireLowercase = false;
                        // Store user data in AspNetUsers database table
                        var result = await userManager.CreateAsync(user, model.Password);

                        // If user is successfully created, sign-in the user using 
                        // SignInManager and redirect to index action of HomeController
                        if (result.Succeeded)
                        {
                            userManager.Options.Password.RequiredLength = 5;
                            userManager.Options.Password.RequiredUniqueChars = 2;
                            userManager.Options.Password.RequireNonAlphanumeric = true;
                            userManager.Options.Password.RequireUppercase = true;
                            userManager.Options.Password.RequireLowercase = true;
                            conn.Open();
                            transactionOBI = conn.BeginTransaction();
                            AccountService accountService = new AccountService(null, null);
                            accountService.OBIRegister(conn, transactionOBI, model.Password, user.UserName, user.Id);
                            //await signInManager.SignInAsync(user, isPersistent: false);
                            transaction.Commit();
                            transactionOBI.Commit();
                            conn.Dispose();
                            return RedirectToAction("Register", "Account");
                        }

                        // If there are any errors, add them to the ModelState object
                        // which will be displayed by the validation summary tag helper
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }

                    //return View("/Views/ICTAdmin/account/register.cshtml", model);
                    return View(model);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    transactionOBI.Rollback();
                    conn.Dispose();
                    return Json("0000");
                }

            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            //return View("/Views/ICTAdmin/account/login.cshtml");
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    var people = appDbContext.People.ToList();
                    var userId = appDbContext.applicationUsers.Where(c => c.UserName == model.Email).SingleOrDefault().Id;
                    var peopleId = appDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
                    var peopleEffectiveStartDate = appDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.EffectiveStartDate;
                    var user = appDbContext.applicationUsers.Where(c => c.UserName == model.Email).SingleOrDefault();
                    HttpContext.Session.SetString("userId", peopleId.ToString());
                    //var test = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    //HttpContext.Session.SetString("userId", (await userManager.GetUserAsync(HttpContext.User))?.Id);
                    //var test2 = userManager.GetUserId(HttpContext.User);

                    var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                    user.LastLoginIpAddress = ip;
                    user.LastLoginDate = DateTime.Now;
                    appDbContext.Update(user);
                    appDbContext.SaveChanges();
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }

                    //MustChangePassControlViewModel mustChangePassControlViewModel = new MustChangePassControlViewModel();
                    //mustChangePassControlViewModel.UserName = model.Email;
                    //AuthorizationResult authorized = await authService.AuthorizeAsync(User, mustChangePassControlViewModel, "MustChangePassword").ConfigureAwait(false);

                    //if (authorized.Succeeded)
                    //{

                    //}
                    //else
                    //{
                    //    return RedirectToAction("MustChangePassword", "Profile");
                    //}
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            //return View("/Views/ICTAdmin/account/login.cshtml",model);
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            //return View("/Views/ICTAdmin/account/CreateRole.cshtml");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // We just need to specify a unique role name to create a new role
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                // Saves the role in the underlying AspNetRoles table
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            //return View("/Views/ICTAdmin/account/CreateRole.cshtml",model);
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            //return View("/Views/ICTAdmin/account/AccessDenied.cshtml");
            return View();
        }
        [HttpGet]
        public IActionResult UserList()
        {
            ResetService resetService = new ResetService(null, connProvider);
            return Json(resetService.UserList());
        }
        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(int employeeId, string newPassword, string confirmPassword)
        {
            var peopleId = appDbContext.People.Where(c => c.PeopleId == employeeId && c.PositionType == 1 && c.EffectiveEndDate == null).SingleOrDefault();
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = appDbContext.applicationUsers.Where(c => c.People.PeopleId == peopleId.PeopleId).FirstOrDefault().Id;
            //int personId = appDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

            IdentityUser identityUser = appDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault();
            userManager.Options.Password.RequiredLength = 5;
            userManager.Options.Password.RequiredUniqueChars = 0;
            userManager.Options.Password.RequireNonAlphanumeric = false;
            userManager.Options.Password.RequireUppercase = false;
            userManager.Options.Password.RequireLowercase = false;
            var token = await userManager.GeneratePasswordResetTokenAsync(identityUser);

            IdentityResult result = await userManager.ResetPasswordAsync(appDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault(), token, newPassword);
            if (result.Succeeded)
            {
                var people = appDbContext.People.ToList();
                ApplicationUser applicationUser = appDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault();
                applicationUser.LastResetPasswordBy = appDbContext.applicationUsers.Where(c => c.UserName == User.Identity.Name).SingleOrDefault().People.PeopleId;
                applicationUser.LastResetPasswordDate = DateTime.Now;
                appDbContext.Update(applicationUser);
                appDbContext.SaveChanges();
                userManager.Options.Password.RequiredLength = 5;
                userManager.Options.Password.RequiredUniqueChars = 2;
                userManager.Options.Password.RequireNonAlphanumeric = true;
                userManager.Options.Password.RequireUppercase = true;
                userManager.Options.Password.RequireLowercase = true;
                return Json(1);
            }
            return Json(0);
        }

        public async Task<IActionResult> RegisterFromCSV()
        {

            var reader = new StreamReader(System.IO.File.OpenRead(@"C:\Users\poroohan_mohsen\Desktop\user2.csv"), System.Text.Encoding.UTF8);

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                var id = (await userManager.GetUserAsync(HttpContext.User))?.Id;
                // Copy data from RegisterViewModel to IdentityUser

                var people = appDbContext.People;
                int maxid;
                if (people.FirstOrDefault() == null)
                {
                    maxid = 1;
                }
                else
                {
                    maxid = appDbContext.People.Max(c => c.PeopleId) + 1;
                }
                var user = new ApplicationUser
                {
                    UserName = values[2],
                    Email = values[2],
                    EffectiveStartDate = DateTime.Now,
                    CreatedBy = id,
                    People = new People
                    {
                        PeopleId = Convert.ToInt32(maxid),
                        EffectiveStartDate = DateTime.Now,
                        FirstName = values[0],
                        LastName = values[1]
                    }
                };

                // Store user data in AspNetUsers database table
                try
                {
                    var result = await userManager.CreateAsync(user, values[4]);
                }
                catch (Exception)
                {

                    throw;
                }

            }

            return RedirectToAction("Register", "Account");
        }

        public async Task<IActionResult> UpdateAspNetUsersFromCSV()
        {

            var reader = new StreamReader(System.IO.File.OpenRead(@"C:\Users\poroohan_mohsen\Desktop\email-IdNumber.csv"), System.Text.Encoding.UTF8);

            while (!reader.EndOfStream)
            {

                try
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    ApplicationUser applicationUser = appDbContext.applicationUsers.Where(c => c.UserName == values[0]).SingleOrDefault();
                    if (applicationUser != null)
                    {
                        applicationUser.Email = values[1];
                        applicationUser.NormalizedEmail = values[1].ToUpper();
                        applicationUser.IdNumber = Convert.ToInt64(values[2]);
                        var result = appDbContext.Update(applicationUser);
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }
            appDbContext.SaveChanges();
            return RedirectToAction("Register", "Account");
        }

    }
}

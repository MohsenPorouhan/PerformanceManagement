using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.Models;
using System.Linq;
using PerformanceManagement.Util;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using PerformanceManagement.Models.Employee.Services;
using PerformanceManagement.Models.ICTAdmin;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using Dapper;

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "Employee")]
    public class ProfileController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConnProviderOBI connProviderOBI;
        private readonly IEmailSender emailSender;

        //private readonly IConfiguration config;

        public ProfileController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider, IHostingEnvironment hostingEnvironment, SignInManager<IdentityUser> signInManager, IConnProviderOBI connProviderOBI, PerformanceManagement.Util.IEmailSender emailSender)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
            this.hostingEnvironment = hostingEnvironment;
            this.signInManager = signInManager;
            this.connProviderOBI = connProviderOBI;
            this.emailSender = emailSender;
        }
        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            using (IDbContextTransaction transaction = applicationDbContext.Database.BeginTransaction())
            {
                IDbConnection conn = connProviderOBI.Connection;
                IDbTransaction transactionOBI = null;
                try
                {
                    applicationDbContext.People.ToList();
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

                    var isValid = await userManager.CheckPasswordAsync(applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault(), currentPassword);
                    IdentityResult result;
                    if (isValid)
                    {
                        var isValidNewPass = await userManager.CheckPasswordAsync(applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault(), newPassword);
                        if (!isValidNewPass)
                        {
                            //userManager.Options.Password.RequireUppercase = true;
                            result = await userManager.ChangePasswordAsync(applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault(), currentPassword, newPassword);
                        }
                        else
                        {
                            result = null;
                            return Json(-2);
                        }
                    }
                    else
                    {
                        return Json(-1);
                    }
                    if (result.Succeeded)
                    {
                        //IConnProviderOBI connProviderOBI;
                        conn.Open();
                        transactionOBI = conn.BeginTransaction();
                        ProfileService profileService = new ProfileService(null, null, null, connProviderOBI);

                        profileService.OBIChangePassword(conn, transactionOBI, newPassword, applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().UserName);

                        ApplicationUser applicationUser = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault();
                        applicationUser.LastChangedPasswordDate = DateTime.Now;
                        applicationDbContext.Update(applicationUser);
                        applicationDbContext.SaveChanges();
                        transaction.Commit();
                        transactionOBI.Commit();
                        conn.Dispose();
                        return Json(1);
                    }
                    else
                    {
                        Dictionary<string, object> dictionary = new Dictionary<string, object>();
                        ArrayList arrayList = new ArrayList();
                        foreach (var error in result.Errors)
                        {
                            arrayList.Add(error.Description);
                        }
                        dictionary.Add("passError", arrayList);
                        return Json(dictionary);
                    }
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    transactionOBI.Rollback();
                    conn.Dispose();
                    return Json("0000");
                }
            }
        }
        [HttpGet]
        public IActionResult MustChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> MustChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            using (IDbContextTransaction transaction = applicationDbContext.Database.BeginTransaction())
            {
                IDbConnection conn = connProviderOBI.Connection;
                IDbTransaction transactionOBI = null;
                try
                {
                    applicationDbContext.People.ToList();
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

                    var isValid = await userManager.CheckPasswordAsync(applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault(), currentPassword);
                    IdentityResult result;
                    if (isValid)
                    {
                        var isValidNewPass = await userManager.CheckPasswordAsync(applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault(), newPassword);
                        if (!isValidNewPass)
                        {
                            //userManager.Options.Password.RequireUppercase = true;
                            result = await userManager.ChangePasswordAsync(applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault(), currentPassword, newPassword);
                        }
                        else
                        {
                            result = null;
                            return Json(-2);
                        }
                    }
                    else
                    {
                        return Json(-1);
                    }
                    if (result != null && result.Succeeded)
                    {
                        conn.Open();
                        transactionOBI = conn.BeginTransaction();
                        ProfileService profileService = new ProfileService(null, null, null, connProviderOBI);

                        ApplicationUser applicationUser = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault();
                        applicationUser.LastMustChangedPasswordDate = DateTime.Now;
                        if (applicationUser.MustChangePassword == true)
                        {
                            applicationUser.MustChangePassword = false;
                        }
                        applicationDbContext.Update(applicationUser);
                        applicationDbContext.SaveChanges();

                        profileService.OBIChangePassword(conn, transactionOBI, newPassword, applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().UserName);

                        transaction.Commit();
                        // return RedirectToAction("index", "home");
                        transactionOBI.Commit();
                        conn.Dispose();

                        return Json(1);
                    }
                    else
                    {
                        Dictionary<string, object> dictionary = new Dictionary<string, object>();

                        ArrayList arrayList = new ArrayList();
                        foreach (var error in result.Errors)
                        {
                            arrayList.Add(error.Description);
                        }
                        dictionary.Add("passError", arrayList);
                        return Json(dictionary);
                    }
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    transactionOBI.Rollback();
                    conn.Dispose();
                    return Json("0000");
                }
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RecoveryPassword(long nationalNumber, int employeeNumber)
        {
            string password;

            RandomPasswordGenerator randomPasswordGenerator = new RandomPasswordGenerator();

            password = randomPasswordGenerator.GeneratePassword(true, true, true, true, 16);



            var user = applicationDbContext.applicationUsers.Where(c => c.UserName == Convert.ToString(employeeNumber) && c.IdNumber == nationalNumber).SingleOrDefault();
            
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //int personId = appDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            if (user != null)
            {
                IdentityUser identityUser = applicationDbContext.applicationUsers.Where(c => c.Id == user.Id).SingleOrDefault();
                userManager.Options.Password.RequiredLength = 5;
                userManager.Options.Password.RequiredUniqueChars = 0;
                userManager.Options.Password.RequireNonAlphanumeric = false;
                userManager.Options.Password.RequireUppercase = false;
                userManager.Options.Password.RequireLowercase = false;
                var token = await userManager.GeneratePasswordResetTokenAsync(identityUser);

                IdentityResult result = await userManager.ResetPasswordAsync(applicationDbContext.applicationUsers.Where(c => c.Id == user.Id).SingleOrDefault(), token, password);
                if (result.Succeeded)
                {
                    var people = applicationDbContext.People.ToList();
                    ApplicationUser applicationUser = applicationDbContext.applicationUsers.Where(c => c.Id == user.Id).SingleOrDefault();
                    applicationUser.LastResetPasswordBy = applicationDbContext.applicationUsers.Where(c => c.UserName == Convert.ToString(employeeNumber)).SingleOrDefault().People.PeopleId;
                    applicationUser.LastResetPasswordDate = DateTime.Now;
                    applicationDbContext.Update(applicationUser);
                    applicationDbContext.SaveChanges();
                    var message = new Message(new string[] { user.Email }, "بازیابی پسورد سامانه مدیریت عملکرد", string.Format("<body style='direction: rtl;color:green'><h2> کاربر گرامی پسورد شما با موفقیت به شرح ذیل بازیابی گردید.</h2><h2>{0}</h2></body>",
                    password));
                    emailSender.SendEmail(message);
                    userManager.Options.Password.RequiredLength = 5;
                    userManager.Options.Password.RequiredUniqueChars = 2;
                    userManager.Options.Password.RequireNonAlphanumeric = true;
                    userManager.Options.Password.RequireUppercase = true;
                    userManager.Options.Password.RequireLowercase = true;
                    return Json(1);
                }
            }
            return Json(0);
        }
    }
}

using Ecommerce.BLL.Interfaces;
using Ecommerce.DAL.Entities;
using Ecommerce.DTOs;
using Ecommerce.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class AcountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManger;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AcountController(UserManager<ApplicationUser> userManger, SignInManager<ApplicationUser> signInManager )
        {
            this.userManger = userManger;
            this.signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManger.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var password = await userManger.CheckPasswordAsync(user, model.Password);
                    if (password)
                    {
                        var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Product");
                        }
                    }

                }

            }

            ViewData["ErrorMessage"] = "Email Or Password Is Invalid";
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {

                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    IsAgree = model.IsAgree
                };

                var result = await userManger.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult ForgetPassword()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManger.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var token = await userManger.GeneratePasswordResetTokenAsync(user); //Token is valid to this user

                    var resetPasswordLink = Url.Action("ResetPassword", "Acount", new { Email = model.Email, Token = token }, Request.Scheme);
                    //https://localhost:44354/Acount/ResetPassword?Email=ahmed@gmail.com&token=sefsf098ffs
                    
                    var email = new Email()
                    {
                        Title = "Reset Password",
                        To = model.Email,
                        Body = resetPasswordLink
                    };

                    EmailSetting.SendEmail(email);
                    return RedirectToAction(nameof(CompleteForgetPassword));
                }

                ModelState.AddModelError(string.Empty, "Email is not Existed!");

            }
            return View(model);
        }

        public IActionResult CompleteForgetPassword()
        {
            return View();
        }

        public IActionResult ResetPassword(string email, string token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManger.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await userManger.ResetPasswordAsync(user, model.Token, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(ResetPasswordDone));
                    }

                    foreach (var Error in result.Errors)
                        ModelState.AddModelError(string.Empty, Error.Description);
                    return View(model);
                }

                ModelState.AddModelError(string.Empty, "This Email is not existed");
            }
            return View(model);
        }

        public IActionResult ResetPasswordDone()
        {
            return View();
        }

    }
}

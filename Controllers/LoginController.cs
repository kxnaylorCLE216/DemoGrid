using ExampleGrid.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace CookieAuthenticationDemo.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin([Bind] Users user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var users = new Users();
            var allUsers = users.GetUsers().FirstOrDefault();
            if (users.GetUsers().Any(u => u.OtherID == user.OtherID))
            {
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.GivenName, user.OtherID),
                 };

                var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");

                var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
                HttpContext.SignInAsync(userPrincipal);

                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }
    }
}
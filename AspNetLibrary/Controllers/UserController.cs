using Microsoft.AspNetCore.Mvc;
using AspNetLibrary.Models;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using AspNetLibrary.Helpers;

namespace AspNetLibrary.Controllers
{
    public class UserController : Controller
    {
        private UserDbContext _userContext;

        private IHelper _helper;

        public UserController(UserDbContext usercontext,IHelper helper)
        {
            _userContext = usercontext;
            _helper = helper;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Registering(Usera newUser, string cpas, string Password)
        {
            //checking if user registered before with that email
            var registered = _userContext.Users.FirstOrDefault(x => x.Email == newUser.Email);
			bool isLetter = Password.Any(x => char.IsLetter(x));
			bool isDigit = Password.Any(x => char.IsDigit(x));

			

			if (registered != null)
            {
                TempData["status"] = "This email already in use!";
                return RedirectToAction("Register");
            }

            else
            {
                if (cpas.Length < 8)
                {
                    TempData["status"] = "Password cannot be less than 8 characters!";
                    return RedirectToAction("Register");
                }

				else if (isLetter == false)
				{
					TempData["status"] = "Your password must contain letter";
					return RedirectToAction("Register");
				}

				else if (isDigit == false)
				{
					TempData["status"] = "Your password must contain number";
					return RedirectToAction("Register");
				}
                else if (cpas != Password)
                {
					TempData["status"] = "Passwords are not matching!";
					return RedirectToAction("Register");
				}
				else
                {
					newUser.Password = _helper.Hash(newUser.Password.ToString());
					_userContext.Users.Add(newUser);
					_userContext.SaveChanges();
					HttpContext.Session.SetString("user", "userlogin");
					return RedirectToAction("Index", "Book");
				}

                

            }
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Logining(Usera newUser)
        {
            //checking if user logins or adminlogins
            var isUser = _userContext.Users.FirstOrDefault(x => x.Email != "admin@admin.com" && x.Email == newUser.Email && x.Password == _helper.Hash(newUser.Password));
            var isAdmin = _userContext.Users.FirstOrDefault(x => x.Email == "admin@admin.com" && x.Password == _helper.Hash(newUser.Password));


            if (isUser != null)
            {
                HttpContext.Session.SetString("user", "userlogin");
                return RedirectToAction("Index", "Book");
            }
            else
            {
                if (isAdmin != null)
                {
                    HttpContext.Session.SetString("admin", "adminlogin");
                    return RedirectToAction("Index", "Admin");
                }
                TempData["status"] = "Incorrect Login Information!";
                return RedirectToAction("Login");

            }


        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }


    }
}

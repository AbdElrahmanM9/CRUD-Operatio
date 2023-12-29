using CRUD_Operations.Models;
using CRUD_Operations.ViewModel;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace CRUD_Operations.Controllers
{
    public class LoginController : Controller
    {
        //private CRUDEntities db = new CRUDEntities();
        //private readonly UserManager<User> _userManager;
        //private readonly SignInManager<User> _signInManager;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly CRUDEntities _Context;
        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public LoginController()
        {
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(UserModel user)
        {
            if (ModelState.IsValid)
            {
                User user1 = new User();
                user1.UserName = user.Name;
                user1.PasswordHash = user.Password;

                var User = await _userManager.FindByEmailAsync(user1.UserName);
                if (User != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(User, user.Password, false);

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(User, isPersistent: false);

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.Error = "User Not Found";
                    ModelState.AddModelError(string.Empty, "User Not Found");
                }
            }
            return View(user);
        }
        //[HttpPost]
        //public async Task<ActionResult> Login(User model)
        //{
        //    var checkUser = db.Users.Where(s => s.UserName == model.UserName).FirstOrDefault();
        //    if (checkUser != null)
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(model.UserName, model.PasswordHash, false, false);
        //        if (SignInResult.Success.Succeeded)
        //            return RedirectToAction("Index", "Home");
        //        else
        //            ModelState.AddModelError("", "Invalid login attempt.");
        //        return View(model);
        //    }
        //    ModelState.AddModelError("", "Invalid login attempt.");
        //    return View(model);
        //}
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.UserName = model.Name;
                user.PasswordHash = model.Password;
                user.RoleID = 1;
                var result = await _userManager.CreateAsync(user, user.PasswordHash);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }
                return null;
            }

            return View(model);
        }

    }
}
namespace Minate.Controllers
{
    using System.Web;
    using System.Web.Mvc;

    using Extensions;
    using Services.Interfaces;
    using DomainModel.Entities;

    [HandleError]
    public class RegistrationController : Controller
    {
        private readonly IMembershipService _membershipService;

        public RegistrationController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken(Salt = "register")]
        public ActionResult Index([Bind(Include = "Username, Password")]User user, string confirmPassword, bool terms, string minateCaptcha, string attempt)
        {
            if(!terms)
                ModelState.AddModelError("terms", "You must accept the terms of use");

            if (!VerifyAndExpireCaptcha(HttpContext, minateCaptcha, attempt))
                ModelState.AddModelError("attempt", "The text doesn't match the CAPTCHA.");

            if(!string.IsNullOrEmpty(confirmPassword) && !string.IsNullOrEmpty(user.Password) && !ValidateNewPasswords(user.Password, confirmPassword)) 
            {
                ModelState.AddModelError("Password", "Passwords don't match");
                ModelState.AddModelError("confirmPassword", "You must confirm the password");
            }

            if (ModelState.IsValid)
            {
                 if(_membershipService.CreateUser(user))
                    return View("Success", user);

                 ModelState.AddModelError("Username", "Username already exists");
            }

            return View();
        }

        [NonAction]
        private static bool ValidateNewPasswords(string password, string confirmPassword)
        {
            return string.Equals(password, confirmPassword);
        }

        [NonAction]
        private static bool VerifyAndExpireCaptcha(HttpContextBase ctx, string challengeGuid, string attemptedSolution)
        {
            var key = HtmlHelperExtensions.SessionKeyPrefix + challengeGuid;
            var solution = (string) ctx.Session[key];
            ctx.Session.Remove(key);

            return ((solution != null) && (string.Equals(attemptedSolution, solution)));
        }
    }
}

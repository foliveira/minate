namespace Minate.Controllers
{
    using System;
    using System.Web;
    using System.Linq;
    using System.Web.Mvc;
    using System.Linq.Expressions;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;


    using Extensions;
    using Exceptions;
    using Services.Interfaces;
    using DomainModel.Entities;
    using DomainModel.Repositories.Interfaces;

    [HandleError]
    public class AccountController : Controller
    {
        private readonly UserRepository _usersRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMembershipService _membershipService;

        public AccountController(UserRepository userRepository, IAuthenticationService authenticationService, IMembershipService membershipService)
        {
            _usersRepository = userRepository;
            _authenticationService = authenticationService;
            _membershipService = membershipService;
        }

        [HttpGet, Authorize]
        public ActionResult Index(int? userid)
        {
            Expression<Predicate<User>> byId = (u => u.Identifier == userid.Value);
            Expression<Predicate<User>> byName = (u => u.Username.Equals(User.Identity.Name));

            var users = _usersRepository.FetchBy(userid.HasValue ? byId.Compile() : byName.Compile());

            if (users.Any())
            {
                if (userid.HasValue)
                    ViewData["hideLinks"] = true;

                return View(users.First());
            }

            throw new UserNotFoundException();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken(Salt = "login")]
        public ActionResult Login(string username, string password, bool remember, string ReturnUrl)
        {
            if (!_membershipService.ValidateUser(username, password))
            {
                ModelState.AddModelError("_FORM", "There was an error with the login. Please try again.");
                return View();
            }

            _authenticationService.Login(username, remember);

            if (string.IsNullOrEmpty(ReturnUrl))
                return RedirectToAction("Index", "Home");

            return Redirect(ReturnUrl);
        }

        [HttpGet, Authorize]
        public ActionResult Logout()
        {
            _authenticationService.Logout();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet, Authorize]
        public ActionResult Edit()
        {
            var user = _usersRepository.FetchBy(u => u.Username.Equals(User.Identity.Name)).First();

            return View(user);
        }

        [HttpPost, Authorize, ValidateAntiForgeryToken(Salt = "edit")]
        public ActionResult Edit(User user)
        {
            if(ModelState.IsValid) 
            {
                _usersRepository.Update(u => u.Identifier == user.Identifier, user);
                _usersRepository.SubmitChanges();

                ViewData["success"] = "Your profile has been updated!";
            }

            return View(user);
        }

        [HttpGet, Authorize]
        public ActionResult List(int page)
        {
            const int pageSize = 10;

            var numUsers = _usersRepository.FetchAll().Count();
            var users = _usersRepository.FetchAll().Skip((page - 1) * pageSize).Take(pageSize);

            ViewData["TotalPages"] = (int)Math.Ceiling((double)numUsers / pageSize);
            ViewData["CurrentPage"] = page;

            return Request.IsAjaxRequest() ? View("MembersList", users) : View(users);
        }

        [HttpGet, Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost, Authorize, ValidateAntiForgeryToken(Salt = "changepassword")]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if(!_membershipService.ValidateUser(User.Identity.Name, currentPassword))
                ModelState.AddModelError("currentPassword", "The current password is invalid.");

            if(!string.Equals(newPassword, confirmPassword))
                ModelState.AddModelError("_FORM", "The new password doesn't match the confirmation.");

            if(ModelState.IsValid)
            {
                _membershipService.ChangePassword(User.Identity.Name, currentPassword, confirmPassword);
                ViewData["success"] = "Your password was successfully updated. Don't forget it!";
            }

            return View();
        }

        [HttpGet, Authorize]
        public ActionResult ChangeImage()
        {
            var user = _usersRepository.FetchBy(u => string.Equals(User.Identity.Name, u.Username));
            ViewData["userid"] = user.First().Identifier;
            return View();
        }

        [HttpPost, Authorize, ValidateAntiForgeryToken(Salt = "changeimage")]
        public ActionResult ChangeImage(int userid, HttpPostedFileBase image)
        {
            var user = _usersRepository.GetUser(userid);
            var uploadBytes = new byte[image.ContentLength];
            image.InputStream.Read(uploadBytes, 0, image.ContentLength);
         
            user.Image = new User.ImageFile {Content = uploadBytes, Type = image.ContentType};

            _usersRepository.SubmitChanges();

            ViewData["userid"] = userid;

            return View();
        }

        [HttpGet]
        public FileResult Image(int userid)
        {
            int height = 150, width = 150;
            var user = _usersRepository.GetUser(userid);
            var userImage = user.Image;

            if(userImage == null)
                return File("~/Content/images/default.png", "image/png");

            var ms = new MemoryStream(userImage.Content);
            var image = System.Drawing.Image.FromStream(ms);

            if(image.Width > image.Height)
                height = image.Height * width / image.Width;
            else
                width = image.Width * height / image.Height;
            
            new Bitmap(image, width, height).Save(ms = new MemoryStream(), ImageFormat.Png);
            ms.Position = 0;

            return File(ms, "image/png");
        }
    }
}

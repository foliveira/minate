namespace Minate.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.Web.Mvc;

    using Extensions;
    using DomainModel.Repositories.Interfaces;

    [Authorize]
    [HandleError]
    public class FriendsController : Controller
    {
        private readonly UserRepository _usersRepository;

        public FriendsController(UserRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public ActionResult List(int page)
        {
            const int pageSize = 10;
            var user = _usersRepository.FetchBy(u => string.Equals(User.Identity.Name, u.Username)).First();

            var numFriends = user.Friends.Count();
            var friends = user.Friends.Skip((page - 1)*pageSize).Take(pageSize);

            ViewData["TotalPages"] = (int)Math.Ceiling((double)numFriends / pageSize);
            ViewData["CurrentPage"] = page;
            ViewData["Pending"] = friends.Where(f => !f.Confirmed);

            friends = friends.Where(f => f.Confirmed);

            return Request.IsAjaxRequest() ? View("FriendsList", friends) : View(friends);
        }

        [AcceptAjax, HttpPost, ActionName("Add")]
        public JsonResult AddFriend(int id)
        {
            var friend = _usersRepository.GetUser(id);
            var user = _usersRepository.FetchBy(u => string.Equals(User.Identity.Name, u.Username)).First();

            user.AddFriend(friend);

            return Json(new {subject = "An invite", predicate = "was sent to " + friend.Username});
        }

        [AcceptAjax, HttpPost, ActionName("Confirm")]
        public JsonResult ConfirmFriend(int id)
        {
            var friend = _usersRepository.GetUser(id);
            var user = _usersRepository.FetchBy(u => string.Equals(User.Identity.Name, u.Username)).First();

            user.ConfirmFriend(friend);

            return Json(new {subject = "You've confirmed", predicate = friend.Username + " as a friend"});
        }

        [AcceptAjax, HttpPost, ActionName("Remove")]
        public JsonResult RemoveFriend(int id)
        {
            var friend = _usersRepository.GetUser(id);
            var user = _usersRepository.FetchBy(u => string.Equals(User.Identity.Name, u.Username)).First();

            user.RemoveFriend(friend);
            friend.RemoveFriend(user);

            return Json(new { subject = friend.Username, predicate = "is no longer your friend" });
        }
    }
}

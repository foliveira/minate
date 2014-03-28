

namespace Minate.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.Web.Mvc;
    using System.Linq.Expressions;

    using DomainModel.Entities;
    using DomainModel.Repositories.Interfaces;
    using Extensions;

    [Authorize]
    [HandleError]
    public class GameController : Controller
    {
        private readonly GameRepository _gamesRepository;
        private readonly UserRepository _usersRepository;

        public GameController(GameRepository gamesRepository, UserRepository userRepository)
        {
            _gamesRepository = gamesRepository;
            _usersRepository = userRepository;
        }

        [HttpGet]
        public ActionResult List()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Statistics(int? userid)
        {
            Expression<Predicate<User>> byId = (u => u.Identifier == userid.Value);
            Expression<Predicate<User>> byName = (u => u.Username.Equals(User.Identity.Name));

            var user = _usersRepository.FetchBy(userid.HasValue ? byId.Compile() : byName.Compile()).First();
            var games = _gamesRepository.FetchBy(g => g.HasPlayer(user.Username) && g.Finished);

            if (games.Any())
            {
                var gamesWon = games.Where(g => g.Winner.UserId == user.Identifier);

                ViewData["totalGames"] = games.Count();
                ViewData["gamesWon"] = gamesWon.Count();
                ViewData["gamesLost"] = games.Count() - gamesWon.Count();
                ViewData["maxMines"] = games.Max(g => g.Players.Where(p => p.UserId == user.Identifier).First().Mines);
                ViewData["minMines"] = games.Min(g => g.Players.Where(p => p.UserId == user.Identifier).First().Mines);
            }

            return View();
        }

        [HttpGet]
        public ActionResult CurrentGames(int page)
        {
            const int pageSize = 10;

            var user = _usersRepository.FetchBy(u => string.Equals(u.Username, User.Identity.Name)).First();
            var games = _gamesRepository.FetchBy(g => g.HasPlayer(user.Username) && g.Full && !g.Finished);

            var numGames = games.Count();
            games = games.Skip((page - 1) * pageSize).Take(pageSize);

            ViewData["TotalPages"] = (int)Math.Ceiling((double)numGames / pageSize);
            ViewData["CurrentPage"] = page;

            return Request.IsAjaxRequest() ? View("CurrentGamesList", games) : View(games);
        }

        [HttpGet]
        public ActionResult PendingGames(int page)
        {
            const int pageSize = 10;

            var user = _usersRepository.FetchBy(u => string.Equals(u.Username, User.Identity.Name)).First();
            var games = _gamesRepository.FetchBy(g => g.HasPlayer(user.Username) && !g.Full);

            var numGames = games.Count();
            games = games.Skip((page - 1) * pageSize).Take(pageSize);

            ViewData["TotalPages"] = (int)Math.Ceiling((double)numGames / pageSize);
            ViewData["CurrentPage"] = page;

            return Request.IsAjaxRequest() ? View("PendingGamesList", games) : View(games);
        }

        [HttpGet]
        public ActionResult FinishedGames(int page)
        {
            const int pageSize = 10;

            var user = _usersRepository.FetchBy(u => string.Equals(u.Username, User.Identity.Name)).First();
            var games = _gamesRepository.FetchBy(g => g.HasPlayer(user.Username) && g.Finished);

            var numGames = games.Count();
            games = games.Skip((page - 1) * pageSize).Take(pageSize);

            ViewData["TotalPages"] = (int)Math.Ceiling((double)numGames / pageSize);
            ViewData["CurrentPage"] = page;

            return Request.IsAjaxRequest() ? View("FinishedGamesList", games) : View(games);
        }

        [HttpGet]
        public ActionResult Play(int? gameid)
        {
            var user = _usersRepository.FetchBy(p => p.Username.Equals(User.Identity.Name)).First();

            if (!gameid.HasValue)
            {
                var games = _gamesRepository.FetchBy(g => (!g.Full && !g.HasPlayer(user.Username)));

                var game = games.Any() ? games.First() : _gamesRepository.Create();

                if (game.Identifier == -1)
                    _gamesRepository.Add(game);

                var player = game.Join(user);

                ViewData["ClientID"] = player.Identifier;

                return View(game);
            } 
            else
            {
                var game = _gamesRepository.FetchBy(g => g.Identifier == gameid.Value).First();

                ViewData["ClientID"] = game.Players.Where(p => string.Equals(p.Name, user.Username)).First().Identifier;

                return View(game);
            }
        }

        [HttpGet]
        public ActionResult Invite(int userid)
        {
            var game = _gamesRepository.Create();
            var user = _usersRepository.FetchBy(u => string.Equals(u.Username, User.Identity.Name)).First();
            var friend = _usersRepository.GetUser(userid);

            _gamesRepository.Add(game);

            ViewData["ClientID"] = game.Join(user).Identifier;
            game.Join(friend);

            return View("Play", game);
        }

        [HttpGet]
        public ActionResult Replay(int gameid)
        {
            var game = _gamesRepository.FetchBy(g => g.Identifier == gameid && g.Finished).First();

            return View(game);
        }
        
        #region Action Methods for AJAX Goodness

        [AcceptAjax, HttpPost, ActionName("OpenCells")]
        public JsonResult GetCellsToOpen(int gameId, int? x, int? y)
        {
            var game = _gamesRepository.GetGame(gameId);

            if (!x.HasValue || !y.HasValue)
                return Json(new {});

            var cellsToOpen = game.MakePlay(x.Value, y.Value);
            return (cellsToOpen != null) ? Json(cellsToOpen) : Json(new {});
        }

        [AcceptAjax, HttpPost, ActionName("CurrentPlayer")]
        public JsonResult GetCurrentPlayer(int gameId)
        {
            var game = _gamesRepository.GetGame(gameId);

            return game.Finished ? Json(new { game.Winner }) : Json(game.CurrentPlayer);
        }

        [AcceptAjax, HttpPost, ActionName("Plays")]
        public JsonResult GetPlaysSince(int gameId, int? sincePlay)
        {
            var game = _gamesRepository.GetGame(gameId);

            return !sincePlay.HasValue ? Json(game.Plays) : Json(game.Plays.Where(p => p.Identifier > sincePlay.Value));
        }

        [AcceptAjax, HttpPost, ActionName("SpecificPlay")]
        public JsonResult GetSpecificPlay(int gameId, int play)
        {
            var game = _gamesRepository.GetGame(gameId);

            return (play < 0 || play >= game.Plays.Count) ? Json(new {}) : Json(game.Plays.ElementAt(play));
        }

        [AcceptAjax, HttpPost, ActionName("Players")]
        public JsonResult GetPlayersInGame(int gameId, int? sincePlayer)
        {
            var game = _gamesRepository.GetGame(gameId);

            if(sincePlayer.HasValue)
            {
                return game.Full
                           ? Json(new {state = "full", list = game.Players.Where(p => p.Identifier > sincePlayer.Value)})
                           : Json(game.Players.Where(p => p.Identifier > sincePlayer.Value));
            }

            return Json(game.Players);
        }

        [AcceptAjax, HttpPost, ActionName("Forfeit")]
        public JsonResult PlayerForfeit(int id)
        {
            var game = _gamesRepository.GetGame(id);

            game.Forfeit(User.Identity.Name);

            return Json(new {subject = "You've", predicate = string.Format("forfeited game {0}", id) });
        }

        [AcceptAjax, HttpPost, ActionName("Remove")]
        public JsonResult RemoveGame(int id)
        {
            var games = _gamesRepository.Delete(g => g.Identifier == id && !g.Full && g.HasPlayer(User.Identity.Name));
            return Json(new {
                                subject = string.Format("Game {0}", id),
                                predicate = games.Any() ? "was deleted" : "wasn't deleted"
                            });
        }

        [AcceptAjax, HttpPost, ActionName("Messages")]
        public JsonResult GetChatMessages(int gameId, int? sinceMessage)
        {
            var game = _gamesRepository.GetGame(gameId);
            var messages = !sinceMessage.HasValue
                               ? game.Messages
                               : game.Messages.Where(m => m.Identifier == sinceMessage.Value);

            return Json(messages.Select(m => m.ToString()));
        }

        [AcceptAjax, HttpPost, ActionName("SendMessage")]
        public JsonResult AddChatMessage(int gameId, dynamic msg)
        {
            var game = _gamesRepository.GetGame(gameId);
            var player = game.Players.Where(p => p.Identifier == msg.Id).Single();
            var message = game.AddMessage(player, msg.Text);

            return Json(new {state = "success", message = message.ToString()});
        }

        #endregion
    }
}
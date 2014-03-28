namespace Minate
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using MvcContrib.Castle;
    using Castle.Windsor;

    using Extensions;

    public class MvcApplication : System.Web.HttpApplication
    {
        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");

            #region Registration Routes

            routes.MapRoute(null, "Register", new {controller = "Registration", action = "Index"});

            #endregion

            #region Friends Routes

            routes.MapRoute(null, "Profile/Friends/{page}", new { controller = "Friends", action = "List", page = 1 });

            routes.MapRoute(null, "Friends/Add", new { controller = "Friends", action = "Add" });

            routes.MapRoute(null, "Friends/Confirm", new { controller = "Friends", action = "Confirm" });

            routes.MapRoute(null, "Friends/Remove", new { controller = "Friends", action = "Remove" });

            #endregion

            #region Account Routes

            routes.MapRoute(null, "Profile/Edit", new { controller = "Account", action = "Edit" });

            routes.MapRoute(null, "Profile/ChangeImage", new { controller = "Account", action = "ChangeImage" });

            routes.MapRoute(null, "Profile/ChangePassword", new { controller = "Account", action = "ChangePassword" });

            routes.MapRoute(null, "Profile/Image/{userid}", new { controller = "Account", action = "Image"});

            routes.MapRoute(null, "Profile/{userid}", new {controller = "Account", action = "Index", userid = (int?)null});

            routes.MapRoute(null, "Login", new {controller = "Account", action = "Login"});

            routes.MapRoute(null, "Logout", new {controller = "Account", action = "Logout"});

            routes.MapRoute(null, "Members/{page}", new {controller = "Account", action = "List", page = 1});

            #endregion

            #region Admin Routes



            #endregion

            #region Security Routes

            routes.MapRoute(null, "Captcha/{challengeGuid}", new {controller = "Security", action = "Captcha"});

            #endregion

            #region Game Routes

            routes.MapRoute(null, "Games", new {controller = "Game", action = "List"});

            routes.MapRoute(null, "Games/Statistics/{userid}",
                            new {controller = "Game", action = "Statistics", userid = (int?) null});

            routes.MapRoute(null, "Games/Current/{page}", new {controller = "Game", action = "CurrentGames", page = "1"});

            routes.MapRoute(null, "Games/Pending/{page}", new {controller = "Game", action = "PendingGames", page = "1"});

            routes.MapRoute(null, "Games/Finished/{page}",
                            new {controller = "Game", action = "FinishedGames", page = "1"});

            routes.MapRoute(null, "Game/Invite/{userid}", new {controller = "Game", action = "Invite"});

            routes.MapRoute(null, "Game/Play/Random", new {controller = "Game", action = "Play", gameid = (int?) null});

            routes.MapRoute(null, "Game/Play/{gameid}", new {controller = "Game", action = "Play"});

            routes.MapRoute(null, "Game/Replay/{gameid}", new {controller = "Game", action = "Replay"});

            routes.MapRoute(null, "Game/Open/{gameId}",
                            new {controller = "Game", action = "OpenCells", gameId = "-1"});

            routes.MapRoute(null, "Game/CurrentPlayer/{gameId}",
                            new {controller = "Game", action = "CurrentPlayer", gameId = "-1"});

            routes.MapRoute(null, "Game/Plays/{gameId}",
                            new {controller = "Game", action = "Plays", gameId = "-1"});

            routes.MapRoute(null, "Game/SpecificPlay/{gameId}", new { controller = "Game", action = "SpecificPlay", gameId = "-1" });

            routes.MapRoute(null, "Game/Players/{gameId}",
                            new { controller = "Game", action = "Players", gameId = "-1" });

            routes.MapRoute(null, "Game/Forfeit", new { controller = "Game", action = "Forfeit" });

            routes.MapRoute(null, "Game/Remove", new { controller = "Game", action = "Remove" });

            routes.MapRoute(null, "Game/Messages/{gameId}", new {controller = "Game", action = "Messages"});

            routes.MapRoute(null, "Game/SendMessage/{gameId}", new {controller = "Game", action = "SendMessage"});

            #endregion

            #region Homepage Routes

            routes.MapRoute(null, string.Empty, new { controller = "Home", action = "Index" });

            routes.MapRoute(null, "Terms", new {controller = "Home", action = "Terms"});

            routes.MapRoute(null, "About", new {controller = "Home", action = "About"});

            #endregion

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                            new {controller = "Home", action = "Index", id = ""}, 
                            new {controller = @"[^\.]*"});

        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(((WindsorContainer)null).PopulateFromConfig()));
        }
    }
}
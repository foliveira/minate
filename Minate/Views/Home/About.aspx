<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	About
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>About Minate</h2>

    <div id="what-is">
    <b>Minate</b> is a place where you can play with your friends and build your own social network around a game. Name a site where you can do the same!!<br />
    Facebook?! It doesn't count! It really sucks...
    </div>
    <div id="what-game">
    Well the game that is the building block for this site is a simple one: <p>Minesweepers Flags!</p> You haven't played it yet? Ahh, that sucks to be you, but whatever,
    I'll try to explain it to you in a very simple way.
    <img src="../../Content/images/cropped/board.png" /><br />
    The purpose of this game is to find as many mines as you can. You read it right... More mines. The player wins if he can find more mines than his opponent.
    </div>
    <div id="what-social">
    If it depended only on playing then <b>Minate</b> wouldn't be much different from other online game sites. But with the magnificient game, comes the opportunity
    to build your own social network, with friends form all around the world (or the universe) that play this game, and you can even compare your results!
    <img src="../../Content/images/cropped/register.png" /> <br />
    </div>
    <div id="what-profile">
    You can build your profile with your information and share it with all your friends. You can even upload a photo of you (or anything else) and be immediately
    recognized by all your friends. So don't stay faceless, and go customize your profile!
    <img src="../../Content/images/default.png" />
    </div>
</asp:Content>
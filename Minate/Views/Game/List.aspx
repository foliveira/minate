<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Games List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Games</h2>
    
    <div id="games">
        <div class="games-row">
            <div id="progress">
                <%= Html.ActionLink("Games in Progress", "CurrentGames", "Game")%><br /><img height="149" src="../../Content/images/cropped/inprogress.png" width="149" />
            </div>
            
            <div id="pending">
                <%= Html.ActionLink("Pending Games", "PendingGames", "Game") %><br /><img src="../../Content/images/cropped/pending.png" width="150" height="150" />
            </div>
        </div>
        
        <div class="games-row">
            <div id="completed">
                <%= Html.ActionLink("Finished Games", "FinishedGames", "Game") %><br /><img src="../../Content/images/cropped/finished.png" width="150" height="149" />
            </div>
            
            <div id="random">
                <%= Html.ActionLink("Play against random opponent", "Play", "Game", new {gameid = (int?)null}, null) %><br /><img src="../../Content/images/cropped/random.png" width="151" height="150" />
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="jQuery" runat="server">
</asp:Content>

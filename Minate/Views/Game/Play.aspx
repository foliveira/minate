<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Minate.DomainModel.Entities.Game>" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
	Game
</asp:Content>

<asp:Content ID="cssContent" ContentPlaceHolderID="Stylesheets" runat="server">
    <%= Html.Stylesheet("~/Content/minate.less.css") %>
</asp:Content>

<asp:Content ID="ScriptsContent" ContentPlaceHolderID="Scripts" runat="server">
	<%= Html.ScriptInclude("~/Scripts/jquery.polling.js") %>
	
    <%= Html.ScriptInclude("~/Scripts/Minate.View.js")%>
    <%= Html.ScriptInclude("~/Scripts/Minate.Model.js") %>
    <%= Html.ScriptInclude("~/Scripts/Minate.Controller.js") %>
    
</asp:Content>

<asp:Content ID="initContent" ContentPlaceHolderID="jQuery" runat="server">
    <script type="text/javascript">
		Urls.Register("GetCellsToOpen", function(x, y) { return "<%= Url.Action("OpenCells", new { gameId = Model.Identifier }) %>?x=" + x + "&y=" + y; });
		Urls.Register("GetCurrentPlayer", function() { return "<%= Url.Action("CurrentPlayer", new { gameId = Model.Identifier }) %>"; });
		Urls.Register("GetPlayersInGame", function(player) { return "<%= Url.Action("Players", new { gameId = Model.Identifier }) %>?sincePlayer=" + player; });
		Urls.Register("GetPlaysSince", function(play) { return "<%= Url.Action("Plays", new { gameId = Model.Identifier }) %>?sincePlay=" + play; });
        Urls.Register("GetChatMessages", function(message) { return "<%= Url.Action("Messages", new { gameId = Model.Identifier }) %>?sinceMessage=" + message });
        Urls.Register("SendChatMessage", function() { return "<%= Url.Action("SendMessage", new { gameId = Model.Identifier }) %>" });
    </script>
	
	<script type="text/template" id="draw_status">
		<div class="status">
			<p class="name"><!= player.Name !></p>
			<p class="mines"><!= player.Mines !></p>
		</div>
	</script>
	
	<script type="text/template" id="end_game">
		<! if(status) { !>
			<p>Congratulations! You've just won the game!</p>
		<! } else { !>
			<p>Oh bummer! Seems that you've lost the game this time!</p>
		<! } !>
	</script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Game <%: Model.Identifier %></h2>
	<div id="dialog">
		<!-- The end of game dialog  goes here! But only when needed. -->
	</div>
	
	<div class="players">
		<!-- Players go here! When they arrive at the game, of course! -->
	</div>
    <div class="mineboard">
        <!-- Board goes here, programatically -->
    </div>    
    
    <script type="text/javascript">
        minateController = new MinateController(<%= Model.Board.Width %>, <%= Model.Board.Height %>, <%= Model.Board.TotalBombs %>, <%= ViewData["ClientID"] %>);
        minateController.PollForPlayers();
    </script>
</asp:Content>


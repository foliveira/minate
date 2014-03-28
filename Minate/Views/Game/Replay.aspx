<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Minate.DomainModel.Entities.Game>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Game Replay
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

<asp:Content ID="Content5" ContentPlaceHolderID="jQuery" runat="server">
<script type="text/javascript">
		Urls.Register("GetPlayersInGame", function(player) { return "<%= Url.Action("Players", new { gameId = Model.Identifier }) %>?sincePlayer=" + player; });
		Urls.Register("GetSpecificPlay", function(play) { return "<%= Url.Action("SpecificPlay", new { gameId = Model.Identifier }) %>?play=" + play; });
        Urls.Register("GetCurrentPlayer", function() { return "<%= Url.Action("CurrentPlayer", new { gameId = Model.Identifier }) %>"; });
    </script>
	
	<script type="text/template" id="draw_status">
		<div class="status">
			<p class="name"><!= player.Name !></p>
			<p class="mines">0</p>
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

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Game <%= Model.Identifier %> Replay</h2>

    <div class="players">
		<!-- Players go here! When they arrive at the game, of course! -->
	</div>
    <div class="mineboard">
        <!-- Board goes here, programatically -->
    </div>    
    
    <%= Html.Button("nextPlay", "Next Play", HtmlButtonType.Button, "minateController.ReplayForward()", new {id="slim"})%>
    
    <script type="text/javascript">
        minateController = new MinateController(<%= Model.Board.Width %>, <%= Model.Board.Height %>, <%= Model.Board.TotalBombs %>, -1);
        minateController.ReplayPlayers();
    </script>
</asp:Content>

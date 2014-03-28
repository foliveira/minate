<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Minate.DomainModel.Entities.Game>>" %>

<table id="games">
    <thead>
	    <tr>
		    <th scope="col">Game</th>
            <th scope="col">Result</th>
		</tr>
	</thead>

	<!-- Table body -->
	<tbody>
		<% foreach (var game in Model) { %>
            <tr>
                <td>
                    <%: Html.ActionLink(string.Format("Game {0} against {1}", game.Identifier, game.Players.Where(p => !string.Equals(Page.User.Identity.Name, p.Name)).First().Name), "Replay", "Game", new {gameid = game.Identifier}, null) %>
                </td>
                <td>
                    You <%= string.Equals(Page.User.Identity.Name, game.Winner.Name) ? "won!" : "lost..." %>
                </td>
            </tr>
        <% } %>
	</tbody>
</table>
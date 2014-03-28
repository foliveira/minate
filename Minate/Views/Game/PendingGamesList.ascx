<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Minate.DomainModel.Entities.Game>>" %>

<table id="games">
    <thead>
	    <tr>
		    <th scope="col">Game</th>
			<th scope="col">Action</th>
		</tr>
	</thead>

	<!-- Table body -->
	<tbody>
		<% foreach (var game in Model) { %>
            <tr>
                <td>
                    <%= string.Format("Game {0}", game.Identifier) %>
                </td>
                <td>
                    <a href="#<%=game.Identifier%>" class="remove">remove</a>
                </td>
            </tr>
        <% } %>
	</tbody>
</table>

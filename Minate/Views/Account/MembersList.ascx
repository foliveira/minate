<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Minate.DomainModel.Entities.User>>" %>
<table id="members">
    <thead>
	    <tr>
		    <th scope="col">Username</th>
			<th scope="col">Action</th>
		</tr>
	</thead>

	<!-- Table body -->
	<tbody>
		<% foreach (var user in Model) { %>
            <tr>
                <td>
                    <%= Html.ActionLink(Html.Encode(user.Username), "Index", "Account", new { userid = !user.Username.Equals(Page.User.Identity.Name) ? user.Identifier : (int?)null}, null) %>
                </td>
                <td>
                    <% if (!string.Equals(user.Username, Page.User.Identity.Name) && !user.Friends.Where(f => string.Equals(f.Username, Page.User.Identity.Name)).Any()) { %>
                        <a href="#<%=user.Identifier%>" class="add">add</a>
                    <% } else {%>
                        <%: string.Equals(user.Username, Page.User.Identity.Name) ? "It's you!" : "Already a friend!"%>
                    <% } %>
                </td>
            </tr>
        <% } %>
	</tbody>
</table>

<div class="pages">
    <%= Html.PageLinks((int)ViewData["CurrentPage"], (int)ViewData["TotalPages"], p => Url.Action("List", new {page = p})) %>
</div>


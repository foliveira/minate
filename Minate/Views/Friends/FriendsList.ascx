<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Minate.DomainModel.Entities.User+Friend>>" %>
<%@ Import Namespace="Minate.DomainModel.Entities" %>

<h2>Friends</h2>
<table id="friends">
        <thead>
		    <tr>
			    <th scope="col">Username</th>
				<th scope="col">Action</th>
                <th scope="col">Game</th>
			</tr>
		</thead>
        <tbody>
            <% foreach (var friend in Model.Where(f => f.Confirmed)) { %>
                <tr>
                    <td>
                        <%= Html.ActionLink(Html.Encode(friend.Username), "Index", "Account", new { userid = friend.UserId }, null)%>
                    </td>
                    <td>
                        <a href="#<%=friend.UserId%>" class="remove">remove</a>
                    </td>
                    <td>
                        <%= Html.ActionLink("invite", "Invite", "Game", new {userid = friend.UserId}, null) %>
                    </td>
                </tr>
            <% } %>
		</tbody>
</table>

<% if(((IEnumerable<User.Friend>)ViewData["Pending"]).Any()) { %>
    <h2>Pending Confirmation</h2>
     <table id="friends-confirm">
            <thead>
    		    <tr>
    			    <th scope="col">Username</th>
    				<th scope="col" colspan="2">Action</th>
    			</tr>
    		</thead>
            <tbody>
                <% foreach (var friend in (IEnumerable<User.Friend>)ViewData["Pending"]) { %>
                    <tr>
                        <td>
                            <%= Html.ActionLink(Html.Encode(friend.Username), "Index", "Account", new { userid = friend.UserId }, null)%>
                        </td>
                        <td>
                            <a href="#<%=friend.UserId%>" class="confirm">confirm</a>
                        </td>
                        <td>
                            <a href="#<%= friend.UserId %>" class="reject">reject</a>
                        </td>
                    </tr>
                <% } %>
    		</tbody>
    </table>
<% } %>

<div class="pages">
    <%= Html.PageLinks((int)ViewData["CurrentPage"], (int)ViewData["TotalPages"], p => Url.Action("List", new {page = p})) %>
</div>
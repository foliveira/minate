<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<li>
    <%= Html.MenuLink("Home", "Home") %>
</li>
<li>
    <%= Html.MenuLink("About", "Home", "About") %>
</li>


<% if (!Request.IsAuthenticated) { %>

<li>
    <%= Html.MenuLink("Register", "Registration") %>
</li>

<% } else { %>

<li>
    <%= Html.MenuLink("Games", "Game", "List") %>
</li>
<li>
    <%= Html.MenuLink("Members", "Account", "List") %>
</li>

<% } %>
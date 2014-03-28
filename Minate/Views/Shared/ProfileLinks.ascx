<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div id="profile-menu">
    <ul>
        <li>
            <%= Html.MenuLink("Edit Profile", "Account", "Edit") %>
        </li>
        <li>
            <%= Html.MenuLink("Change Password", "Account", "ChangePassword") %>
        </li>
        <li>
            <%= Html.MenuLink("Change Image", "Account", "ChangeImage") %>
        </li>
        <li>
            <%= Html.MenuLink("Manage Friends", "Friends", "List", new {page = 1}) %>
        </li>
        <li>
            <%= Html.MenuLink("User Statistics", "Game", "Statistics") %>
        </li>
    </ul>
</div>
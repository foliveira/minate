<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="login">
    <%
        if (Request.IsAuthenticated) {
    %>
            Welcome <b><%= Html.ActionLink(Page.User.Identity.Name, "Index", "Account", new {userid = (int?)null}, null) %></b>!
            [ <%= Html.ActionLink("Logout", "Logout", "Account")%> ]
    <%
        }
        else {
    %> 
            <div id="loginLink">
                <a href="#">Login</a>
            </div>
            
            <div id="menuLoginForm" class="hide">
                    <% using (Html.BeginForm("Login", "Account")) { %>                        
                        <%= Html.AntiForgeryToken("login")%>
                        <%= Html.Hidden("returnUrl", Url.Action("Index", "Home")) %>
                        
                        <b><%= Html.Label("Username: ")%></b>
                        <%= Html.TextBox("username")%>
                        <%= Html.ValidationMessage("username", "*")%>
                        
                        <b><%= Html.Label("Password: ") %></b>
                        <%= Html.Password("password") %>
                        <%= Html.ValidationMessage("password", "*") %>

                        <b><%= Html.Label("Remember?") %></b>
                        <%= Html.CheckBox("remember") %>

                        <%= Html.Button("login", "Login", HtmlButtonType.Submit, null, new {id="slim"}) %>
                    <% } %>
    <%
        }
    %>
</div>
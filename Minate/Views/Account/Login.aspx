<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Login
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="basic" class="minate-form">
        <% using (Html.BeginForm()) { %>
            <h1>Login</h1>
            <p><%= Html.ValidationSummary("There were some errors with the login. Correct them and try again.") %></p>
            
            <div class="spacer"></div>
            <%= Html.AntiForgeryToken("login")%>
            <%= Html.Hidden("returnUrl", "Home") %>
            
            <%= Html.Label("Username: ")%>
            <%= Html.TextBox("username")%>
            <%= Html.ValidationMessage("username", "*")%>
            
            <%= Html.Label("Password: ") %>
            <%= Html.Password("password") %>
            <%= Html.ValidationMessage("password", "*") %>

            <%= Html.Label("Should I remember you?") %>
            <%= Html.CheckBox("remember") %>

            <%= Html.Button("login", "Login", HtmlButtonType.Submit) %>
            <div class="spacer"></div>
        <% } %>
    </div>
</asp:Content>


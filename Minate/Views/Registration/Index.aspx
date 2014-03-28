<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Minate Registration
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="basic" class="minate-form">
        <% using (Html.BeginForm()) { %>
            <h1>Register</h1>
            <p><%= Html.ValidationSummary("Registration was unsuccessful. Please correct the errors and try again.") %></p>
            
            <%= Html.AntiForgeryToken("register") %>
            
            <%= Html.Label("Username:") %>
            <%= Html.TextBox("Username") %>
            <%= Html.ValidationMessage("Username", "*")%>

            <%= Html.Label("Password:") %>
            <%= Html.Password("Password") %>
            <%= Html.ValidationMessage("Password", "*")%>

            <%= Html.Label("Confirm Password:") %>
            <%= Html.Password("confirmPassword") %>
            <%= Html.ValidationMessage("confirmPassword", "*")%>
            
            <%= Html.Captcha("minateCaptcha") %>
            <label>Verification text<span class="small">Please enter the letters into the box</span></label>
            <%= Html.TextBox("attempt") %>
            <%= Html.ValidationMessage("attempt", "*")%>
            <div class="spacer"></div>
            <label for="terms" name="terms">I accept the <%= Html.ActionLink("terms", "Terms", "Home") %> of use</label>
            <%= Html.CheckBox("terms", false) %>
            <%= Html.ValidationMessage("terms", "*") %>

            <%= Html.Button("register", "Register", HtmlButtonType.Submit) %>
            <div class="spacer"></div>
            
        <% } %>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Profile.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Change Password
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% if(ViewData["success"] != null) { %>
        <div id="success-form-post" align="center">
            <%= ViewData["success"] %>
        </div>
    <% } %>
    
    <div id="basic" class="minate-form">
        <% using (Html.BeginForm()) { %>
            <h1>Change password</h1>
            <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>
            
            <div class="spacer"></div>
            <%= Html.AntiForgeryToken("changepassword") %>
            
            <%= Html.Label("Current Password:") %>
            <%= Html.Password("currentPassword")%>
            <%= Html.ValidationMessage("currentPassword", "*")%>

            <%= Html.Label("New Password:") %>
            <%= Html.Password("newPassword")%>
            <%= Html.ValidationMessage("newPassword", "*")%>

            <%= Html.Label("Confirm Password:") %>
            <%= Html.Password("confirmPassword") %>
            <%= Html.ValidationMessage("confirmPassword", "*")%>
            
            <%= Html.Button("change", "Change", HtmlButtonType.Submit) %>
            <%= Html.ActionLink("Back to Profile", "Index") %>
            
            <div class="spacer"></div>
        <% } %>
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Profile.Master" Inherits="System.Web.Mvc.ViewPage<Minate.DomainModel.Entities.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit Profile
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% if(ViewData["success"] != null) { %>
        <div id="success-form-post" align="center">
            <%= ViewData["success"] %>
        </div>
    <% } %>

    <div id="basic" class="minate-form">
        <% using (Html.BeginForm()) { %>
            <h1><%: Model.Username %> - Edit profile</h1>
            <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>
            
            <div class="spacer"></div>
            <%= Html.AntiForgeryToken("edit") %>            
            <%= Html.Hidden("Identifier", Model.Identifier) %>
            <%= Html.Hidden("Username", Model.Username) %>
            
            <%= Html.Label("Name:") %>
            <%= Html.TextBox("Name", Model.Name) %>
            <%= Html.ValidationMessage("Name", "*") %>
            
            <%= Html.Label("Email:") %>
            <%= Html.TextBox("Email", Model.Email) %>
            <%= Html.ValidationMessage("Email", "*") %>
            
            <%= Html.Label("Location:") %>
            <%= Html.TextBox("Location", Model.Location) %>
            <%= Html.ValidationMessage("Location", "*") %>
            
            <%= Html.Label("Birthday:") %>
            <%= Html.TextBox("Birthday", String.Format("{0:d}", Model.Birthday)) %>
            <%= Html.ValidationMessage("Birthday", "*") %>
            
            <%= Html.Label("About Me:") %>
            <%= Html.TextArea("Biography", Model.Biography) %>
            <%= Html.ValidationMessage("Biography", "*") %>
            
            <%= Html.Button("edit", "Edit", HtmlButtonType.Submit) %>
            
            <%= Html.ActionLink("Back to Profile", "Index") %>
            <div class="spacer"></div>
        <% } %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="jQuery" runat="server">
    <script type="text/javascript">
        $(function() {
            $("#Birthday").datepicker({ dateFormat: "dd-mm-yy", defaultDate: $.datepicker.parseDate("d m y", "1 1 1975") });
        });
    </script>
</asp:Content>


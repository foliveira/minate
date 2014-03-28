<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Profile.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Change Image
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="basic" class="minate-form">
        <% using (Html.BeginForm("ChangeImage", "Account", FormMethod.Post, new { enctype = "multipart/form-data" })) { %>
            <h1>Change Image</h1>
            
            <%=Html.AntiForgeryToken("changeimage")%>
            <%= Html.Hidden("userid", ViewData["userid"]) %>
            
            <%= Html.Label("Upload a new image:")%>
            <input type="file" name="image" />
            
            <%= Html.Button("change", "Change", HtmlButtonType.Submit) %>
        <% } %>
        
    </div>
    
    <div id="current-image" align="center">
        <h2>Current Image</h2><br /><img src="<%=Url.Action("Image", new {userid = ViewData["userid"]})%>" />
    </div>

</asp:Content>

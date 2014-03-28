<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Profile.Master" Inherits="System.Web.Mvc.ViewPage<Minate.DomainModel.Entities.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: Model.Username %>'s Profile
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%:Model.Username%></h2>
    
    <p>
        <img src="<%=Url.Action("Image", new {userid = Model.Identifier})%>" />
    </p>
    
    <div id="profile">
        <ul>
            <li>
                <div class="desc"><%=Html.Label("real name")%></div>
                <span><%:Model.Name%></span>
            </li>

            <li>
                <div class="desc"><%=Html.Label("e-mail")%></div>
                <span><%:Model.Email%></span>
            </li>

            <li>
                <div class="desc"><%=Html.Label("from")%></div>
                <span><%:Model.Location%></span>
            </li>

            <li>
                <div class="desc"><%=Html.Label("age")%></div>
                <span>
                <% if (Model.Birthday != null) { %>
                    <%=(DateTime.Now.Year - Model.Birthday.Value.Year) -
                                  ((DateTime.Now.Month <= Model.Birthday.Value.Month && DateTime.Now.Day < Model.Birthday.Value.Day) ? 1 : 0)%>
                <% } %>
                </span>
            </li>

            <li>
                <div class="desc"><%=Html.Label("short bio")%></div>
                <span><%:Model.Biography%></span>
            </li>   
        </ul>
    </div> 
    
    <% if(ViewData["hideLinks"] != null) { %>
        <p><%= Html.ActionLink("User Statistics", "Statistics", "Game", new { userid = Model.Identifier }, null)%></p>
    <% } %>
</asp:Content>



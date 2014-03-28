<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Minate.DomainModel.Entities.Game>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: Page.User.Identity.Name %> Pending Games
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Pending Games</h2>
    
    <% Html.RenderPartial("PendingGamesList"); %>
    
    <div class="pages">
        <%= Html.PageLinks((int)ViewData["CurrentPage"], (int)ViewData["TotalPages"], p => Url.Action("PendingGames", new {page = p})) %>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Stylesheets" runat="server">
    <%= Html.Stylesheet("~/Content/jquery.undoable.less.css")%>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Scripts" runat="server">
    <%= Html.ScriptInclude("~/Scripts/jquery.undoable.js") %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="jQuery" runat="server">
    <script type="text/javascript">
        $(function () {
            $("a.remove").undoable({url: "<%= Url.Action("Remove", "Game") %>"});
            
            $(".pages a").live("click", function() {
                $.get($(this).attr("href"), function(response) {
                    $("#games").replaceWith($("#games", response));
                      
                    $("a.remove").undoable({url: "<%= Url.Action("Remove", "Game") %>"});
                });
            });
        });
    </script>
</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Minate.DomainModel.Entities.Game>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: Page.User.Identity.Name %> Finished Games
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Finished Games</h2>
    
    <% Html.RenderPartial("FinishedGamesList"); %>

    <div class="pages">
        <%= Html.PageLinks((int)ViewData["CurrentPage"], (int)ViewData["TotalPages"], p => Url.Action("FinishedGames", new {page = p})) %>
    </div>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="jQuery" runat="server">
    <script type="text/javascript">
        $(function () {            
            $(".pages a").live("click", function() {
                $.get($(this).attr("href"), function(response) {
                    $("#games").replaceWith($("#games", response));
                });
            });
        });
    </script>
</asp:Content>
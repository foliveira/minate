<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Minate.DomainModel.Entities.User>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.RenderPartial("MembersList"); %>
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
            initPage();
            
            $(".pages a").live("click", function() {
                $.get($(this).attr("href"), function(response) {
                    $("#members").replaceWith($("#members", response));
                    
                    initPage();
                });
            });
        });
        
        function initPage() {
            $("#members tr:even").addClass("alternate");
            $("a.add").undoable({url: "<%= Url.Action("Add", "Friends") %>"});
        }
    </script>
</asp:Content>
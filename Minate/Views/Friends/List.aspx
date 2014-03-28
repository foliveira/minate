<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Profile.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Minate.DomainModel.Entities.User+Friend>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Your Friends
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <% Html.RenderPartial("FriendsList"); %>

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
                    $("#friends").replaceWith($("#friends", response));
                    
                    initPage();
                });
            });
        });
        
        function initPage() {    
            $("#friends tr:even").addClass("alternate");
            $("a.remove").undoable({url: "<%= Url.Action("Remove", "Friends") %>"});
            
            $("a.confirm").undoable({url: "<%= Url.Action("Confirm", "Friends") %>"});
            $("a.reject").undoable({url: "<%= Url.Action("Remove", "Friends") %>"});
        }
    </script>
</asp:Content>
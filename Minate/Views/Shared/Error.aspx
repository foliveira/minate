<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HandleErrorInfo>" %>

<asp:Content ID="IndexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Oops! There seems to have a problem with this request.
</asp:Content>

<asp:Content ID="IndexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Our beloved nano-workers will now investigate what's wrong </h1>
    
    <h2>Please check if you're authenticated, if your not trying to access a restricted area or if you aren't disconnected from the internet (hey it can happen!)</h2>
</asp:Content>


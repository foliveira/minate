<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Minate.DomainModel.Entities.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Registration Success
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Account Created</h2>
    
    <p>You are now registered <b><%: Model.Username %></b>! You can start playing by <%= Html.ActionLink("logging in", "Login", "Account") %>.</p>
    <p>Then you can <%= Html.ActionLink("edit", "Edit", "Account") %> your personal preferences, add friends and start playing!</p>
</asp:Content>

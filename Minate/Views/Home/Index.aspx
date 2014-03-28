<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="IndexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Minate Online
</asp:Content>

<asp:Content ID="IndexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="welcome">
        Welcome to <b>Minate</b> the most awesome online game ever done...this week! If you're already registred you can proceed to the 
        <%= Html.ActionLink("Login", "Login", "Account") %> page and play right away.<br />
        If you're not registered, why won't you read the following testimonies and learn more about <b>Minate</b>. Right after the break.
        
    </div>
    <div id="testimonies">
        <p>Hear what users from all around globe have to say about Minate</p>
        <blockquote>
            O Minate foi, a meu ver, a melhor coisa que me aconteceu desde que descobri que posso dizer <b>"Carrega Benfica"</b> em qualquer situação social! <b>Carrega Benfica!</b> 
            <p>-Sócio do Glorioso nº45431</p>
        </blockquote>
        
        <blockquote>
            Since I started playing <b>Minate</b> I've abandoned all my addictions: porn, alcohol and even pot. <b>Minate</b> is truly a gift from the gods!
            <p>-Random guy</p>
        </blockquote>
    </div>
    
    <div id="know-more">
        To know more about the fantastic game that <b>Minate</b> be sure to navigate to our <%= Html.ActionLink("About", "About") %> page.
    </div>
</asp:Content>

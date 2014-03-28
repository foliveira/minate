<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Profile.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Player Statistics
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Statistics</h2>

<table id="stats">
    <thead>
	    <tr>
		    <th scope="col">Field</th>
            <th scope="col">Value</th>
		</tr>
	</thead>

	<!-- Table body -->
	<tbody>
            <tr>
                <td>
                    Total Games
                </td>
                <td>
                    <%= ViewData["totalGames"] ?? 0 %>
                </td>
            </tr>
            <tr>
                <td>
                    Games Won
                </td>
                <td>
                    <%= ViewData["gamesWon"] ?? 0%>
                </td>
            </tr>
            <tr>
                <td>
                    Games Lost
                </td>
                <td>
                    <%= ViewData["gamesLost"] ?? 0%>
                </td>
            </tr>
            <tr>
                <td>
                    Max Mines
                </td>
                <td>
                    <%= ViewData["maxMines"] ?? 0%>
                </td>
            </tr>
            <tr>
                <td>
                    Min Mines
                </td>
                <td>
                    <%= ViewData["minMines"] ?? 0%>
                </td>
            </tr>
	</tbody>
</table>
</asp:Content>



<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create a new repository
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Create a new repository</h2>

	<% using (Html.BeginForm()) { %>

	<input type="text" name="project" />.git<br /><br />
	<input type="submit" value="Create a new repository" />

	<% } %>

</asp:Content>
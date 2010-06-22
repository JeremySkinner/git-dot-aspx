<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<GitAspx.ViewModels.DirectoryListViewModel>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
	Repositories
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
	<h1><%: Model.RepositoriesDirectory %></h1>

	<ul id="repositories">
		<% foreach (var directory in Model.Directories) { %>
			<li>
				<%: directory.Name %>
			</li>
		<% } %>
	</ul>
</asp:Content>

<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<GitAspx.ViewModels.DirectoryListViewModel>" MasterPageFile="~/Views/Shared/Site.Master" %>
<%@ Import Namespace="GitAspx" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
	Repositories
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
	<h1><%: Model.RepositoriesDirectory %></h1>

	<ul id="repositories">
		<% foreach (var directory in Model.Directories) { %>
			<li>
				<a class="repository" href="javascript:void(0)" title="<%: Url.ProjectUrl(directory.Name) %>"><%: directory.Name %></a>
			</li>
		<% } %>
	</ul>

	<%: Html.ActionLink("Create a new bare repository", "Create") %>

	<div class="jqmWindow" id="dialog">
		<a href="#" class="jqmClose">Close</a>
		<p>
			Clone the repository using this command: 
			<pre>git clone <input type="text" id="repository-url"  size="40" /></pre>
		</p>
	</div>

<script type="text/javascript">
	$(function () {
		$('#dialog').jqm();

		$('#repository-url').click(function () {
			$(this).select();
		});

		$('a.repository').click(function () {
			var url = $(this).attr('title');
			$('#dialog').jqmShow();
			$('#repository-url').val(url).focus().select();
		});
	});
</script>

</asp:Content>

<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<GitAspx.ViewModels.DirectoryListViewModel>" MasterPageFile="~/Views/Shared/Site.Master" %>
<%@ Import Namespace="GitAspx" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
	Repositories
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
	
	<div class="repositoryContainer">
	
		<h1><%: Model.RepositoriesDirectory %></h1>		

		<a href="javascript:void(0)" class="createRepository">Create a new bare repository</a>
		<div class="clear"></div>

		<ul id="repositories">
			<% foreach (var directory in Model.Directories) { %>
				<li>
					<a class="repository" href="javascript:void(0)" title="<%: Url.ProjectUrl(directory.Name) %>"><%: directory.Name %></a>
				</li>
			<% } %>
		</ul>

	</div>

	<div class="jqmWindow" id="dialog">
		<div class="title">Clone the repository using this command <a href="#" class="jqmClose"><img src="../../Content/images/close.png" alt="Close" /></a></div>		
		<div class="content">			
			<pre>git clone <input type="text" id="repository-url" /></pre>
		</div>
	</div>

	<div class="jqmWindow" id="createRepositoryDialog">
		<div class="title">Create a new repository <a href="#" class="jqmClose"><img src="../../Content/images/close.png" alt="Close" /></a></div>		
		<div class="content">
		<% using (Html.BeginForm("Create", "DirectoryList")) { %>

			<input type="text" name="project" />.git<br /><br />
			<input type="submit" value="Create a new repository" class="button" />

		<% } %>
		</div>
	</div>

<script type="text/javascript">
	$(function () {
		$('#dialog').jqm();
		$('#createRepositoryDialog').jqm();

		$('#repository-url').click(function () {
			$(this).select();
		});

		$('a.repository').click(function () {
			var url = $(this).attr('title');
			$('#dialog').jqmShow();
			$('#repository-url').val(url).focus().select();
		});

		$('a.createRepository').click(function () {
			$('#createRepositoryDialog').jqmShow();
		});
	});
</script>

</asp:Content>

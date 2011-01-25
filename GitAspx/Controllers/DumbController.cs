namespace GitAspx.Controllers {
	using System;
	using System.IO;
	using System.Web.Mvc;
	using System.Web.SessionState;
	using GitAspx.Lib;

	[SessionState(SessionStateBehavior.Disabled)]
	public class DumbController : BaseController {
		readonly RepositoryService repositories;

		public DumbController(RepositoryService repositories) {
			this.repositories = repositories;
		}

		public ActionResult GetTextFile(string project) {
			return WriteFile(project, "text/plain");
		}

		public ActionResult GetInfoPacks(string project) {
			return WriteFile(project, "text/plain; charset=utf-8");
		}

		public ActionResult GetLooseObject(string project) {
			return WriteFile(project, "application/x-git-loose-object");
		}

		public ActionResult GetPackFile(string project) {
			return WriteFile(project, "application/x-git-packed-objects");
		}

		public ActionResult GetIdxFile(string project) {
			return WriteFile(project, "application/x-git-packed-objects-toc");
		}

		private ActionResult WriteFile(string project, string contentType) {
			Response.WriteNoCache();
			Response.ContentType = contentType;
			var repo = repositories.GetRepository(project);

			string path = Path.Combine(repo.GitDirectory(), GetPathToRead(project));

			if(! System.IO.File.Exists(path)) {
				return new NotFoundResult();
			}

			Response.WriteFile(path);

			return new EmptyResult();
		}

		private string GetPathToRead(string project) {
			int index = Request.Url.PathAndQuery.IndexOf(project) + project.Length + 1;
			return Request.Url.PathAndQuery.Substring(index);
		}
	}
}
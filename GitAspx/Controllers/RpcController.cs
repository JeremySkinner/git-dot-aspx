namespace GitAspx.Controllers {
	using System;
	using System.IO;
	using System.Web.Mvc;
	using GitAspx.Lib;
	using GitSharp.Core;
	using GitSharp.Core.Transport;

	// Handles project/git-upload-pack and project/git-receive-pack
	public class RpcController : BaseController {
		RepositoryService repositories;

		public RpcController(RepositoryService repositories) {
			this.repositories = repositories;
		}

		public ActionResult UploadPack(string project) {
			return ExecuteRpc(project, Rpc.UploadPack, repository => {
				using (var pack = new UploadPack(repository)) {
					pack.setBiDirectionalPipe(false);
					pack.Upload(Request.InputStream, Response.OutputStream, Response.OutputStream);
				}
			});
		}

		public ActionResult ReceivePack(string project) {
			return ExecuteRpc(project, Rpc.ReceivePack, repository => {
				var pack = new ReceivePack(repository);
				pack.setBiDirectionalPipe(false);
				pack.receive(Request.InputStream, Response.OutputStream, Response.OutputStream);
			});
		}

		private ActionResult ExecuteRpc(string project, Rpc rpc, Action<Repository> action) {
			if (!HasAccess(Rpc.UploadPack, checkContentType: true)) {
				return new ForbiddenResult();
			}

			Response.ContentType = string.Format("application/x-git-{0}-result", rpc.GetDescription());
			WriteNoCache();

			var repository = repositories.GetRepository(project);

			if (repository == null) {
				return new NotFoundResult();
			}

			using (repository) {
				action(repository);
			}

			return new EmptyResult();
		}
	}
}
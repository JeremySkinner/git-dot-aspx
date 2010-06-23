namespace GitAspx.Controllers {
	using System;
	using System.IO;
	using System.Web.Mvc;
	using GitAspx.Lib;
	using GitSharp.Core.Transport;

	// Handles project/git-upload-pack and project/git-receive-pack
	public class RpcController : BaseController {
		RepositoryService repositories;

		public RpcController(RepositoryService repositories) {
			this.repositories = repositories;
		}

		public ActionResult UploadPack(string project) {
			if(!HasAccess(Rpc.UploadPack, checkContentType: true)) {
				return new ForbiddenResult();
			}

			Response.ContentType = "application/x-git-upload-pack-result";
			WriteNoCache();

			var repository = repositories.GetRepository(project);

			if(repository == null) {
				return new NotFoundResult();
			}

			using (repository) {
				using (var pack = new UploadPack(repository)) {
					pack.setBiDirectionalPipe(false);
					pack.Upload(Request.InputStream, Response.OutputStream, Response.OutputStream);
				}
			}

			return new EmptyResult();
		}

		public ActionResult ReceivePack(string project) {
			if (!HasAccess(Rpc.ReceivePack, checkContentType: true)) {
				return new ForbiddenResult();
			}

			Response.ContentType = "application/x-git-receive-pack-result";
			WriteNoCache();

			var repository = repositories.GetRepository(project);

			if(repository == null) {
				return new NotFoundResult();
			}

			using (repository) {
				var pack = new ReceivePack(repository);
				pack.setBiDirectionalPipe(false);
				pack.receive(Request.InputStream, Response.OutputStream, Response.OutputStream);
			}

			return new EmptyResult();
		}
	}
}
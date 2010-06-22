namespace GitAspx.Controllers {
	using System;
	using System.IO;
	using GitAspx.Lib;
	using GitSharp.Core.Transport;

	// Handles project/git-upload-pack and project/git-receive-pack
	public class RpcController : BaseController {
		RepositoryService repositories = new RepositoryService();

		public void UploadPack(string project) {
			Response.ContentType = "application/x-git-upload-pack-result";
			WriteNoCache();

			using (var repository = repositories.GetRepository(project))
			using (var pack = new UploadPack(repository)) {
				pack.setBiDirectionalPipe(false);
				pack.Upload(Request.InputStream, Response.OutputStream, Response.OutputStream);
			}
		}

		public void ReceivePack(string project) {
			Response.ContentType = "application/x-git-receive-pack-result";
			WriteNoCache();

			using (var repository = repositories.GetRepository(project)) {
				var pack = new ReceivePack(repository);
				pack.setBiDirectionalPipe(false);
				pack.receive(Request.InputStream, Response.OutputStream, Response.OutputStream);
			}
		}
	}
}
namespace GitAspx.Controllers {
	using System.Web.Mvc;
	using GitAspx.Lib;
	using GitSharp.Core.Transport;

	// Handles /project/info/refs
	public class InfoRefsController : BaseController {
		RepositoryService repositories;

		public InfoRefsController(RepositoryService repositories) {
			this.repositories = repositories;
		}

		public ActionResult Execute(string project, string service) {
			service = service.Replace("git-", "");

			Response.StatusCode = 200;
			Response.ContentType = string.Format("application/x-git-{0}-advertisement", service);
			WriteNoCache();

			var repository = repositories.GetRepository(project);
			
			if(repository == null) {
				return new NotFoundResult();
			}

			using (repository) {

				Response.Write(PktWrite("# service=git-{0}\n", service));
				Response.Write(PktFlush());

			
				if (service == "upload-pack") {
					var pack = new UploadPack(repository);
					pack.sendAdvertisedRefs(new RefAdvertiser.PacketLineOutRefAdvertiser(new PacketLineOut(Response.OutputStream)));
				}

				else if (service == "receive-pack") {
					var pack = new ReceivePack(repository);
					pack.SendAdvertisedRefs(new RefAdvertiser.PacketLineOutRefAdvertiser(new PacketLineOut(Response.OutputStream)));
				}
			}

			return new EmptyResult();
		}
	}
}
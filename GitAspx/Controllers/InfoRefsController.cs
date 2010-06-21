using GitAspx.Lib;

namespace GitAspx.Controllers
{
	using System.IO;
	using GitSharp.Core;
	using GitSharp.Core.Transport;

	// Handles /project/info/refs
	public class InfoRefsController : BaseController
	{
		RepositoryService repositories = new RepositoryService();
		GitCommands git = new GitCommands();

		public void Execute(string project, string service)
		{
			service = service.Replace("git-", "");

			Response.ContentType = string.Format("application/x-git-{0}-advertisement", service);
			WriteNoCache();
			Response.Write(PktWrite("# service=git-{0}\n", service));
			Response.Write(PktFlush());

			using(var repository = repositories.GetRepository(project)) {

				if(service == "upload-pack") {
					var pack = new UploadPack(repository);
					pack.sendAdvertisedRefs(new RefAdvertiser.PacketLineOutRefAdvertiser(new PacketLineOut(Response.OutputStream)));
				}

				else if(service == "receive-pack") {
					var pack = new ReceivePack(repository);
					pack.SendAdvertisedRefs(new RefAdvertiser.PacketLineOutRefAdvertiser(new PacketLineOut(Response.OutputStream)));
				}
			}

			/*using (var repository = new Repository(new DirectoryInfo("C:\\Projects\\gittest\\simplegit"))) {
				var pack = new UploadPack(repository);
				pack.setBiDirectionalPipe(false);
				pack.sendAdvertisedRefs(new RefAdvertiser.PacketLineOutRefAdvertiser(new PacketLineOut(Response.OutputStream)));
			}*/

		/*	var refs = git.Invoke("{0} --stateless-rpc --advertise-refs .", service);
			Response.ContentType = string.Format("application/x-git-{0}-advertisement", service);
			Response.StatusCode = 200;
			WriteNoCache();
			Response.Write(PktWrite("# service=git-{0}\n", service));
			Response.Write(PktFlush());
			Response.Write(refs);
			Response.Flush();*/
		}
	}
}
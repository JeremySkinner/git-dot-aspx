using GitAspx.Lib;

namespace GitAspx.Controllers
{
	// Handles /project/info/refs
	public class InfoRefsController : BaseController
	{
		GitCommands git = new GitCommands();

		public void Execute()
		{
			string serviceName = GetServiceType();
			var refs = git.Invoke("{0} --stateless-rpc --advertise-refs .", serviceName);
			Response.StatusCode = 200;
			Response.ContentType = string.Format("application/x-git-{0}-advertisement", serviceName);
			WriteNoCache();
			Response.Write(PktWrite("# service=git-{0}\n", serviceName));
			Response.Write(PktFlush());
			Response.Write(refs);
			Response.Flush();
		}

		private string GetServiceType() {
			var service = Request.Params["service"];

			if (string.IsNullOrEmpty(service) || service.Length < 4) {
				return null;
			}

			return service.Replace("git-", "");
		}
	}
}
using GitAspx.Lib;

namespace GitAspx.Controllers
{
	// Handles project/git-upload-pack and project/git-receive-pack
	public class RpcController : BaseController
	{
		GitCommands git = new GitCommands();

		public void UploadPack()
		{
			Execute("upload-pack");
		}

		public void ReceivePack()
		{
			Execute("receive-pack");
		}

		private void Execute(string rpc)
		{
			/*var input = ReadBody();
			Response.StatusCode = 200;
			Response.ContentType = string.Format("application/x-git-{0}-result", rpc);
			var process = git.InvokeProcess("{0} --stateless-rpc .", rpc);
			process.StandardInput.Write(input);

			var buffer = new char[8192];
			while(true)
			{
				int read = process.StandardOutput.Read(buffer, 0, buffer.Length);
				if(read <= 0)
				{
					break;
				}
				Response.Write(buffer, 0, read);
			}*/
		}
	}
}
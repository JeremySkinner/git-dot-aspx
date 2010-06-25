#region License

// Copyright 2010 Jeremy Skinner (http://www.jeremyskinner.co.uk)
//  
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
// 
// The latest version of this file can be found at http://github.com/JeremySkinner/git-dot-aspx

#endregion

namespace GitAspx.Controllers {
	using System.Web.Mvc;
	using GitAspx.Lib;
	using GitSharp.Core.Transport;

	// Handles /project/info/refs
	public class InfoRefsController : BaseController {
		readonly RepositoryService repositories;

		public InfoRefsController(RepositoryService repositories) {
			this.repositories = repositories;
		}

		public ActionResult Execute(string project, string service) {
			service = service.Replace("git-", "");

			Response.StatusCode = 200;
			Response.ContentType = string.Format("application/x-git-{0}-advertisement", service);
			WriteNoCache();

			var repository = repositories.GetRepository(project);

			if (repository == null) {
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
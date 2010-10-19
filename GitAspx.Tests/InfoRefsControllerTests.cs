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

namespace GitAspx.Tests {
	using System.IO;
	using GitAspx.Controllers;
	using GitAspx.Lib;
	using NUnit.Framework;

	[TestFixture]
	public class InfoRefsControllerTests {
		InfoRefsController controller;

		[SetUp]
		public void Setup() {
			controller = new InfoRefsController(new RepositoryService(TestExtensions.GetAppSettings())).
					FakeContxt();
		}

		[Test]
		public void Gets_upload_pack_advertisement() {
			controller.Execute("test.git", "git-upload-pack");
			controller.Response.ContentType.ShouldContain("application/x-git-upload-pack-advertisement");

			var body = controller.Response.OutputStream.GetString();

			body.SplitOnNewLine()[0].ShouldEqual("001e# service=git-upload-pack");
			body.ShouldContain("0000009514bf0836c3371b740ebad55fbda6223bd7940f20 HEAD");
			body.ShouldContain("multi_ack_detailed");

		}

		[Test]
		public void Gets_receive_pack_advertisement() {
			controller.Execute("test.git", "git-receive-pack");

			controller.Response.ContentType
				.ShouldContain("application/x-git-receive-pack-advertisement");

			var body = controller.Response.OutputStream.GetString();
			body.SplitOnNewLine()[0].ShouldEqual("001f# service=git-receive-pack");
			body.ShouldContain("0000007314bf0836c3371b740ebad55fbda6223bd7940f20 refs/heads/master");
			body.ShouldContain("report-status");
			body.ShouldContain("delete-refs");
			body.ShouldContain("ofs-delta");

		}

		[Test]
		public void Returns_404_when_repository_not_found() {
			var result = controller.Execute("NoSuchProject", "git-receive-pack");
			result.ShouldBe<NotFoundResult>();
		}
	}
}
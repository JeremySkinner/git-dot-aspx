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
			var dir = new DirectoryInfo("../../Repositories");
			this.controller = new InfoRefsController(new RepositoryService(new AppSettings { ReceivePack = true, UploadPack = true, RepositoriesDirectory = dir })).FakeContxt();
		}

		[Test]
		public void Gets_upload_pack_advertisement() {
			/*controller.Execute("test", "git-upload-pack");
			controller.Response.StatusCode.ShouldEqual(200);
			controller.Response.ContentType.ShouldContain("application/x-git-upload-pack-advertisement");

			var body = controller.Response.OutputStream.GetString();

			body.SplitOnNewLine()[0].ShouldEqual("001E# service=git-upload-pack");
			body.ShouldContain("0000009514bf0836c3371b740ebad55fbda6223bd7940f20 HEAD");
			body.ShouldContain("multi_ack_detailed");

*/		}

		[Test]
		public void Gets_receive_pack_advertisement() {
			
		}

		[Test]
		public void Returns_404_when_repository_not_found() {
			
		}
	}
}
namespace GitAspx.Tests {
	using GitAspx.Controllers;
	using GitAspx.Lib;
	using NUnit.Framework;

	[TestFixture]
	public class RpcControllerTests {
		RpcController controller;

		[SetUp]
		public void Setup() {
			controller = new RpcController(new RepositoryService(TestExtensions.GetAppSettings()))
				.FakeContxt();

			controller.AppSettings = TestExtensions.GetAppSettings();
		}


		[Test]
		public void NoAccess_to_UploadPack_when_incorrect_content_type() {
			var result = controller.UploadPack("test");
			result.ShouldBe<ForbiddenResult>();
		}

		[Test]
		public void NoAccess_to_ReceivePack_when_incorrect_content_type() {
			var result = controller.ReceivePack("test");
			result.ShouldBe<ForbiddenResult>();
		}

		[Test]
		public void Not_found_when_wrong_path() {
			controller.Request.ContentType = "application/x-git-receive-pack-request";
			var result = controller.ReceivePack("no-such-project");
			result.ShouldBe<NotFoundResult>();
		}

	}
}
namespace GitAspx.Tests {
	using System;
	using System.Net;
	using NUnit.Framework;
	using System.Linq;

	// These tests are largely ported from GRack.
	// They are all in one file to ensure the webserver is only started once (slooow)
	[TestFixture]
	public class MainTests : BaseTest {
		[Test]
		public void Gets_upload_pack_advertisement() {
			var response = Get("/test/info/refs?service=git-upload-pack");
			response.StatusCode.ShouldEqual(HttpStatusCode.OK);
			response.Headers["Content-Type"].ShouldContain("application/x-git-upload-pack-advertisement");

			var body = response.GetString();

			body.SplitOnNewLine()[0].ShouldEqual("001E# service=git-upload-pack");
			body.ShouldContain("0000009514bf0836c3371b740ebad55fbda6223bd7940f20 HEAD");
			body.ShouldContain("multi_ack_detailed");
		}

		[Test]
		public void Gets_receive_pack_advertisement() {
			var response = Get("/test/info/refs?service=git-receive-pack");
			response.StatusCode.ShouldEqual(HttpStatusCode.OK);
			response.Headers["Content-Type"].ShouldContain("application/x-git-receive-pack-advertisement");

			var body = response.GetString();
			body.SplitOnNewLine()[0].ShouldEqual("001F# service=git-receive-pack");
			body.ShouldContain("0000007314bf0836c3371b740ebad55fbda6223bd7940f20 refs/heads/master");
			body.ShouldContain("report-status");
			body.ShouldContain("delete-refs");
			body.ShouldContain("ofs-delta");
		}

		[Test]
		public void NoAccess_to_UploadPack_when_incorrect_content_type() {
			var response = Post("test/git-upload-pack");
			response.StatusCode.ShouldEqual(HttpStatusCode.Forbidden);
		}

		[Test]
		public void NoAccess_to_ReceivePack_when_incorrect_content_type() {
			var response = Post("test/git-receive-pack");
			response.StatusCode.ShouldEqual(HttpStatusCode.Forbidden);
		}

		[Test]
		public void Not_found_when_wrong_http_method() {
			var response = Get("test/git-receive-pack");
			response.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
		}

		[Test]
		public void Not_found_when_wrong_path() {
			var response = Post("no-such-project/git-receive-pack", contentType: "application/x-git-receive-pack-request");
			response.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
		}
	}
}
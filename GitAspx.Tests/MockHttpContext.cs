namespace GitAspx.Tests {
	using System;
	using System.Collections;
	using System.Collections.Specialized;
	using System.IO;
	using System.Web;
	using Moq;

	public class MockHttpContext : Mock<HttpContextBase> {

		readonly Mock<HttpServerUtilityBase> server = new Mock<HttpServerUtilityBase>();

		public MockHttpContext() {
			HttpRequest = new HttpRequestMock();
			HttpResponse = new HttpResponseMock();

			Setup(c => c.Request).Returns(this.HttpRequest.Object);
			Setup(c => c.Response).Returns(this.HttpResponse.Object);
			Setup(x => x.Session).Returns(new MockSessionState());
			Setup(x => x.Server).Returns(server.Object);
			Setup(x => x.Items).Returns(new Hashtable());
			this.SetupProperty(x => x.User);
		}

		public HttpRequestMock HttpRequest { get; private set; }
		public HttpResponseMock HttpResponse { get; private set; }

		public static HttpContextBase Create() {
			return new MockHttpContext().Object;
		}
	}

	public class HttpRequestMock : Mock<HttpRequestBase> {
		readonly NameValueCollection form = new NameValueCollection();
		readonly NameValueCollection querystring = new NameValueCollection();
		readonly Stream inputStream = new MemoryStream(new byte[8192]);

		/// <summary>
		/// Default Constructor
		/// </summary>
		public HttpRequestMock() {
			SetupProperty(r => r.ContentType);
			Setup(r => r.QueryString).Returns(querystring);
			Setup(r => r.ApplicationPath).Returns("/");
			Setup(r => r.Form).Returns(form);
			Setup(x => x.InputStream).Returns(inputStream);
		}

	}

	public class HttpResponseMock : Mock<HttpResponseBase> {
		readonly Stream outputStream = new MemoryStream(new byte[8192]);
		readonly StreamWriter writer;

		public HttpResponseMock() {
			writer = new StreamWriter(outputStream);
			Setup(x => x.OutputStream).Returns(outputStream);
			SetupProperty(x => x.ContentType);
			SetupProperty(x => x.StatusCode);
			Setup(x => x.Write(It.IsAny<string>())).Callback(new Action<string>(s => {
				writer.Write(s);
			}));
		}
	}

	public class MockSessionState : HttpSessionStateBase {
		private Hashtable hash = new Hashtable();

		public override object this[string name] {
			get { return hash[name]; }
			set { hash[name] = value; }
		}

		public override void Abandon() {
			Clear();
		}

		public override void Remove(string name) {
			hash.Remove(name);
		}

		public override void RemoveAll() {
			Clear();
		}

		public override HttpSessionStateBase Contents {
			get { return this; }
		}

		public override void Clear() {
			hash.Clear();
		}
	}

}
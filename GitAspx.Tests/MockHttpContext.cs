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

			Setup(c => c.Request).Returns(HttpRequest.Object);
			Setup(c => c.Response).Returns(HttpResponse.Object);
			Setup(x => x.Session).Returns(new MockSessionState());
			Setup(x => x.Server).Returns(server.Object);
			Setup(x => x.Items).Returns(new Hashtable());
			SetupProperty(x => x.User);
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
			Setup(x => x.Write(It.IsAny<string>())).Callback(new Action<string>(s => { writer.Write(s); writer.Flush(); }));
		}
	}

	public class MockSessionState : HttpSessionStateBase {
		readonly Hashtable hash = new Hashtable();

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
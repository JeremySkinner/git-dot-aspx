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
	using System.Linq;
	using System.Net;
	using System.Xml.Linq;
	using CassiniDev;
	using NUnit.Framework;

	[TestFixture]
	public class BaseTest : CassiniDevServer {
		//Server server;

		[TestFixtureSetUp]
		public void TestFixtureSetup() {
			ChangeRepo(new DirectoryInfo("..\\..\\Repositories").FullName);
			StartServer("..\\..\\..\\GitAspx");
		}

		[TestFixtureTearDown]
		public void TestFixtureTeardown() {
			StopServer();
			ChangeRepo("C:\\Repositories");
		}

		void ChangeRepo(string repoDir) {
			string path = "..\\..\\..\\GitAspx\\Web.config";

			var doc = XDocument.Load(path);
			doc.Element("configuration").Element("appSettings").Elements("add").Single(
				x => x.Attribute("key").Value == "RepositoriesDirectory")
				.Attribute("value").Value = repoDir;

			doc.Save(path);
		}

		protected HttpWebResponse Get(string url) {
			try {
				url = NormalizeUrl(url);
				var request = (HttpWebRequest) WebRequest.Create(url);
				return (HttpWebResponse) request.GetResponse();
			}
			catch (WebException e) {
				//wtf, why does WebRequest throw for non-200 status codes?!
				return (HttpWebResponse) e.Response;
			}
		}

		public HttpWebResponse Post(string url, string contentType = null) {
			try {
				url = NormalizeUrl(url);
				var request = (HttpWebRequest) WebRequest.Create(url);
				if (contentType != null) {
					request.ContentType = contentType;
				}
				request.Method = "POST";
				return (HttpWebResponse) request.GetResponse();
			}
			catch (WebException e) {
				return (HttpWebResponse) e.Response;
			}
		}
	}
}
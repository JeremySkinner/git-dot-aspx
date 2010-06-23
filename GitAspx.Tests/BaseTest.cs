namespace GitAspx.Tests {
	using System;
	using System.IO;
	using System.Net;
	using System.Web.Configuration;
	using System.Xml.Linq;
	using CassiniDev;
	using NUnit.Framework;
	using System.Linq;

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

		private void ChangeRepo(string repoDir) {
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
				var request = (HttpWebRequest)WebRequest.Create(url);
				return (HttpWebResponse)request.GetResponse();
			}
			catch(WebException e) { //wtf, why does WebRequest throw for non-200 status codes?!
				return (HttpWebResponse) e.Response;
			}
		}

		public HttpWebResponse Post(string url, string contentType = null) {
			try {
				url = NormalizeUrl(url);
				var request = (HttpWebRequest)WebRequest.Create(url);
				if(contentType!=null) {
					request.ContentType = contentType;
				}
				request.Method = "POST";
				return (HttpWebResponse)request.GetResponse();
			}
			catch(WebException e) { 
				return (HttpWebResponse)e.Response;
			}
		}
	}
}
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
			url = NormalizeUrl(url);
			var request = (HttpWebRequest)WebRequest.Create(url);
			return (HttpWebResponse) request.GetResponse();
		}
	}
}
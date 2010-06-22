namespace GitAspx.Tests {
	using System;
	using System.IO;
	using CassiniDev;
	using NUnit.Framework;

	[TestFixture]
	public class BaseTest : CassiniDevServer {
		//Server server;

		[TestFixtureSetUp]
		public void TestFixtureSetup() {
			Console.WriteLine(new DirectoryInfo("..\\..\\GitAspx").FullName);

			StartServer("..\\..\\..\\GitAspx");//   "C:\\Projects\\git-dot-aspx\\GitAspx");
		}

		[TestFixtureTearDown]
		public void TestFixtureTeardown() {
			StopServer();
		}

	}
}
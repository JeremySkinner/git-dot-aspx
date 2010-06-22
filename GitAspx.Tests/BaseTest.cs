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
			StartServer("..\\..\\..\\GitAspx");
		}

		[TestFixtureTearDown]
		public void TestFixtureTeardown() {
			StopServer();
		}

	}
}
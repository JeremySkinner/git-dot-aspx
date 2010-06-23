namespace GitAspx.Tests {
	using System;
	using System.IO;
	using System.Net;
	using System.Text;
	using System.Web.Mvc;
	using System.Web.Routing;
	using NUnit.Framework;

	public static class TestExtensions {
		public static void ShouldEqual<T>(this T acutal, T expected) {
			Assert.AreEqual(expected, acutal);
		}

		public static void ShouldContain(this string actual, string expected) {
			StringAssert.Contains(expected, actual);
		}

		public static string GetString(this Stream stream) {
			var reader = new StreamReader(stream, Encoding.UTF8);
			var read = new char[256];
			int count = reader.Read(read, 0, 256);
			var sb = new StringBuilder();

			while (count > 0) {
				var str = new String(read, 0, count);
				sb.Append(str);
				count = reader.Read(read, 0, 256);
			}

			return sb.ToString();
		}

		public static string GetString(this HttpWebResponse response) {
			return response.GetResponseStream().GetString();
		}

		public static string[] SplitOnNewLine(this string str) {
			return str.Split(new[] { "\n" }, StringSplitOptions.None);
		}

		public static T FakeContxt<T>(this T controller) where T:Controller {
			var context = MockHttpContext.Create();
			var cc = new ControllerContext(context, new RouteData(), controller);
			controller.ControllerContext = cc;
			return controller;
		}
	}
}
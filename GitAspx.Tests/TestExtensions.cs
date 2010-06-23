namespace GitAspx.Tests {
	using System;
	using System.IO;
	using System.Net;
	using System.Text;
	using NUnit.Framework;

	public static class TestExtensions {
		public static void ShouldEqual<T>(this T acutal, T expected) {
			Assert.AreEqual(expected, acutal);
		}

		public static void ShouldContain(this string actual, string expected) {
			StringAssert.Contains(expected, actual);
		}

		public static string GetString(this HttpWebResponse response) {
			var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
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

		public static string[] SplitOnNewLine(this string str) {
			return str.Split(new[] { "\n" }, StringSplitOptions.None);
		}
	}
}
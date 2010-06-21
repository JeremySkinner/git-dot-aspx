using System.IO;
using System.Web.Mvc;

namespace GitAspx.Controllers
{
	public class BaseController : Controller
	{
		protected string PktFlush() {
			return "0000";
		}

		protected string PktWrite(string input, params object[] args) {
			input = string.Format(input, args);
			return (input.Length + 4).ToString("X").PadLeft(4, '0') + input;
		}

		protected void WriteNoCache() {
			Response.AddHeader("Expires", "Fri, 01 Jan 1980 00:00:00 GMT");
			Response.AddHeader("Pragma", "no-cache");
			Response.AddHeader("Cache-Control", "no-cache, max-age=0, must-revalidate");
		}

		protected string ReadBody() {
			var reader = new StreamReader(Request.InputStream);
			return reader.ReadToEnd();
		}
	}
}
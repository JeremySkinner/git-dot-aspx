namespace GitAspx.Lib {
	using System;
	using System.Web.Mvc;

	public class NotFoundResult : ActionResult {
		public override void ExecuteResult(ControllerContext context) {
			context.HttpContext.Response.StatusCode = 404;
			context.HttpContext.Response.ContentType = "text/plain";
			context.HttpContext.Response.Write("Not Found");
		}
	}
}
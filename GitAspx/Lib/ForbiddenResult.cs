namespace GitAspx.Lib {
	using System;
	using System.Web.Mvc;

	public class ForbiddenResult : ActionResult {
		public override void ExecuteResult(ControllerContext context) {
			context.HttpContext.Response.StatusCode = 403;
			context.HttpContext.Response.ContentType = "text/plain";
			context.HttpContext.Response.Write("Forbidden");
		}
	}
}
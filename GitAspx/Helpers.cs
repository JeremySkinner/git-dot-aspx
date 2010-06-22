namespace GitAspx {
	using System.Web.Mvc;
	using System.Web.Routing;

	public static class Helpers {
		public static string ProjectUrl(this UrlHelper urlHelper, string project) {
			return urlHelper.RouteUrl("project", new RouteValueDictionary(new { project }), urlHelper.RequestContext.HttpContext.Request.Url.Scheme, urlHelper.RequestContext.HttpContext.Request.Url.Host);
		}
	}
}
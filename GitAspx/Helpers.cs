namespace GitAspx {
	using System;
	using System.ComponentModel;
	using System.Reflection;
	using System.Web.Mvc;
	using System.Web.Routing;

	public static class Helpers {
		public static string ProjectUrl(this UrlHelper urlHelper, string project) {
			return urlHelper.RouteUrl("project", new RouteValueDictionary(new { project }), urlHelper.RequestContext.HttpContext.Request.Url.Scheme, urlHelper.RequestContext.HttpContext.Request.Url.Host);
		}

		public static string GetDescription(this Enum e) {
			var field = e.GetType().GetField(e.ToString());
			var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
			
			if (attributes.Length > 0) {
				return attributes[0].Description;
			}

			return e.ToString();
		}
	}
}
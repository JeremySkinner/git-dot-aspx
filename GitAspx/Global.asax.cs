using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GitAspx {
	public class MvcApplication : System.Web.HttpApplication {
		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute("info-refs", "{project}/info/refs", 
				new {controller = "InfoRefs", action = "Execute"}, 
				new{ method = new HttpMethodConstraint("GET")});

			routes.MapRoute("upload-pack", "{project}/git-upload-pack",
			                new {controller = "Rpc", action = "UploadPack"},
			                new {method = new HttpMethodConstraint("POST")});


			routes.MapRoute("receive-pack", "{project}/git-receive-pack",
							new { controller = "Rpc", action = "ReceivePack" },
							new { method = new HttpMethodConstraint("POST") });
		}

		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();

			RegisterRoutes(RouteTable.Routes);
		}
	}
}
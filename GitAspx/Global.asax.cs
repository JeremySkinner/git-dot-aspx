namespace GitAspx {
	using System.Web;
	using System.Web.Mvc;
	using System.Web.Routing;
	using GitAspx.Lib;
	using StructureMap;
	using StructureMap.Configuration.DSL;

	public class MvcApplication : HttpApplication {
		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute("DirectoryList", "", new {controller = "DirectoryList", action = "Index"});

			routes.MapRoute("info-refs", "{project}/info/refs",
			                new {controller = "InfoRefs", action = "Execute"},
			                new {method = new HttpMethodConstraint("GET")});

			routes.MapRoute("upload-pack", "{project}/git-upload-pack",
			                new {controller = "Rpc", action = "UploadPack"},
			                new {method = new HttpMethodConstraint("POST")});


			routes.MapRoute("receive-pack", "{project}/git-receive-pack",
			                new {controller = "Rpc", action = "ReceivePack"},
			                new {method = new HttpMethodConstraint("POST")});

			routes.MapRoute("project", "{project}");
		}

		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();

			RegisterRoutes(RouteTable.Routes);

			ObjectFactory.Initialize(cfg => cfg.AddRegistry(new AppRegistry()));
			ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
		}

		private class AppRegistry : Registry {
			public AppRegistry() {
				For<AppSettings>()
					.Singleton()
					.Use(AppSettings.FromAppConfig);
			}
		}
	}
}
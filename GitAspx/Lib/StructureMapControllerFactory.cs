namespace GitAspx.Lib {
	using System.Web.Mvc;
	using GitAspx.Controllers;
	using StructureMap;

	public class StructureMapControllerFactory : DefaultControllerFactory {
		protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, System.Type controllerType) {
			var controller = (IController) ObjectFactory.GetInstance(controllerType);

			var baseCon = controller as BaseController;
			if(baseCon != null) {
				baseCon.AppSettings = ObjectFactory.GetInstance<AppSettings>();
			}

			return controller;
		}
	}
}
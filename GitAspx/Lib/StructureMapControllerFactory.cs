namespace GitAspx.Lib {
	using System.Web.Mvc;
	using StructureMap;

	public class StructureMapControllerFactory : DefaultControllerFactory {
		protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, System.Type controllerType) {
			return (IController) ObjectFactory.GetInstance(controllerType);
		}
	}
}
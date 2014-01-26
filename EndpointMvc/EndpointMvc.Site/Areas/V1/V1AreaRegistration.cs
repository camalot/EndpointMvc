using System.Web.Mvc;

namespace EndpointMvc.Site.Areas.V1 {
	public class V1AreaRegistration : AreaRegistration {
		public override string AreaName {
			get {
				return "V1";
			}
		}

		public override void RegisterArea ( AreaRegistrationContext context ) {
			context.MapRoute (
					"v1_default",
					"api/v1/{controller}/{action}/{id}",
					new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
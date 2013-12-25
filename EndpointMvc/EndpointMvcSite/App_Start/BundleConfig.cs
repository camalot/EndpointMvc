using System.Web;
using System.Web.Optimization;
using EndpointMvc.Extensions;

namespace EndpointMvcSite {
	public class BundleConfig {
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles ( BundleCollection bundles ) {
			bundles.Add ( new ScriptBundle ( "~/js/jquery" ).Include (
									"~/assets/scripts/jquery-{version}.js" ) );

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add ( new ScriptBundle ( "~/js/modernizr" ).Include (
									"~/assets/scripts/modernizr-*" ) );

			bundles.Add ( new ScriptBundle ( "~/js/bootstrap" ).Include (
								"~/assets/scripts/bootstrap.js",
								"~/assets/scripts/respond.js" ) );

			bundles.AddEndpointMvcScripts ( "~/assets/scripts/" );

			bundles.Add ( new StyleBundle ( "~/css/site" )
				.Include (  
									 "~/assets/styles/spacing.css",
									 "~/assets/styles/typography.css",
									 "~/assets/styles/colors.css",
									 "~/assets/styles/gist.css",
									 "~/assets/styles/google.adsense.css",
									 "~/assets/styles/bootstrap.css", 
									 "~/assets/styles/font-awesome.css",
									 "~/assets/styles/nuget.css",
									 "~/assets/styles/bootstrap-overrides.css",
									 "~/assets/styles/bootstrap-win8.css",
									 "~/assets/styles/site.css"
				) 
			);

			bundles.AddEndpointMvcStyles ( "~/assets/styles/" );
		}
	}
}

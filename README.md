EndpointMvc
===========

Builds api endpoint information / documentation dynamically by reflecting over the controllers and actions

Route Registration
----------
EndpointMvc needs to register the route to handle the calls: 

* Register via the EndpoingMvc RouteConfig
>     EndpointMvc.Config.RouteConfig.RegisterRoutes ( routes );

* Register via the RouteCollection Extension Method

>     using EndpointMvc.Extensions;
>     //												
>     routes.RegisterEndpointMvc();

* If you want to be able to call EndpointMvc on specific areas you must either register EndpointMvc routes in each area registration
or call <code>RegisterEnpointMvcForAllAreas()</code> before you call <code>AreaRegistration.RegisterAllAreas()</code>.

>     protected void Application_Start ( ) {
>       // this done before areas allows specific calls to /{area}/endpoints/ to get just area info
>       RouteTable.Routes.RegisterEndpointMvc ( ).RegisterEnpointMvcForAllAreas ( );
>       // now the original registrations
>       AreaRegistration.RegisterAllAreas ( );
>       WebApiConfig.Register ( GlobalConfiguration.Configuration );
>       FilterConfig.RegisterGlobalFilters ( GlobalFilters.Filters );
>       RouteConfig.RegisterRoutes ( RouteTable.Routes );
>       BundleConfig.RegisterBundles ( BundleTable.Bundles );
>       AuthConfig.RegisterAuth ( );
>     }


Once registered, you can get the endpoint data by calling the EndpointMvc controller (endpoints) and the format you 
want to output

* Json
* Xml
* Html 

Calling Html requires there be a view called <code>Html.cshtml</code> in the <code>~/Views/Endpoints</code> path. The sample Mvc application 
has an example of the view

Defining your Endpoints
---------
Endpoints must be defined in order for EndpointMvc to pick them up. 

* EndpointAttribute - <code>( Class )</code>
 - This is declared on a class that is the api endpoint. All public actions in the controller that is defined as an endpoint
will then be registered as an endpoint, unless they are flagged with the <code>IgnoreAttribute</code>.
 >     [Endpoint]
 >     public class MyApiEndpoint { }
* IgnoreAttribute - <code>( Class | Method | Parameter )</code>
 - Ignores a method, parameter, or class from EndpointMvc. 
 - *note: Overloaded action methods are not supported by EndpointMvc. You must flag other overloads with the <code>IgnoreAttribute</code>.*

 >     [AcceptsVerbs( HttpVerbs.Delete )]
 >     public ActionResult Delete ( int id ) { }
 >     
 >     [Ignore]
 >     [AcceptsVerbs( HttpVerbs.Delete )]
 >     public ActionResult Delete ( UserModel user ) { }
* DeprecatedAttribute - <code>( Class | Method )</code>
	- Marks a class or method as deprecated
* RequiresAuthenticationAttribute - <code>( Class | Method )</code>
 - Indicates that the class or method requires a user to be authenticated in order to call the endpoint. 
 - This attribute does not handle the checks on the endpoint to ensure the user is authenticated, you should have 
 some other attribute, like a filter, that performs the check that they are correctly authenticated.
* SinceVersionAttribute - <code>( Class | Method )</code>
 - Indicates that the specified class or method has been available since the specified version
* ObsoleteAttribute - <code>( Class | Method )</code>
 - Flags a method or class as obsolete
* DescriptionAttribute - <code>( Class | Method | Parameter )</code>
 - Describes the class, method, or parameter
* RequiredAttribute - <code>( Parameter )</code>
 - Indicates that the parameter must have a value passed to the endpoint
* OptionalAttribute - <code>( Parameter )</code>
 - Can be used instead of the required attribute. 
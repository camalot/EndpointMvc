EndpointMvc
===========

Builds api endpoint information / documentation dynamically by reflecting over the controllers and actions

Nuget
----------
>    Install-Package EndpointMvc

Demo
---------
[Live Demo](http://endpointmvc.bit13.com)

Route Registration
----------
EndpointMvc needs to register the route to handle the calls: 

* Register via the EndpoingMvc RouteConfig
>     EndpointMvc.Config.RouteConfig.RegisterRoutes ( routes );

* Register via the RouteCollection Extension Method

>     using EndpointMvc.Extensions;
>     //												
>     routes.RegisterEndpointMvc();
>     routes.RegisterEnpointMvcForAllAreas();

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
* Html (default action)

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

* IgnoreAttribute - <code>( Class | Method | Parameter | Property )</code>
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
* AuthorizeAttribute - <code>( Class | Method )</code>
 - Works like <code>RequiresAuthenticationAttribute</code>.
* CustomPropertyAttribute - <code>( Class | Method | Parameter | Property )</code>
 - Allows custom properties to be added to the generated documentation. You can add multiple attributes to the element. Custom 
properties that are added to a Class are merged with properties added to methods.
* SinceVersionAttribute - <code>( Class | Method )</code>
 - Indicates that the specified class or method has been available since the specified version
* ReturnTypeAttribute - <code>( Method )</code>
 - For methods that return <code>ActionResult</code>, this allow you to specify what the return type is. If the return type
is not an ActionResult, this will be calculated from the method. The attribute will override that type.
* ContentTypeAttribute - <code>( Method )</code>
 - Allows you to specify the content types that can be returned. Add an attribute for each.
* ObsoleteAttribute - <code>( Class | Method )</code>
 - Flags a method or class as obsolete
* DescriptionAttribute - <code>( Class | Method | Parameter )</code>
 - Describes the class, method, or parameter
* RequiredAttribute - <code>( Parameter )</code>
 - Indicates that the parameter must have a value passed to the endpoint
* OptionalAttribute - <code>( Parameter )</code>
 - Can be used instead of the required attribute. 
* AcceptsVerbs - <code>( Method )</code>
 - This is used on the action methods to tell MVC what HTTP methods are accepted. EndpointMvc uses these to give details on this information
 - The other single method attributes are used as well, like <code>HttpGetAttribute</code>.
* RequireHttpsAttribute - <code>( Class | Method )</code>
 - Indicates if the request requires SSL.
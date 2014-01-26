using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EndpointMvc.Extensions {
	public static partial class EndpointMvcExtensions {
		public static IHtmlString LiteralFor<TModel, TValue> ( this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression ) {
			return LiteralFor ( helper, expression, null );
		}

		public static IHtmlString LiteralFor<TModel, TValue> ( this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, object htmlAttributes ) {
			return LiteralFor ( helper, expression, new RouteValueDictionary ( htmlAttributes ) );
		}

		public static IHtmlString LiteralFor<TModel, TValue> ( this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes ) {
			ModelMetadata metadata = ModelMetadata.FromLambdaExpression ( expression, html.ViewData );
			string htmlFieldName = ExpressionHelper.GetExpressionText ( expression );
			string literalText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split ( '.' ).Last ( );
			if ( String.IsNullOrEmpty ( literalText ) ) {
				return MvcHtmlString.Empty;
			}
			return MvcHtmlString.Create ( literalText );
		}
	}
}

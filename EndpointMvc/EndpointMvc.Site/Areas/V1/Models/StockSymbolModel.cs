using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndpointMvc.Site.Areas.V1.Models {
	public class StockSymbolModel {
		public String Symbol { get; set; }
		public String Name { get; set; }
		public decimal Price { get; set; }
		public decimal Movement { get; set; }
	}
}
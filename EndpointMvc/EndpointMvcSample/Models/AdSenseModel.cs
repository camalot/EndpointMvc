using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndpointMvcSample.Models {
	public class AdSenseModel {
		public String Client { get; set; }
		public String Slot { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
	}
}
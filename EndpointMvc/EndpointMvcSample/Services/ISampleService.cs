using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace EndpointMvcSample.Services {
	[ServiceContract(Name = "SampleService")]
	public interface ISampleService {

		[OperationContract(Name = "SetIsAllowed" )]
		void SetIsAllowedToDoSomething ( bool allowed );

	}
}
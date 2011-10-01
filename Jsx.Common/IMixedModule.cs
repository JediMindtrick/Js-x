using System;

namespace Jsx
{
	public interface IMixedModule
	{
	 	bool init(JsModule someCallingModule, JsProcess someProcess);
		string loadSource();
	}
}


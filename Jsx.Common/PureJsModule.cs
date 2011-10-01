using System;
using System.IO;

namespace Jsx
{
	public enum ModuleResolutionType{
		COMMMON_JS,
		REQUIRE_JS
	}
	
	public class PureJsModule : JsModule
	{
		private ModuleResolutionType myResType = ModuleResolutionType.COMMMON_JS;
		
		public PureJsModule (JsProcess someProcess,ModuleResolutionType someResType) : base(someProcess)
		{
			myResType = someResType;
		}
		
		protected override string loadSource ()
		{
			//resolve according to myPath
			var fullPath = PathResolver.resolve(myPath,myResType,myProcess);
			
			var toReturn = File.ReadAllText(fullPath);
			
			return toReturn;
		}
	}
}


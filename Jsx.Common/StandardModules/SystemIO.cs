using System;
using System.IO;

namespace Jsx
{
	public enum JS_LOG_LEVEL{
		All = 7,
		Info = 6,
		Trace = 5,
		Debug = 4,
		Warn = 3,
		Error = 2,
		Fatal = 1,
		Off = 0
	}
	
	public class SystemIO : IMixedModule
	{	
		private Jint.JintEngine engine = null;
		private JsProcess process = null;
		
		public SystemIO ()
		{
		}
		
		public bool init(JsModule someCallingModule, JsProcess someProcess){
			var toReturn = true;
			
			engine = someCallingModule.Engine;
			process = someProcess;
			
			engine.SetFunction("__log", new Action<object>(Console.WriteLine));
			engine.SetFunction("__updateLevel", new Action<object>(someProcess.OnChangeLogLevel));
			
			return toReturn;
		}
		
		public string loadSource(){
			var toReturn = File.ReadAllText(PathResolver.resolve("system-console.js",ModuleResolutionType.COMMMON_JS,process));
			
			return toReturn;
		}
	}
}


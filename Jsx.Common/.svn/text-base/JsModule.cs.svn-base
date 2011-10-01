using System;
using System.IO;
using Jint;

namespace Jsx
{
	public class JsModule
	{
		private JintEngine myEngine = new JintEngine();
		protected JsProcess myProcess = null;
		private String myID = Guid.NewGuid().ToString();
		private Jint.Native.JsObject myExported = null;
		protected String myPath = String.Empty;
		
		public String ID { get { return myID; } }
		public JintEngine Engine { get { return myEngine; } }
		protected void logJs(JS_LOG_LEVEL level, string msg, params object[] args){
			myProcess.logJs(level, msg,args);
		}
		private JsModule(){}
		public JsModule (JsProcess someProcess)
		{
			myProcess = someProcess;
			
			myEngine.Run(@"
var module = {};
module.ID = '" + myID + "';");
			
			/*Currently I'm not really using these two variables, but later they will be used to make html5 apis available
			on both server and client -side.  Ex. window.requsetFileSystem() should work regardless of which location we are in.
			The Env object (or something like it) will only exist on the server side.  (thought...maybe just adding a property
			to window is the way to go...window.IsServerside maybe?)*/	
			myEngine.Run(
			@"
var Env = {};
var window = Env;
");
			
			logJs(JS_LOG_LEVEL.Trace, "Created module: {0}",ID);
			
				myEngine.Run(@"
var exports = {};
	");
				
				addRequire();	
			
				bootstrapFunctionality();
		}
		
		public object js(string script, params object[] args){
			return myProcess.js(script, myEngine, args);
		}
		public T js<T>(string script, params object[] args){
			return myProcess.js<T>(script,myEngine,args);
		}
		
		public void bootstrapFunctionality(){
			foreach(var script in myProcess.StandardImports){
				myEngine.Run(script);
			}
		}
		
		public Jint.Native.JsObject requireModule(string somePath){
			myPath = somePath;
			
			if(myExported == null){		
				
				//load module
				String moduleCode = loadSource();
				
				if(moduleCode != String.Empty){
					
				myEngine.Run(moduleCode);
				
				logJs(JS_LOG_LEVEL.Trace, "Providing exports from: {0}", ID);
					
				var export = myEngine.Run(@"
return exports;");
				
				myExported = (Jint.Native.JsObject)export;
				}
				else{
					logJs(JS_LOG_LEVEL.Error, "Did not find any working source-code for module {0}", somePath);
				}
			}
			
			return myExported;
		}
		
		private void addRequire(){
			myEngine.SetFunction("__require",new Func<string,string,Jint.Native.JsObject>(myProcess.requireModule));
			
			 myEngine.Run(@"
require =
function(path,options){
	var OptType = (typeof options);
	var opts = options;
	if(OptType !== 'undefined' && OptType !== 'string' && (typeof JSON) !== 'undefined'){
		//console.trace('require(), attempting to stringify options...');
		opts = JSON.stringify(options);
	}

	return __require(path,opts);
};
"			);	
		}
		
		protected virtual String loadSource(){
			return String.Empty;
		}
	}
}
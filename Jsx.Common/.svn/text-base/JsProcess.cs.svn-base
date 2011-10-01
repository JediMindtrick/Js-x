using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Script;
using System.Web.Script.Serialization;
using Jint;
using Jint.Native;

namespace Jsx
{
	/*
	 * If I decide to go ahead with the String.js() extension method, it should default to use this 
	 * context.
	 * */
	public class JsProcess
	{
		private String loggingStatus = "cache";
		private JsModule myEntryPoint = null;
		private Dictionary<string,JsModule> myModules = new Dictionary<string, JsModule>();
		private List<String> myStandardImports = new List<String>();
		private JS_LOG_LEVEL myLogLevel = JS_LOG_LEVEL.Debug;
		public List<String> StandardImports { get{ return myStandardImports; } }
		
		internal void OnChangeLogLevel(object someLogLevel){
			var toChange = Convert.ToInt32(someLogLevel);
			
			if(toChange >= (int)JS_LOG_LEVEL.Off && toChange <= (int)JS_LOG_LEVEL.All)
				myLogLevel = (JS_LOG_LEVEL)toChange;
		}
		public void changeLogLevel(object someLogLevel){
			OnChangeLogLevel(someLogLevel);
			retrieveCachedModule("Jsx.SystemIO").Engine.Run("console.LogLevel = " + ((int)myLogLevel).ToString() + ";");
		}
		public void logJs(JS_LOG_LEVEL level, string msg, params object[] args){
			if(myLogLevel < level)
				return;
			
			var toPrint = String.Format(msg.Replace(Environment.NewLine,@"\n") ,args);
			var startedCaching = false;
			
			if(loggingStatus == "cache"){
				startedCaching = true;
				js(@"
if(typeof Env.AsyncLogs === 'undefined'){
	Env.AsyncLogs = [];
}

Env.AsyncLogs.push('" + toPrint + "');");
				bool changeStatus = false;
				object result = js(@"return (typeof console !== 'undefined' && typeof console.log !== 'undefined');");
				changeStatus = result != null && (bool)result;
				if(changeStatus){
					loggingStatus = "writecache";
				}
			}
			if(loggingStatus == "writecache"){
				js(@"
for(var i = 0, l = Env.AsyncLogs.length; i < l; i++){
	console.log(Env.AsyncLogs[i]);
}
");
				loggingStatus = "normal";
			}
			if(!startedCaching){
				js("console.log('{0}');",toPrint);
			}
			
		}
		public JsModule EntryPoint{
			get{
				return myEntryPoint;
			}
		}
		
		public JsProcess ()
		{	
			addStandardImports();
			
			//create entry point module
			myEntryPoint = new JsModule(this);
		}
		
		private void addStandardImports(){
			//pre-load, as modules, some functions that we want to make available to all modules 
			//(i.e. standard functionality that will be injected into all other modules inside the JsModule constructor)
			//If we don't first load and cache the module, then we'll get stuck in an infinite loop 
			//in the JsModule constructor
			requireModule("Jsx.SystemIO",@"{""Mode"" : "".net"",""Assembly"" : ""Jsx.Common.dll""}");
			retrieveCachedModule("Jsx.SystemIO").Engine.Run("console.LogLevel =" + ((int)myLogLevel).ToString() + ";");
			myStandardImports.Add(@"var console = require('Jsx.SystemIO').console;
var LOG_LEVEL = require('Jsx.SystemIO').LOG_LEVEL;
");
			requireModule("json2.js",null);
			myStandardImports.Add("var JSON = require('json2.js').JSON;");
		}
		
		private object js(string script, params object[] args){
			//var toRun = String.Format(script,args);
			return this.js(script, myEntryPoint.Engine,args);
		}
		
		private object js(string script){
			return this.js(script,myEntryPoint.Engine);
		}
		private T js<T>(string script, params object[] args){
			return this.js<T>(script,myEntryPoint.Engine,args);
		}
		
		public T js<T>(string script, JintEngine engine, params object[] args){
			object intermediate = js(script,engine,args);
			return Util.ConvertTo<T>(intermediate);
		}
		
		public object js(string script, JintEngine engine, params object[] args){
			var toRun = script; 
			
			if(args != null && args.Length > 0)
				toRun = String.Format(script,args);
			
			return this.js(toRun, engine);
		}
		
		private object js(string script, JintEngine engine){			
			//NOTE:  running with .Run(string,false) and returning the dictionary object .Value, because
			//otherwise we are unable to cast some types of objects when they are returned from this function
			//one example is String[], such as used by Directory.GetFiles().  If we don't do this here, then
			//we are unable to get a reference to the actual array without doing this after we call this function.
			//So it's been put in here for convenience.
			object genericObj = null; 
			
			try{
				genericObj = engine.Run(script,false);
			}
			
			catch(Exception ex){
				//It is bad form to swallow exceptions, but I'm going to do it anyway...
				//the reason this is needed (for now) is that for some reason I'm getting a null reference exception when I run
				//this against the console.log() that I created, which is a method that should return void.
				var WhatIMightLog = ex.ToString();
			}
			
			object toReturn = null;
			
			Jint.Native.JsDictionaryObject returnVal = null; 
			
			if(genericObj != null){
			   	returnVal = (Jint.Native.JsDictionaryObject)genericObj;
				toReturn = returnVal.Value;
			}
			
			return toReturn;
		}
		
		public Jint.Native.JsObject requireModule(string someModule, string someOptions){
			JsModule loadedMod = this.retrieveCachedModule(someModule);
			
			JsObject toReturn = null;
			
			//This method can accept a variable number of arguments
			//If only the path is supplied, then we assume this is a plain old' javascript module
			//we're wanting to load, according to the CommonJs spec...
			if(loadedMod == null && someOptions == null){
				loadedMod = new PureJsModule(this, ModuleResolutionType.COMMMON_JS);
			}
			
			if(loadedMod == null){
				var mySerializer = new JavaScriptSerializer();
				RequireOptions myOptions = mySerializer.Deserialize<RequireOptions>(someOptions);
				
				switch(myOptions.Mode.ToLower()){
					case ".net":
						if(myOptions.Assembly == null || myOptions.Assembly == String.Empty 
					   || someModule == null || someModule == String.Empty){
							logJs(JS_LOG_LEVEL.Error, @"
When requiring a .net module, an assembly and a fully qualified type must be passed.
Passed Assembly: {0}
Passed Type: {1}
", 
						      (myOptions.Assembly == null ? "null" : myOptions.Assembly), 
						      (someModule == null ? "null" : someModule.ToString())
						    );
							break;
						}
						loadedMod = new MixedJsModule(this,someModule,myOptions);
						break;
					case "translate":
						logJs(JS_LOG_LEVEL.Error, "This require mode, {0}, is not yet supported!","'translate'");
						break;
					default:
						var resType = ModuleResolutionType.COMMMON_JS;
						if(myOptions.Resolver != null && myOptions.Resolver.ToLower() == "requirejs")
							resType = ModuleResolutionType.REQUIRE_JS;
					   
						loadedMod = new PureJsModule(this, resType);
						break;
				}
			}
			
			if(loadedMod != null)
				toReturn = loadedMod.requireModule(someModule);
			
			if(toReturn != null && !myModules.ContainsKey(someModule)){
				myModules.Add(someModule,loadedMod);
			}
			
			if(toReturn != null){			
				logJs(JS_LOG_LEVEL.Trace, "Providing module {0}, {1}", someModule, loadedMod.ID);
			}else{
				logJs(JS_LOG_LEVEL.Error, "Module not found, or did not successfully return exports!");
			}
			
			return toReturn;		
		}
		public JsModule retrieveCachedModule(string somePath){
			JsModule toReturn = null;
			
			if(myModules.ContainsKey(somePath))
				toReturn = myModules[somePath];
			
			return toReturn;
		}
	}
}
using System;
using System.IO;
using System.Reflection;

namespace Jsx
{
	public class MixedJsModule : JsModule
	{
		private IMixedModule myMixedModule = null;
		private string myAssemblyName = String.Empty;
		private string myFullyQualifiedType = String.Empty;
		public String AssemblyName { get{ return myAssemblyName; } }
		public String FullyQualifiedType { get{ return myFullyQualifiedType; } }
		
		public MixedJsModule (JsProcess someProcess, string someFullyQualifiedType, RequireOptions someOptions) : base(someProcess)
		{
			myAssemblyName = (someOptions.Assembly != null ? someOptions.Assembly.ToString() : String.Empty);
			myFullyQualifiedType = someFullyQualifiedType;
		}
		
		protected override string loadSource ()
		{
			var toReturn = String.Empty;
			
			try
            {
                Assembly toLoadFrom = null;
                Type ImplementingType = null;

                //determine if the assembly is already loaded
                foreach(var a in AppDomain.CurrentDomain.GetAssemblies()){
					var currName = a.GetName().Name;
				   logJs(JS_LOG_LEVEL.Trace, "{0} == {1}",currName,AssemblyName);
					var pieces = AssemblyName.Split('.');
					var extension = pieces[pieces.Length - 1];
					var shortAssemblyName = AssemblyName.Replace("." + extension,String.Empty);
                   if(currName == AssemblyName
					   || currName == shortAssemblyName
					   ){
                       toLoadFrom = a;
                       break;
                   }
                }
                
				//attempt to load the assembly from the resolved path
                if (toLoadFrom == null) {
					var fullPath = PathResolver.resolve(myPath,ModuleResolutionType.COMMMON_JS,myProcess);

                    if (File.Exists(fullPath))
                    {
                        toLoadFrom = Assembly.LoadFrom(fullPath);
                    }
                }
			
                //attempt to get the type
                if (toLoadFrom != null) {
                    Type[] containedTypes = toLoadFrom.GetTypes();
                    foreach (var t in containedTypes) {
                        if (t.FullName == FullyQualifiedType) {
                            ImplementingType = t;
                            break;
                        }
                    }
                }else{
					logJs(JS_LOG_LEVEL.Error, "Unable to load module in assembly: {0}, type: {1}",myAssemblyName,myFullyQualifiedType);
				}
			
                //attempt to create an instance of the type
                if (ImplementingType != null)
                {
                    myMixedModule = (IMixedModule)Activator.CreateInstance(ImplementingType);
                    //m_GWC[ix].init(m_config);
					myMixedModule.init(this,myProcess);
					toReturn = myMixedModule.loadSource();
                }
            }
            catch (Exception ex) {
				logJs(JS_LOG_LEVEL.Error, ex.ToString());
            }
			
			return toReturn;
		}
	}
}


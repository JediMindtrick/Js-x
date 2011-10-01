using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Jsx
{
	public static class PathResolver
	{
		private static List<String> resolvePaths = new List<String>{
#if DEBUG
			//"/home/brandon/Projects/JS.Net/js/lib/core/",
			//"/home/brandon/Projects/JS.Net/js/lib/installed/"
			"/home/brandon/Desktop/temp/Jsx.Common/lib/core/",
			"/home/brandon/Desktop/temp/Jsx.Common/lib/"
#elif RELEASE
			"/opt/jsx-1.0/lib/core/",  //later will be configured
			"/opt/jsx-1.0/lib/installed/",  //later will be configured
#endif
		};
		public static List<String> ResolvePaths { get{ return resolvePaths; } set{ resolvePaths = value; } }
		
		public static string resolve(string somePath, ModuleResolutionType someResType, JsProcess someProcess){
			var resolved = false;
			var toReturn = somePath;
			
			try{
				resolved = File.Exists(somePath);
			}catch(Exception ex){}
			
			if(!resolved && !somePath.Contains("/")){
				foreach(var p in resolvePaths){
					try{
						resolved = File.Exists(p + somePath);
						if(resolved){
							toReturn = p + somePath;
							break;
						}
					}catch(Exception ex){}
				}
			}
			
			return toReturn;
		}
	}
}


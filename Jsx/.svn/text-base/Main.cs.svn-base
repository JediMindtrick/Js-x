using System;
using System.Text;
using System.IO;
using System.Windows.Forms;
//using Jsx;

namespace Jsx
{
	class MainClass
	{
		private static JsProcess process = new JsProcess();
		private static string[] myArgs;
		
		public static void Main (string[] args)
		{	
			myArgs = args;
			PathResolver.ResolvePaths.Add('"' + Directory.GetCurrentDirectory() + '/' + '"');
			/*
			foreach(var p in PathResolver.ResolvePaths){
				Console.WriteLine(p);
			}
			Console.WriteLine(args[0]);
			*/
			if(myArgs.Length < 1){
				Console.WriteLine("No arguments supplied, REPL started.");
				runREPL();
			}
			else{
				if(myArgs[0] == "-t" || myArgs[0] == "-test")
					runScript("TestBasic.js");
				else
					runScript(myArgs[0]);
			}
		}
		
		private static void runScript(string somePath){

			if(myArgs.Length >= 3 && (myArgs[1] == "-log" || myArgs[1] == "-l")){
				process.changeLogLevel(myArgs[2]);
			}
			var fullPath = PathResolver.resolve(somePath,ModuleResolutionType.COMMMON_JS,process);
			var toRun = File.ReadAllText(fullPath);
			process.js(toRun,process.EntryPoint.Engine);
			//Console.ReadLine();
		}
		
		private static void runREPL(){
			Console.Write("js> ");
			System.ConsoleKeyInfo key;
			StringBuilder script = new StringBuilder();
			while(ConsoleKey.Escape != (key = Console.
			                            ReadKey()).Key){
				if(key.Key == ConsoleKey.F5){
					Console.WriteLine();
					
					var exec = script.ToString();
					
					exec = String.Format(@"
try{
{0}
}catch(err){
console.log(err);
}", exec);
					
					process.js(exec,process.EntryPoint.Engine);
					script = new StringBuilder();
					Console.Write("js> ");
				}
				else if(key.Key == ConsoleKey.Backspace && script.Length > 0){
					script.Remove(script.Length - 1, 1);
				}
				else if (key.Modifiers == ConsoleModifiers.Control
				         && key.Key.ToString() == "V"){
					IDataObject iData = Clipboard.GetDataObject();                
        
			        string s = (string) iData.GetData(DataFormats.Text);
					script.Append(s);
					Console.Write(s);
				}
				else{
					script.Append(key.KeyChar);
				}
			}
		}
	}
}


using System;
using System.ComponentModel;
using System.Reflection;

namespace Jsx
{
	public class TestReturnNullable : IMixedModule
	{
		JsModule myModule = null;
		
		public bool init (JsModule someCallingModule, JsProcess someProcess)
		{
			myModule = someCallingModule;
			myModule.Engine.SetFunction("__test",new Action<bool>(this.testNullable));
			myModule.Engine.SetFunction("__testThrow",new Action(this.testFailTestInCSharp));
			return true;
		}
		
		private void testNullable(bool nullOrNot){
			var toRun = String.Format(@"
var toReturn = returnNullable({0});
return toReturn;
",(nullOrNot ? "true" : "false"));
			int? someNullable = myModule.js<int?>(toRun);
		}
		
		private void testFailTestInCSharp(){
			throw new Exception("Simulate throwing a failed assert in C#");
		}
		
		public string loadSource ()
		{
			var toReturn = @"
var returnNullable =
function(nullOrNot){
	if(!nullOrNot)
		return 5;
	else
		return null;
};

var testNullable = 
function(nullOrNot){
	__test(nullOrNot);
};

var testThrow =
function(){
	__testThrow();
};

exports.testNullable = testNullable;
exports.testThrow = testThrow;
";
			
			return toReturn;
		}
		
		public TestReturnNullable ()
		{
		}
	}
}


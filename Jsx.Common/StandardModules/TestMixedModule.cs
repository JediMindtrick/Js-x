using System;

namespace Jsx
{
	public class TestMixedModule : IMixedModule
	{
		#region IMixedModule implementation
		public bool init (JsModule someCallingModule, JsProcess someProcess)
		{
			return true;
		}
			
		public string loadSource ()
		{
			return @"
var crazy = 'like a fox!';
exports.Foo = 52;
exports.doubleMe =
function(num){
	return num * 2;
};";		
		}
		
		#endregion
		public TestMixedModule ()
		{
		}
	}
}


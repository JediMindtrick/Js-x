var test = require('yuitest-node.js').YUITest;
var assert = test.Assert;

//create the test suite
var suite = new test.TestSuite({
	name : "Environment Test Suite"
});

//add test cases
suite.add(new test.TestCase({
    name: "Basic Environment Tests",
  
     _should: {
     	fail: {"Test should fail" : true}
     	/*,error: {"Function should throw an error in C# and be caught in javascript": true}*/
     },
    "Test should pass" : function(){ assert.areEqual(true,true); },
    "Test should fail" : function(){ assert.areEqual(true,false); },
    "LogLevel should change" : function(){ 
    	var previousLogLevel = console.LogLevel;
    	assert.areEqual(true,true);
    	console.trace('Inside LogLevel should change test');
    	console.trace('Type of LOG_LEVEL = ' + (typeof LOG_LEVEL));
    	console.trace('Type of LOG_LEVEL.Error = ' + (typeof LOG_LEVEL.Error));
    	console.trace('Value of LOG_LEVEL.Error = ' + LOG_LEVEL.Error);
    	console.trace('console loglevel = ' + console.LogLevel);
    	console.trace('Type of console.setLogLevel =  ' (typeof console.setLogLevel));
    	console.setLogLevel(LOG_LEVEL.Error);
    	assert.areEqual(console.LogLevel,LOG_LEVEL.Error);
    	console.setLogLevel(previousLogLevel);
    	assert.areEqual(console.LogLevel,previousLogLevel);
    	//console.log('you should not see this.');
    	//console.error('if you see this then setLogLevel returned and console.error() worked.');
    },
    "Pure js module should load and export Foo = 52" : function(){
    	assert.areEqual(true,true);
    	console.log('Testing require() of a pure js module...');
		console.log('====================================================');
    	var imports = require('TestModule.js').Foo;
    	assert.areEqual(imports,52);
		console.log('Exported:');
		console.log(imports);
		console.log('Test finished.');
		console.log('\n');
    },
    "Mixed module with incorrect arguments should return undefined":function(){
    	console.log('Try require() without the right mix of arguments, mixed-mode missing one arg, this should fail...');
		console.log('====================================================');
		var imports = require('ugh','{"Mode" : ".net"}').doubleMe;
		assert.isUndefined(imports);
		console.log('Trying another bad set of arguments, fully-qualified type does not exist.');
		imports = require('','{"Mode":".net","Assembly":"Jsx.Common.dll"}').doubleMe;
		assert.isUndefined(imports);
		console.log('Test finished.');
		console.log('\n');
    },
    "Mixed module with a set of correct arguments should return the expected test module":function(){
    	console.log('Try require() on mixed-mode with a full set of good args, should succeed...');
		console.log('====================================================');
		exFunc = require('Jsx.TestMixedModule','{"Mode":".net","Assembly":"Jsx.Common.dll"}').doubleMe;
		assert.isTypeOf('function',exFunc);
		assert.areEqual(40,exFunc(20));
		console.log('Test finished.');
		console.log('\n');
    },
    "Attempts to access private module variables should fail":function(){
    	console.log('Two calls in a row that should fail because we are trying to call a variable defined in the module, but not exported...');
    	console.log('====================================================');
		var imports = require('Jsx.TestMixedModule','{"Mode":".net","Assembly":"Jsx.Common.dll"}');
		
		assert.isUndefined(crazy);
		assert.throwsError("Method isn't defined: toString",function(){
			var foo = crazy.toString();
		});
		console.log('Test finished.');
		console.log('\n');
    },
    "JSON object should be available":function(){
    	console.log('See if we can use standard JSON module...');
		console.log('====================================================');
		var original = {};
		original.Foo = 'bar';
		original.Fizz = 'buzz';
		assert.areEqual('buzz', original.Fizz);
		console.trace('Type of original.Fizz = ' + (typeof original.Fizz));
		var	jsonString = JSON.stringify(original);
		console.trace(jsonString);
		assert.areEqual('{"Foo":"bar","Fizz":"buzz"}',jsonString);
		console.trace('Type of JSON.parse = ' + (typeof JSON.parse));
		var parsedString = JSON.parse(jsonString);
		assert.areEqual('buzz',parsedString.Fizz);
		console.log('Test finished.');
		console.log('\n');
    },
    "Function should return nullable integer":function(){
    	assert.areEqual(true,true);
    	var testCall = require('Jsx.TestReturnNullable','{"Mode":".net","Assembly":"Jsx.Common.dll"}').testNullable;
    	testCall(true);
    	testCall(false);
    },
    "Function should throw an error in C# and be caught in javascript":function(){
    	assert.areEqual(true,true);
    	var testCall = require('Jsx.TestReturnNullable','{"Mode":".net","Assembly":"Jsx.Common.dll"}').testThrow;
    	
    	assert.throwsError('Simulate throwing a failed assert in C#',function(){
			testCall();
		});
    },
   	"Mixed module with correct arguments passed as object should return the expected test module":function(){
    	console.log('Try require() on mixed-mode with a full set of good args, should succeed...');
		console.log('====================================================');
		var exFunc = require('Jsx.TestMixedModule',{Mode:".net",Assembly:"Jsx.Common.dll"}).doubleMe;
		assert.isTypeOf('function',exFunc);
		assert.areEqual(40,exFunc(20));
		console.log('Test finished.');
		console.log('\n');
    }
}));

var toLog = typeof test.TestRunner.getResults;
console.trace(toLog);

var runner = test.TestRunner;
runner.add(suite);

console.log('Preparing to run tests.');
runner.run();
console.log('run has returned');


console.setLogLevel(LOG_LEVEL.Trace);
console.trace('Tests complete.  Results:');
var results = 
//runner.getResults(test.TestFormat.JSON);
//runner.getResults(test.TestFormat.JUnitXML);
runner.getResults(test.TestFormat.TAP);
//console.trace('\n');
console.trace(results.toString());
//console.trace(JSON.stringify(results));
//console.trace(JSON.stringify({Test:"object"}));
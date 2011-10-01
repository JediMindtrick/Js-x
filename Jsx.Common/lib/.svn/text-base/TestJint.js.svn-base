console.log('Basic sanity check...');
console.log('====================================================');
console.log('foo here');
console.log('foo again','foo');
console.log('\n');

console.log('Testing require() of a pure js module...');
console.log('====================================================');
var toLog = 
require('/home/brandon/Projects/JS.Net/js/lib/TestModule.js').Foo;
console.log('Exported');
console.log(toLog);
console.log('\n');

//this should log an error to the console
console.log('Try require() without the right mix of arguments, mixed-mode missing one arg, this should fail...');
console.log('====================================================');
var exFunc = require('ugh','{"Mode" : ".net"}').doubleMe;
console.log('\n');

console.log('Try require() again with bad args, fully qualified type does not exist, this should fail...');
console.log('====================================================');
//exFunc = require('','mixed','JsREPL.exe').doubleMe;
exFunc = require('','{"Mode":".net","Assembly":"JsREPL.exe"}').doubleMe;
console.log('\n');

console.log('Try require() on mixed-mode with a full set of good args, should succeed...');
console.log('====================================================');
exFunc = require('JsREPL.TestMixedModule','{"Mode":".net","Assembly":"JsREPL.exe"}').doubleMe;
console.log(exFunc(10));
console.log('\n');

console.log('Two calls in a row that should fail because we are trying to call a variable defined in the module, but not exported...');
//These should fail, because they are declared in the modules, but not exported
console.log(crazy);
try
{
console.log(crazy.toString());
}
catch(err){
console.log(err);
}
console.log('\n');

console.log('See if we can use standard JSON module...');
console.log('====================================================');
try{
	toLog = JSON.stringify({Foo:"bar",Fizz:"buzz"});
	console.log(toLog);
}catch(err){
	console.log(err);
}
console.log('\n');

console.log('See if we can load YUItest module...');
console.log('====================================================');
var test = require('yuitest-node.js').YUITest;
try{
	console.log('This assertion should fail...');
	test.Assert.areEqual(true,false);
}catch(err){
	console.log(err.message);
}
console.log('\n');
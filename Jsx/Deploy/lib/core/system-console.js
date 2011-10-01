var console = {};

console.__log = function(args){
	var toCall = '';

	for(var i = 0, l = arguments.length; i <l; i++){
		toCall += arguments[i] + ' ';
	}

	__log(toCall);
};

var LOG_LEVEL = {};
LOG_LEVEL.All = 7;
LOG_LEVEL.Info = 6;
LOG_LEVEL.Trace = 5;
LOG_LEVEL.Debug = 4;
LOG_LEVEL.Warn = 3;
LOG_LEVEL.Error = 2;
LOG_LEVEL.Fatal = 1;
LOG_LEVEL.Off = 0;
console.LogLevel = LOG_LEVEL.All;

var prettyLogLevels = [];
prettyLogLevels[LOG_LEVEL.Off] = "Off";
prettyLogLevels[LOG_LEVEL.Fatal] = "Fatal";
prettyLogLevels[LOG_LEVEL.Error] = "Error";
prettyLogLevels[LOG_LEVEL.Warn] = "Warn";
prettyLogLevels[LOG_LEVEL.Debug] = "Debug";
prettyLogLevels[LOG_LEVEL.Trace] = "Trace";
prettyLogLevels[LOG_LEVEL.Info] = "Info";
prettyLogLevels[LOG_LEVEL.All] = "All";

console.setLogLevel =
function(level){
	console.log('Inside setLogLevel.');
	console.trace('Attempting to set log level to ' + level);
	
	if(level < LOG_LEVEL.Off || level > LOG_LEVEL.All){
		var toThrow = "LogLevel can only be a value between " + LOG_LEVEL.Off + " and " + LOG_LEVEL.All;
		console.fatal(toThrow);
		throw toThrow;
	}
	
	console.log('Log level corresponds to ' + prettyLogLevels[level]);
	
	console.log('Type of console.Loglevel = ' + (typeof console.LogLevel));
	console.log('Value of console.LogLevel = ' + console.LogLevel);
	
	__updateLevel(level);
	
	try{
		console.LogLevel = level;
	}catch(err){
		console.log(err);
	}
	
	console.log('console.setLogLevel(), new level = ' + console.LogLevel + ', ' + prettyLogLevels[console.LogLevel]);
	
	console.log('console.setLogLevel(), exiting.');
}

console.log =
function(args){
	if(console.LogLevel >= LOG_LEVEL.Info){
		console.__log.apply(this,arguments);
	}
};

console.trace =
function(args){
	if(console.LogLevel >= LOG_LEVEL.Trace){
		console.__log.apply(this,arguments);
	}
}

console.debug =
function(args){
	if(console.LogLevel >= LOG_LEVEL.Debug){
		console.__log.apply(this,arguments);
	}
}

console.warn =
function(args){
	if(console.LogLevel >= LOG_LEVEL.Warn){
		console.__log.apply(this,arguments);
	}
}

console.error =
function(args){
	if(console.LogLevel >= LOG_LEVEL.Error){
		console.__log.apply(this,arguments);
	}
}

console.fatal =
function(args){
	if(console.LogLevel >= LOG_LEVEL.Fatal){
		console.__log.apply(this,arguments);
	}
}

exports.console = console;
exports.LOG_LEVEL = LOG_LEVEL;
=======
**This is a basic module-loader for use with Jint, a C# server-side javascript implementation.**
**This is *not* intended to grow into a .Net version of Node.js!**

Jint can be found at: http://jint.codeplex.com/

Roughly follows the Common.js spec for server-side javascript modules.  Each module is loaded in its own
sandbox and is loaded only once.  More work still needs to be done (i.e. path resolution), but the code
is already functional as-is.

This is currently still very much under development :)

Copyright (c) 2011 Brandon Wilhite. This software is licensed under the MIT License.

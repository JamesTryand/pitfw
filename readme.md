#Pit - F# to JS Compiler
>Lets you code, debug and manage easily

Pit is F# to Javascript compiler that leverages the beauty of F# and also JavaScript.

##Roadmap
###Features
* F# Map Structure
* AJAX
    * Support for mapping response text as JSON
    * Support for reading response XML
* jQuery integration
    * Additionally implement extension points in the compiler to modify the AST using MEF.
* HTML5 apis
    * WebWorkers
    * File API
    * Data attributes
* F# views for ASP.NET MVC (Tomas Petricek's project)
* WinRT + VS2011 integration
    * F# 3.0 build targetting VS 2011
    * Metro-specific application templates
    * JavaScript bindings to WinJS
Note: Incremental release based on feature set completion, and no specific version based releases from now on. This is because we have initial set of feature set ready with Pit. Any of these features could be contributed by the community.

##Release Notes
###Pit v0.2

####Features
* Array2D, F# Set, F# String extensions
* Operator overloading for types, records, unions
* AJAX / XMLHttpRequest both in debug/JS mode
* HTML5 DOM elements & SVG
* Custom Library project templates
* Mac MonoDevelop support
####Fixes
* Fix closure issue in for loops
* Fix mapping .NET string functions to JsString
* Fix tuple value code generation in function parameters
* Fix type extensions for DOM type classes
* Fix overloaded constructors issue
* Fix overloaded members
* Fix code generation issue with static property GETTER
* Fix missing method for Window.setInterval

###Pit v0.1

* Support all features of F# that can be translated using "ReflectedDefinitionAttribute"
* Clean JS code generation
* Support below major F# libraries
  * Seq
  * List
  * Array
  * Event
  * IObservable
  * Computation Expressions
* Full HTML DOM and HTML5 Canvas Support
* Visual Studio 2010 integration with Application Project Templates
  * Debugging support for F# code
  * Build support with Pit compiler
  * JavaScript error notification
     
##How to contribute?
If you would like to contribute to the Pit project you could take up any of the features that is available in the roadmap / any custom feature that you think would provide nice value. You can email mohamedsuhaiba@live.in for dicussing and getting included in the list.
Note: All features should pass the existing tests found in the "tests" folder and also provide new tests for the feature, only then the feature will be accepted for inclusion in the main branch.